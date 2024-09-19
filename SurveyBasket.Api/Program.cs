using Asp.Versioning;
using FluentValidation.AspNetCore;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Authentication.Filters;
using SurveyBasket.Api.Errors;
using SurveyBasket.Api.Health;
using SurveyBasket.Api.Settings;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Mapster
var mapConfig = TypeAdapterConfig.GlobalSettings;
mapConfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IMapper>(new Mapper(mapConfig));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailSender, EmailService>();
builder.Services.AddScoped<IPollService,PollService>();
builder.Services.AddScoped<IQuestionService,QuestionService>();
builder.Services.AddScoped<IVoteService,VoteService>();
builder.Services.AddScoped<IResultService ,ResultService>();
builder.Services.AddScoped<INotificationService , NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddScoped<ICacheService, CacheService>();



builder .Services.AddSingleton<IJwtProvider, JwtProvider>();
//Fluent Validation package
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();


//database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().
	AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddTransient<IAuthorizationHandler,PermissionAuthorizationHandler>();  
builder.Services.AddTransient<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();  
	

builder.Services.Configure<IdentityOptions>(options => { 
	options.SignIn.RequireConfirmedEmail = true;
	options.User.RequireUniqueEmail = true;
});


//Options class
builder.Services.AddOptions<JwtOptions>()
	.BindConfiguration("Jwt")
	.ValidateDataAnnotations()
	.ValidateOnStart();

var JwtSetting = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

//add jwt config
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(o =>
	 {
		 o.SaveToken = true;
		 o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		 {
			 ValidateIssuerSigningKey = true,
			 ValidateIssuer = true,
			 ValidateAudience = true,
			 ValidateLifetime = true,
			 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting?.Key!)),
			 ValidIssuer = JwtSetting?.Issuer,
			 ValidAudience = JwtSetting?.Audience
         };
	});
//Adding Cors
	builder.Services.AddCors(options =>
			options.AddPolicy("AllowAll",builder=>
				builder.AllowAnyOrigin()
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
			)
	);
//Exeption Handler
builder.Services.AddExceptionHandler<GlopalExeptionHanler>();
builder.Services.AddProblemDetails();

// Add Serilog 
builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration)

);

//Caching
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

// Email Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

//hangfire 
builder.Services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

//healt chek
builder.Services.AddHealthChecks()
	.AddHangfire(options =>
	{
		options.MinimumAvailableServers = 1;
	})
	.AddCheck<MailProviderHealthCheck>(name:"mail Service");

//Rate Limiting
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    rateLimiterOptions.AddPolicy("IpLimiter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 2,
                Window = TimeSpan.FromSeconds(20)
            }
    )
    );

    rateLimiterOptions.AddPolicy("UserLimiter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.GetUserId(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 2,
                Window = TimeSpan.FromSeconds(20)
            }
    )
    );

    rateLimiterOptions.AddConcurrencyLimiter("Concurrency", options =>
    {
        options.PermitLimit = 1000;
        options.QueueLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });


});

// Versioning
builder.Services.AddApiVersioning(options =>
{
	options.DefaultApiVersion = new ApiVersion(1);
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.ReportApiVersions = true;

	options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
}).AddApiExplorer(options => {
	options.GroupNameFormat = "'v'V";
	options.SubstituteApiVersionInUrl=true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
	Authorization =
	[
		new HangfireCustomBasicAuthenticationFilter
		{
			User =app.Configuration.GetValue<string>("HangFireSettings:UserName"),
			Pass =app.Configuration.GetValue<string>("HangFireSettings:Password")
		}
	]
});
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

RecurringJob.AddOrUpdate("SendNewPollNotification", () => notificationService.SendNewPollNotification(null),Cron.Daily);


//middleware for pollicy should be before auth
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.UseRateLimiter();

app.MapHealthChecks("health");

app.Run();
