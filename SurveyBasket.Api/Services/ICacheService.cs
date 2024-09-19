namespace SurveyBasket.Api.Services
{
    public interface ICacheService
    {
        Task<T?> GetAsunc<T>(string key,CancellationToken cancellationToken = default) where T : class;
        Task SetAsunc<T>(string key,T value ,CancellationToken cancellationToken = default) where T : class;
        Task RemoveAsunc(string key ,CancellationToken cancellationToken = default) ;
    }
}
