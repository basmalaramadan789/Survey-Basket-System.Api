﻿namespace SurveyBasket.Api.Services
{
	public interface IPollService
	{
		Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<IEnumerable<PollResponse>> GetCurrentAsyncV1(CancellationToken cancellationToken = default);
		Task<IEnumerable<PollResponseV2>> GetCurrentAsyncV2(CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> AddAsync(CreatePollRequest request, CancellationToken cancellationToken = default);
		Task<Result> UpdateAsync(int id , CreatePollRequest poll, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default);
	}
}
