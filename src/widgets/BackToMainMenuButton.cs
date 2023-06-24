using Godot;

public partial class BackToMainMenuButton : Button {
	public override void _Ready() {
		Pressed += () => GetTree().ChangeSceneToPacked(ContentManager.MainMenu);
	}
}
