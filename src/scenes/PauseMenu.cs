using Godot;

public partial class PauseMenu : Control {
	public override void _Ready() {
		GetChild<Control>(0).Position = new Vector2(0, DisplayServer.WindowGetSize().Y);
	}

	public void Toggle() {
		if (GetTree().Paused) {Disappear(); return;}
		Appear();
	}

	public void Appear() {
		GetTree().Paused = true;
		Show();
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Back)
			.TweenProperty(GetChild<Control>(0), "position", new Vector2(0, 0), 0.4);
	}

	public void Disappear() {
		GetTree().Paused = false;
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Linear)
			.TweenProperty(GetChild<Control>(0), "position", new Vector2(0, DisplayServer.WindowGetSize().Y), 0.1)
			.Finished += () => Hide();;
	}

	public void Continue() {
		Disappear();
	}

	public void Quit() {
		SaveManager.Save();
		GetTree().Quit();
	}

	public void ToMainMenu() {
		SaveManager.Save();
		GetTree().Paused = false;
		GetTree().ChangeSceneToPacked(ContentManager.MainMenu);
	}
}
