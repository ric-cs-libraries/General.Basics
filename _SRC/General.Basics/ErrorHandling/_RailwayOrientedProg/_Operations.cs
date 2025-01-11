namespace General.Basics.ErrorHandling.RailwayOrientedProg;

public delegate Result<TResponse> QueryOperation<TResponse>();
public delegate Result<TResponse> QueryOperation<TOperationParamType, TResponse>(TOperationParamType param);

public delegate Result CommandOperation();
public delegate Result CommandOperation<TOperationParamType>(TOperationParamType param);


