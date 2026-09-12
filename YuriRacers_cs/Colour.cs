using System.Drawing;
using Silk.NET.Maths;

internal record struct Colour(float r, float g, float b, float a)
{
	public Colour(uint col) : this((byte)(col >> 8 * 3), (byte)(col >> 8 * 2),(byte)(col >> 8 * 1),(byte)col) {}
	public static Colour White { get; } = new(0xFFFFFFFF);
	public static Colour DarkGrey { get; } = new(0x222222FF);
	public static implicit operator Vector4D<float>(Colour c) => new(c.r, c.g, c.b, c.a);
	
}