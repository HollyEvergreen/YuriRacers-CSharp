using YuriEngine;

class App : YuriEngine.App
{
	protected override void Start() { }
	protected override void OnLoad() { }
	protected override void Update(double dt) { }
	protected override void Render(double dt) { }
}

internal static class Program{
	public static int Main(string[] Args)
	{
		var app = new App();
		app.run();
		return 0;
	}
}
