using Godot;

public partial class AnimatedWaveDisplay : Control
{
	[Export] Label AnimatedLabel;
	int ScreendWidth;
	double TweenProgress = 0.0;

	public override void _Ready()
	{
		OnResized();
		Resized += OnResized;
	}

	public override void _Process(double delta)
	{
		AnimatedLabel.Position = new((float)(ScreendWidth - ScreendWidth * TweenProgress * 2), 0f);
	}

	public void DisplayNextWave(int wave)
	{
		AnimatedLabel.Text = "Wave " + wave.ToString();
		TweenProgress = 0.0;
		CreateTween().SetEase(Tween.EaseType.OutIn)
				.SetTrans(Tween.TransitionType.Expo)
				.TweenProperty(this, (NodePath)"TweenProgress", 1.0, 1.2);
	}

	private void OnResized()
	{
		ScreendWidth = DisplayServer.WindowGetSize().X;
		AnimatedLabel.SetDeferred((StringName)"size", new Vector2(ScreendWidth, 64f));
	}
}
