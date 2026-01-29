using General.Basics.Extensions;
using General.Basics.Extensions.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace General.Basics.UnitTests.Extensions.POO;
using static IEnumerableExtensionTests.DowncastAll_Fixtures; //<<<<<<<<<<<<<

public class IEnumerableExtensionTests
{
    #region DowncastAll_
    [Fact]
    public void DowncastAll_WhenAllCanBeDowncast_ShouldDowncastAll()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2
        };

        //--- Assert sur l'Original ---
        Assert.True(listeEtresVivants.All(instance => typeof(Vertebre).IsInstanceOfType(instance)));
        Assert.True(listeEtresVivants.All(instance => instance is Vertebre));
        Assert.True(listeEtresVivants.All(instance => typeof(Vertebre).IsAssignableFrom(instance.GetType())));
        Assert.True(listeEtresVivants.All(instance => typeof(IEtreVivant).IsAssignableFrom(instance.GetType())));
        // listeEtresVivants.ForEach(instance => instance.MethodePourVertebreUniquement()); //<<< INTERDIT car les élts
                                                                                            //sont considérés de type IEtreVivant

        //--- Act ---
        List<Vertebre> vertebres = listeEtresVivants.DowncastAll_<IEtreVivant, Vertebre>().ToList();

        //--- Assert sur le Résultat ---
        Assert.True(vertebres.All(instance => typeof(Vertebre).IsInstanceOfType(instance)));
        Assert.True(vertebres.All(instance => instance is Vertebre));
        Assert.True(vertebres.All(instance => typeof(Vertebre).IsAssignableFrom(instance.GetType())));
        Assert.True(vertebres.All(instance => typeof(IEtreVivant).IsAssignableFrom(instance.GetType())));
        vertebres.ForEach(instance => instance.MethodePourVertebreUniquement()); //<<< AUTORISÉ, car cette fois
                                                                                 // les élts sont bien vus comme de type
                                                                                 // Vertebre !
    }

    [Fact]
    public void DowncastAll_WhenNotAllCanBeDowncast_ShouldThrowACannotDowncastExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3 = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3,
            etreVivant4
        };
        var unconvertibleInstance = listeEtresVivants.First(instance => instance is not Vertebre);
        var unconvertibleInstanceIndex = listeEtresVivants.IndexOf(unconvertibleInstance);

        //--- Assert sur l'Original ---
        //Assert.False(listeEtresVivants.All(instance => typeof(Vertebre).IsInstanceOfType(instance)));
        //Assert.False(listeEtresVivants.All(instance => instance is Vertebre));
        //Assert.False(listeEtresVivants.All(instance => typeof(Vertebre).IsAssignableFrom(instance.GetType())));
        //Assert.True(listeEtresVivants.All(instance => typeof(IEtreVivant).IsAssignableFrom(instance.GetType())));

        //--- Act & Assert ---
        var ex = Assert.Throws<CannotDowncastException>(() => listeEtresVivants.DowncastAll_<IEtreVivant, Vertebre>());

        var expectedMessage = string.Format(CannotDowncastException.MESSAGE_FORMAT,
                                            unconvertibleInstance.GetType().Name,
                                            nameof(Vertebre),
                                            $"Item[{unconvertibleInstanceIndex}] in List<{nameof(IEtreVivant)}>"
                                            );

        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion DowncastAll_

    #region CanDowncastAll_
    [Fact]
    public void CanDowncastAll_WhenAllCanBeDowncast_ShouldReturnTrue()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2
        };

        //--- Act ---
        bool result = listeEtresVivants.CanDowncastAll_<IEtreVivant, Vertebre>();

        //--- Assert sur le Résultat ---
        Assert.True(result);
    }

    [Fact]
    public void CanDowncastAll_WhenNotAllCanBeDowncast_ShouldReturnFalse()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3 = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3,
            etreVivant4
        };

        //--- Act ---
        bool result = listeEtresVivants.CanDowncastAll_<IEtreVivant, Vertebre>();

        //--- Assert sur le Résultat ---
        Assert.False(result);
    }
    #endregion CanDowncastAll_

    #region ContainsOnlyThoseConcreteTypes_
    [Fact]
    public void ContainsOnlyThoseConcreteTypes_WhenAllAreAmongPossibleTypes_ShouldReturnTrue()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3 = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3,
            etreVivant4
        };

        IEnumerable<Type> possibleTypes = new[] { typeof(Vertebre), typeof(Invertebre) };

        //--- Act ---
        bool result = listeEtresVivants.ContainsOnlyThoseConcreteTypes_(possibleTypes);

        //--- Assert sur le Résultat ---
        Assert.True(result);
    }

    [Fact]
    public void ContainsOnlyThoseConcreteTypes_WhenAllAreAmongPossibleTypesWithNull_ShouldReturnTrue()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3 = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant?> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3,
            null,
            etreVivant4
        };

        IEnumerable<Type?> possibleTypes = new[] { typeof(Vertebre), typeof(Invertebre), null, typeof(Arbre) };

        //--- Act ---
        bool result = listeEtresVivants.ContainsOnlyThoseConcreteTypes_(possibleTypes);

        //--- Assert sur le Résultat ---
        Assert.True(result);
    }

    [Fact]
    public void ContainsOnlyThoseConcreteTypes_WhenAllAreAmongPossibleTypesWithNull_ShouldReturnTrue_2()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Arbre();
        IEtreVivant etreVivant2 = new Invertebre();
        IEtreVivant etreVivant3 = new Invertebre();
        IEtreVivant etreVivant4 = new Arbre();
        List<IEtreVivant?> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3,
            null,
            etreVivant4
        };

        IEnumerable<Type?> possibleTypes = new[] { typeof(Vertebre), typeof(Invertebre), null, typeof(Arbre) };

        //--- Act ---
        bool result = listeEtresVivants.ContainsOnlyThoseConcreteTypes_(possibleTypes);

        //--- Assert sur le Résultat ---
        Assert.True(result);
    }

    [Fact]
    public void ContainsOnlyThoseConcreteTypes_WhenNotAllAreAmongPossibleTypes_ShouldReturnFalse()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3NotVertebre = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3NotVertebre,
            etreVivant4
        };

        IEnumerable<Type> possibleTypes = new[] { typeof(Vertebre) };

        //--- Act ---
        bool result = listeEtresVivants.ContainsOnlyThoseConcreteTypes_(possibleTypes);

        //--- Assert sur le Résultat ---
        Assert.False(result);
    }

    [Fact]
    public void ContainsOnlyThoseConcreteTypes_WhenNotAllAreAmongPossibleTypes_ShouldReturnFalse_2()
    {
        //--- Arrange ---
        IEtreVivant etreVivant1 = new Vertebre();
        IEtreVivant etreVivant2 = new Vertebre();
        IEtreVivant etreVivant3NotVertebre = new Invertebre();
        IEtreVivant etreVivant4 = new Vertebre();
        List<IEtreVivant?> listeEtresVivants = new()
        {
            etreVivant1,
            etreVivant2,
            etreVivant3NotVertebre,
            null, //<<<<<
            etreVivant4
        };

        IEnumerable<Type> possibleTypes = new[] { typeof(Vertebre), typeof(Invertebre) };

        //--- Act ---
        bool result = listeEtresVivants.ContainsOnlyThoseConcreteTypes_(possibleTypes);

        //--- Assert sur le Résultat ---
        Assert.False(result);
    }
    #endregion ContainsOnlyThoseConcreteTypes_

    internal static class DowncastAll_Fixtures
    {
        internal interface IEtreVivant
        {

        }

        internal interface IAnimal : IEtreVivant
        {

        }

        internal abstract class AEtreVivant : IEtreVivant
        {

        }

        internal abstract class AAnimal : AEtreVivant, IAnimal
        {

        }

        internal abstract class AVegetal : AEtreVivant
        {

        }

        //---------------

        internal class Vertebre : AAnimal
        {
            public void MethodePourVertebreUniquement()
            {

            }
        }

        internal class Invertebre : AAnimal
        {
        }

        internal class Arbre : AVegetal
        {
        }
    }
}
