using Silk.NET.OpenGL;
using Yui;
new PixelRacers(true).run();

internal class PixelRacers(bool debug = false) : App(debug)
{
	protected override void Start()
	{
		Log("hello\n");
	}
	protected override void OnLoad() { }
	protected override void Update(double dt) { }
	protected override void Render(double dt) { }
}

