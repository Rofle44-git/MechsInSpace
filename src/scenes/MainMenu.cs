using Godot;

public partial class MainMenu : Control
{
	[Export] bool OverrideDebugMenu = false;

	public void OnStartPressed() { GetTree().ChangeSceneToPacked(ContentManager.GameScene); }
	public void OnSettingsPressed() { GetTree().ChangeSceneToPacked(ContentManager.SettingsScene); }
	public void OnAboutPressed() { GetTree().ChangeSceneToPacked(ContentManager.AboutScene); }
}
