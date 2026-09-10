public interface IMass
{
    public float scale { get; init; } 
    protected float Value { get; set; }
    public Kilogram ToBase()
    {
        return scale * Value;
    }
}
public struct Kilogram(float Kg) : IMass
{
    public float scale { get; init; } = 1;
    public float Value { get; set; } = Kg;
    public Kilogram ToBase()
    {
        return Value;
    }
    public static implicit operator float(Kilogram kg)
    {
        return kg.ToBase().Value;
    }
    public static implicit operator Kilogram(float _kg)
    {
        return new(_kg);
    }

    public override string ToString()
    {
        return $"{ToBase()} KG";
    }
}

public struct Gram(float g) : IMass
{
    public float scale { get; init; } = 0.001f;
    public float Value { get; set; }
    public Kilogram ToBase()
    {
        return Value * scale;
    }
    public override string ToString()
    {
        return $"{ToBase().Value} g";
    }
}

public struct Tonne(float tonne) : IMass
{
    public float scale { get; init; } = 1000;
    public float Value { get => field * scale; set => field = value * scale; } = tonne;
    public Kilogram ToBase()
    {
        return Value * scale;
    }
    public override string ToString()
    {
        return $"{ToBase().Value} T";
    }
}

public struct GenericMass(float m, float scale, string units = "") : IMass
{
    public readonly string units = units;
    public float scale { get; init; } = scale;
    public float Value { get; set; } = m;
    public Kilogram ToBase()
    {
        return Value * scale;
    }
    public override string ToString()
    {
        return $"{ToBase().Value} {units}";
    }
}

public record struct PhysicsMaterial()
{
    public Kilogram mass_kg = 0;
};

public interface IWheel;
public interface IEngine;

public abstract class IVehicle
{
    public readonly List<IWheel> _wheels = [];
    public readonly IEngine _engine = null!;
    // public 
}

