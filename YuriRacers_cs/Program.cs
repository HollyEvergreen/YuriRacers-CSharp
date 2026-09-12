using System.Diagnostics;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Yui;
using YuriEngine;

new PixelRacers(true).run();

internal class PixelRacers(bool debug = false) : App(debug)
{
	float[] verts =
	[
	//   X,     Y,    Z,    r  g  b  U,    V
		-1.0f,  1.0f, 0.0f, 1, 0, 0, 1.0f, 0.0f, // 0: Top-Left
		-1.0f, -1.0f, 0.0f, 0, 1, 0, 1.0f, 0.0f, // 1: Bottom-Left
		 0.5f, -1.0f, 0.0f, 0, 0, 1, 0.0f, 0.0f, // 2: Bottom-Right
		 1.0f,  1.0f, 0.0f, 1, 0, 0, 1.0f, 0.0f // 3: Top-Right
	];

	readonly int [] indices = [ 
		0, 1, 3, 
		3, 1, 2
	];

	private uint vao;
	private uint vbo;
	private uint ebo;

	protected override void Start()
	{
	}
	protected override void OnLoad()
	{
		Debug.Assert(OGL.gl != null, nameof(OGL.gl) + " != null");
		ShaderRegistry.__init__(OGL.gl, fs);
		OGL.gl.ClearColor<float>(Colour.DarkGrey);
		vao = OGL.gl.GenVertexArray();
		vbo = OGL.gl.GenBuffer();
		ebo = OGL.gl.GenBuffer();
		OGL.gl.BindVertexArray(vao);
		OGL.gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
		OGL.gl.BufferData(BufferTargetARB.ArrayBuffer, verts, BufferUsageARB.StaticDraw);
		OGL.VertexAttributePointer<float>(0, 3, 3+3+2, 0);
		OGL.VertexAttributePointer<float>(1, 3, 3+3+2, 3);
		OGL.VertexAttributePointer<float>(2, 2, 3+3+2,  5);
		OGL.gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
		OGL.gl.BufferData(BufferTargetARB.ElementArrayBuffer, indices, BufferUsageARB.StaticDraw);
		OGL.gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		OGL.gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
		OGL.gl.BindVertexArray(0);
	}
	protected override void Update(double dt) { }
	protected override void Render(double dt)
	{
		Debug.Assert(OGL.gl != null, nameof(OGL.gl) + " != null");
		OGL.ClearFramebuffer();
		ShaderRegistry.Use("fb-blit");
		OGL.DrawVAO(vao,ebo, 6);
		ShaderRegistry.Stop();
		_window.SwapBuffers();
	}
}