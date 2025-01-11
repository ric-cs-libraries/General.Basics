
namespace General.Basics.ErrorHandling.RailwayOrientedProg;


public static class ROPViaResultAsyncExtension
{
    public static async Task<Result<TResponse>> OnSuccess<TResponse>(this Result previousResult, QueryOperationAsync<TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return await operation()
        ;
        return Result<TResponse>.FromResult(previousResult);
    }

    public static async Task<Result<TResponse>> OnSuccess<TIn, TResponse>(this Result<TIn> previousResult, QueryOperationAsync<TIn, TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return await operation(previousResult.Value)
        ;
        return Result<TResponse>.FromResult(previousResult);
    }
}
