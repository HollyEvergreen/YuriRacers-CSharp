using System.Diagnostics;
using Silk.NET.OpenGL;
using Yui;

namespace YuriEngine;

public static class ShaderRegistry
{
	public static void __init__(GL _gl, Filesystem _fs)
	{
		OGL.gl = _gl;
		fs = _fs;
		fs.Foreach("/content/shaders/", "*.spv", (file) =>
		{
			Console.WriteLine($"{file.FullName}, {file.Name.Split('.')[0]}");
			LoadFromFile(file);
		} );
		
	}
	private static Filesystem fs = null!;
	private static readonly Dictionary<string, Shader> register = [];
	// private static readonly Dictionary<string, string> cached = [];
	public static bool TryAdd(string Name, Shader Shader) => register.TryAdd(Name, Shader);
	public static void Use(string Name) => register[Name].Use();
	public static void Stop() => OGL.gl?.UseProgram(0);
	public static unsafe void Unload(string Name)
	{
	}
	private static void LoadFromFile(FileInfo File) => register[File.Name.Split('.')[0]] = new Shader(File.Name, File.FullName);
}

public class Shader : IDisposable
{
	// public static unsafe Shader FromCache(GL OGL.gl, string name, string path)
	// {
		// Shader __shader = new(OGL.gl, OGL.gl.CreateProgram());
		// var reader = new BinaryReader(File.OpenRead(path));
		
		// OGL.gl.ProgramBinary(__shader.ID, (GLEnum)reader.ReadInt32(), reader.ReadExactly());
		// return __shader;
	// }
	// public Shader(GL OGL.gl, uint Id)
	// {
		// this.OGL.gl = OGL.gl;
		// id = Id;
	// }
	public unsafe Shader( string Name, string path)
	{
		name = Name;
		Debug.Assert(OGL.gl != null);
		var shaders = OGL.LoadShaderBinary(path);
		OGL.SetEntryPoint(shaders[0], "vertexMain");
		OGL.SetEntryPoint(shaders[1], "fragmentMain");
		id = OGL.CreateProgram(shaders);
		ShaderRegistry.TryAdd(Name, this);
	}
	private readonly uint id;
	public readonly string name;
	public uint ID => id;

	public void Use() => OGL.gl?.UseProgram(id);
	public void Stop() => OGL.gl?.UseProgram(0);
	public void Dispose()
	{
		#if DEBUG
		if (App.debug) Console.WriteLine("disposing shader "+name); 
		#endif
		OGL.gl?.DeleteProgram(id);
		GC.SuppressFinalize(this);
	}
}