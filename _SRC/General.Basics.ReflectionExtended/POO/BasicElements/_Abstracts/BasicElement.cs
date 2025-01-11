namespace General.Basics.ReflectionExtended.POO.Abstracts;

public abstract record BasicElement
{
    public Type Type { get; }

    public BasicElement(Type type)
    {
        Validate(type);

        Type = type;
    }

    protected abstract void Validate(Type type);
}
