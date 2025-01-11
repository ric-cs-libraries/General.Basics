namespace General.Basics.ErrorHandling.RailwayOrientedProg;

public delegate Task<Result<TResponse>> QueryOperationAsync<TResponse>();
public delegate Task<Result<TResponse>> QueryOperationAsync<TOperationParamType, TResponse>(TOperationParamType param);

//public delegate Task<Result> CommandOperationAsync();
//public delegate Task<Result> CommandOperationAsync<TOperationParamType>(TOperationParamType param);

