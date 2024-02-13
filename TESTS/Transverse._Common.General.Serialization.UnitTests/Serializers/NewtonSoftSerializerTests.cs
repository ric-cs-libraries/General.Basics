using Xunit;


using Transverse._Common.General.Serialization;
using Transverse._Common.General.Serialization.Interfaces;

namespace Transverse._Common.General.Serialization.Serializers.UnitTests;

public class NewtonSoftSerializerTests
{
    [Fact]
    public void Create_WhenCalled_ShouldReturnANewInstanceOfISerializer()
    {
        ISerializer s1 = NewtonSoftSerializer.Create();
        ISerializer s2 = NewtonSoftSerializer.Create();

        Assert.NotEqual(s1, s2);
    }

    [Fact]
    public void ToJson_WhenCalled_ShouldReturnTheCorrectJsonString()
    {
        ISerializer s1 = NewtonSoftSerializer.Create();
        var obj = new MyClass();

        string objSerialized = s1.ToJson(obj);
        //var tempFile = "T:/zTempForTest.txt"; File.WriteAllText(tempFile, objSerialized);
        var expected = @$"{{""MyInt"":105,""MyString"":""HELLO"",""MyBool"":false,""MyStringsList"":[""str1"",""str2""],""MyDateAsString"":""{DateTime.Now.ToString("dd/MM/yyyy")}""}}";
        //File.AppendAllText(tempFile, expected);
        Assert.Equal(expected, objSerialized);
    }

    [Fact]
    public void FromJson_WhenCalledWithASerializedClassInstance_ShouldReturnTheCorrectClassInstance()
    {
        ISerializer s1 = NewtonSoftSerializer.Create();
        List<string> myStringsList = new() { "str10", "str20" };
        var obj = new MyClass(myStringsList);
        string objSerialized = s1.ToJson(obj);

        MyClass? unserializedObj = s1.FromJson<MyClass>(objSerialized);

        Assert.Equivalent(obj, unserializedObj);
    }


    private class MyClass
    {
        public MyClass(List<string>? myStringsList = null)
        {
            MyStringsList = myStringsList ?? new() { "str1", "str2" };
        }
        public int MyInt { get; set; } = 105;
        public string MyString { get; set; } = "HELLO";

        public bool MyBool { get; set; }

        public List<string> MyStringsList { get; set; }

        public string MyDateAsString { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    }


}