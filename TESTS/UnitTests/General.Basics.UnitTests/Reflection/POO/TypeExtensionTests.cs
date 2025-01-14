using General.Basics.Reflection.POO.Extensions;
using Xunit;

namespace General.Basics.UnitTests.Reflection.POO.Extensions;

public class TypeExtensionTests
{
    #region IsOfType_
    [Fact]
    public void IsOfType__ShouldReturnTheCorrectBoolValue()
    {
        var enfant1 = new Fixtures.Enfant1();
        var enfant2 = new Fixtures.Enfant2();

        Assert.Equal(typeof(Fixtures.Parent1<int, bool>), enfant1.GetType().BaseType!); //Obligé de spécifier les arguments de type.
        Assert.NotEqual(typeof(Fixtures.Parent1<,>), enfant1.GetType().BaseType!);      //D'où l'intérêt de IsOfType_ ci-dessous.

        Assert.True(enfant1.GetType().BaseType!.IsOfType_(typeof(Fixtures.Parent1<,>)));
        Assert.False(enfant1.GetType().IsOfType_(typeof(Fixtures.Parent1<,>)));


        Assert.Equal(typeof(Fixtures.Parent2<string>), enfant2.GetType().BaseType!); //Obligé de spécifier les arguments de type.
        Assert.NotEqual(typeof(Fixtures.Parent2<>), enfant2.GetType().BaseType!);    //D'où l'intérêt de IsOfType_ ci-dessous.

        Assert.True(enfant2.GetType().BaseType!.IsOfType_(typeof(Fixtures.Parent2<>)));
        Assert.False(enfant2.GetType().IsOfType_(typeof(Fixtures.Parent2<>)));
    }
    #endregion IsOfType_

    #region Implements_
    [Fact]
    public void Implements__ShouldReturnTheCorrectBoolValue()
    {
        var oc = new Fixtures.MyClass(); //Implements Interface1
        var or = new Fixtures.MyRecord(); //Implements Interface2
        var oc2 = new Fixtures.SomeClass();  //Implements Interface3<T>
        var occ = new Fixtures.SomeChildClass(); //SomeChildClass inherits from MyClass

        Assert.True(oc.GetType().Implements_(typeof(Fixtures.Interface0))); //Interface1 inherits from Interface0
        Assert.True(oc.GetType().Implements_(typeof(Fixtures.Interface1)));
        Assert.False(oc.GetType().Implements_(typeof(Fixtures.Interface2)));

        Assert.True(or.GetType().Implements_(typeof(Fixtures.Interface0))); //Interface2 inherits from Interface0
        Assert.True(or.GetType().Implements_(typeof(Fixtures.Interface2)));
        Assert.False(or.GetType().Implements_(typeof(Fixtures.Interface1)));

        Assert.True(oc2.GetType().Implements_(typeof(Fixtures.Interface0))); //Interface3<T> inherits from Interface0
        Assert.False(oc2.GetType().Implements_(typeof(Fixtures.Interface3<>))); //<<<< ATTENTION false, pour les types
                                                                                //interface génériques, sans argument de type !
        Assert.True(oc2.GetType().Implements_(typeof(Fixtures.Interface3<int>))); //OK, car argument de type (le bon (int)) précisé.
        Assert.False(oc2.GetType().Implements_(typeof(Fixtures.Interface3<string>)));
        Assert.False(oc2.GetType().Implements_(typeof(Fixtures.Interface2)));
        Assert.False(oc2.GetType().Implements_(typeof(Fixtures.Interface1)));

        //
        Assert.True(occ.GetType().Implements_(typeof(Fixtures.Interface0))); //Interface1 inherits from Interface0
        Assert.True(occ.GetType().Implements_(typeof(Fixtures.Interface1)));
        Assert.False(occ.GetType().Implements_(typeof(Fixtures.Interface2)));
    }
    #endregion Implements_


    //=============================================
    static class Fixtures
    {
        #region IsOfType_
        public class Parent1<T, U>
        {

        }

        public record Parent2<V>
        {

        }

        public class Enfant1 : Parent1<int, bool>
        {

        }
        public record Enfant2 : Parent2<string>
        {

        }
        #endregion IsOfType_

        #region Implements_

        public interface Interface0
        {

        }
        public interface Interface1 : Interface0
        {

        }
        public interface Interface2 : Interface0
        {

        }
        public interface Interface3<T> : Interface0
        {

        }
        public class MyClass : Interface1
        {

        }
        public record MyRecord : Interface2
        {

        }
        public class SomeClass : Interface3<int>
        {

        }
        public class SomeChildClass : MyClass
        {

        }
        #endregion Implements_
    }
}

