using Godot;

public partial class MainMenu : Control {
	[Export] bool OverrideDebugMenu = false;

	public void _OnStartPressed() {
		GetTree().ChangeSceneToPacked(ContentManager.GameScene);
	}
	public void _OnSettingsPressed() {
		GetTree().ChangeSceneToPacked(ContentManager.SettingsScene);
	}
	public void _OnAboutPressed() {
		GetTree().ChangeSceneToPacked(ContentManager.AboutScene);
	}
}
