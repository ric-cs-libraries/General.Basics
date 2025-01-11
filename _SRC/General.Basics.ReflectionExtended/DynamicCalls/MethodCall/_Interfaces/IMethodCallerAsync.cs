namespace General.Basics.ReflectionExtended.DynamicCalls.Interfaces;

public interface IMethodCallerAsync
{
    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    //La méthode à invoquer devra retourner une : Task<TReturnType?> .
    Task<TReturnType?> Call<TReturnType>(object obj, string methodName, object?[]? parameters = null);


    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    //La méthode à invoquer devra retourner une : Task.
    Task Call(object obj, string methodName, object?[]? parameters = null);
}
