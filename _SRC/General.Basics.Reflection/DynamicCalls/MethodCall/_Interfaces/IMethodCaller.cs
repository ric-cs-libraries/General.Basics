namespace General.Basics.Reflection.DynamicCalls.Interfaces;

public interface IMethodCaller
{
    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    TReturnType? Call<TReturnType>(object obj, string methodName, object?[]? parameters = null);


    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    void Call(object obj, string methodName, object?[]? parameters = null);
}
