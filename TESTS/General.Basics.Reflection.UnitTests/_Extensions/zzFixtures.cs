
namespace General.Basics.Reflection.Extensions.UnitTests;



interface IMyClass
{
    void Method1(string p1, bool? p2, MyClass? p3, int p40);

    string Method2(DateTime dt);
}
class MyClass : IMyClass
{
    public void Method1(string p1, bool? p2, MyClass? p3, int p4 = 10)
    {

    }

    public string Method2(DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy");
    }
}


interface IMyClass<T, U, V>
{
    void Method(T p1, U? p2, V p3);
}
class MyClass<T,U> : IMyClass<T, U, bool>
    {
    public void Method(T p1, U? p2, bool p3)
    {

    }
}

class MyClassAsync
{
    public async Task GetX() { await Task.CompletedTask;  }

    public async Task<string> GetStr() { return await Task.FromResult(""); }
}
