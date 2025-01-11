
namespace General.Basics.ErrorHandling.RailwayOrientedProg;


/*
 * Railway Oriented Programming : dans le principe, on a typiquement le genre de méthodes que j'ai écrites dans la classe d'extension ROPViaResultExtension, plus bas.
 * 
 * Le souci c'est quand même que : il y a en réalité, des tas de possibilités concernant la signature des méthodes.
 *  Notamment, concernant le param. que j'appelle "operation" : cette méthode "operation", peut en fait prendre des params ou pas, éventuellement prendre le previousResult, et d'autres choses, ...,
 *  de plus, elle peut aussi retourner tout un tas de choses (sauf dans OnSuccess où le type retourné est cadré).
 *  De plus..., on peut aussi imaginer les versions Async de tout ça... on n'en finit pas...
 *  Là, j'ai donc mis quelques méthodes, loin d'être exhaustif tout ça, ce sera peut-être à compléter en fonction des besoins.
 *  
 *  A noter quand même, que globalement, l'utilisation de ces syntaxes, bien qu'elles allègent le code, peut le rendre un peu moins compréhensible quand on est pas encore habitué 
 *  à ce genre de syntaxe et de raisonnements qui vont avec.
 *  
 *  Rem.: ici, ma méthode Then(), équivaut en fait à ce qu'ils appellent eux, OnBoth().
 */

/*
 * Bon, avec l'existant en l'état(à priori fonctionnel de ma classe d'extension ROPViaResultExtension), 
 * voici 3 exemples concrets d'utilisation, normalement valides, et que je pourrais dès à présent, mettre en place :
 

    A) Corps de CreateCustomerUseCase.Handle(CreateCustomerQuery request) :

        Result<CustomerName> nameResult = CustomerName.Create(request.Name);
        Result<CustomerBirthDate> birthDateResult = CustomerBirthDate.Create(request.birthDate);

        return Result<CreateCustomerResponse>.WithAllErrorsOrOk(nameResult, birthDateResult)
        .OnSuccess<CreateCustomerResponse>(() =>
        {
            Customer newCustomer = new Customer(nameResult.Value!, birthDateResult.Value!);
            repository.Add(newCustomer);

            return Result<CreateCustomerResponse>.Ok(new CreateCustomerResponse(newCustomer.Id, newCustomer.Name, newCustomer.BirthDate));
        });



    B) Corps de la méthode:  Result FileWriter.RenameAndOverwrite(string fileFullName, string fileNewName) :

        return ValidateFileFullName(fileFullName)
        .OnSuccess(() => ValidateFileName(fileNewName))
        .OnSuccess(() =>
        {
            FileInfo fileFullNameInfo = new(fileFullName);
            string samePath = fileFullNameInfo.DirectoryName ?? string.Empty;

            string targetFileFullName = $@"{Path.Combine(samePath, fileNewName)}";
            return MoveAndOverwrite(fileFullName, targetFileFullName);
        });



    C) Corps de la méthode:  Result MoveAndOverwrite(string sourceFileFullName, string targetFileFullName) :

        return ValidateFileFullName(sourceFileFullName)
               .OnSuccess(() => ValidateFileFullName(targetFileFullName))
               .OnSuccess(() =>
               {
                   if (targetFileFullName.Backslash_().ToUpper() == sourceFileFullName.Backslash_().ToUpper())
                       return Result.NotOk(new FileCannotBeMovedOnItselfError(sourceFileFullName))
                   ;
                   return Result.Ok();
               })
               .OnSuccess(() =>
               {
                   if (!File.Exists(sourceFileFullName))
                       return Result.NotOk(new FileNotFoundError(sourceFileFullName))
                   ;
                   return Result.Ok();
               })
               .OnSuccess(() =>
               {
                   if (File.Exists(targetFileFullName))
                   {
                       return Delete(targetFileFullName);
                   }
                   else
                   {
                       string targetFilePath = new FileInfo(targetFileFullName).DirectoryName!;
                       return FolderWriter.CreateIfNotExists(targetFilePath);
                   }
               })
               .OnSuccess(() =>
               {
                   try
                   {
                       File.Move(sourceFileFullName, targetFileFullName);
                       if (File.Exists(sourceFileFullName) || !File.Exists(targetFileFullName))
                           return Result.NotOk(new FileCannotBeMovedError(sourceFileFullName, targetFileFullName))
                       ;
                       return Result.Ok();
                   }
                   catch (Exception ex)
                   {
                       return Result.NotOk(ex.ToError());
                   }
               });
 
 */
public static class ROPViaResultExtension
{
    public static Result<TResponse> Then<TResponse>(this Result<TResponse> previousResult, CommandOperation operation)
    {
        operation();
        return previousResult;
    }
    public static Result Then(this Result previousResult, CommandOperation operation)
    {
        operation();
        return previousResult;
    }

    public static Result<TResponse> OnSuccess<TResponse>(this Result<TResponse> previousResult, QueryOperation<TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return operation()
        ;
        return previousResult;
    }
    public static Result<TResponse> OnSuccess<TResponse>(this Result<TResponse> previousResult, QueryOperation<TResponse, TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return operation(previousResult.Value)
        ;
        return previousResult;
    }
    public static Result<TResponse> OnSuccess<TIn, TResponse>(this Result<TIn> previousResult, QueryOperation<TIn, TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return operation(previousResult.Value)
        ;
        return Result<TResponse>.FromResult(previousResult);
    }
    public static Result<TResponse> OnSuccess<TResponse>(this Result previousResult, QueryOperation<TResponse> operation)
    {
        if (previousResult.IsSuccess)
            return operation()
        ;
        return Result<TResponse>.FromResult(previousResult);
    }
    public static Result OnSuccess(this Result previousResult, CommandOperation operation)
    {
        if (previousResult.IsSuccess)
            return operation()
        ;
        return previousResult;
    }
    public static Result OnSuccess<TIn>(this Result<TIn> previousResult, CommandOperation<TIn> operation)
    {
        if (previousResult.IsSuccess)
            return operation(previousResult.Value)
        ;
        return previousResult;
    }
    


    public static Result<TResponse> OnFailure<TResponse>(this Result<TResponse> previousResult, CommandOperation<IReadOnlyList<Error>> operation)
    {
        if (previousResult.IsFailure)
            operation(previousResult.Errors!)
        ;
        return previousResult;
    }
    public static Result OnFailure(this Result previousResult, CommandOperation<IReadOnlyList<Error>> operation)
    {
        if (previousResult.IsFailure)
            operation(previousResult.Errors!)
        ;
        return previousResult;
    }

}
