namespace StringFormatter.Tests.Classes;

public class FakeClass
{
    public int IntValueField;

    public string StaticText => "Hello world!";

    public string StringValue { get; set; }

    public FakeClass RecursivelyProperty { get; set; }

    public ArrayElement[] ArrayProperty { get; set; }
}