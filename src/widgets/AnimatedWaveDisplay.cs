using Godot;

public partial class AnimatedWaveDisplay : Control {
	[Export] Label AnimatedLabel;
	int ScreendWidth;
	double TweenProgress = 0.0;

	public override void _Ready() {
		_OnResized();
		Resized += () => _OnResized();
	}

	public override void _Process(double delta) {
		AnimatedLabel.Position = new Vector2((float)(ScreendWidth-ScreendWidth*TweenProgress*2), 0);
	}

	public void DisplayNextWave(int wave) {
		AnimatedLabel.Text = "Wave " + wave.ToString();
		TweenProgress = 0.0;
		CreateTween()
			.SetEase(Tween.EaseType.OutIn)
			.SetTrans(Tween.TransitionType.Expo)
			.TweenProperty(this, "TweenProgress", 1.0, 1.2);
	}

	private void _OnResized() {
		ScreendWidth = DisplayServer.WindowGetSize().X;
		AnimatedLabel.SetDeferred("size", new Vector2(ScreendWidth, 64));
	}
}
