using Godot;

public partial class menu : Node {
	[Export] ColorRect BlackFade;
	[Export] Control MenuNode;

	public override void _Ready() {
		MenuNode.Hide();
		MenuNode.Position = new Vector2(0, DisplayServer.ScreenGetSize().Y);
	}

	public void Appear() {
		MenuNode.Show();

		CreateTween()
			.SetTrans(Tween.TransitionType.Back)
			.SetEase(Tween.EaseType.Out)
			.TweenProperty(MenuNode, "position", Vector2.Zero, 0.4);
		
		CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out)
			.TweenProperty(MenuNode, "color", new Color(0, 0, 0, 0.4f), 0.4);
	}

	public async void Disappear() {
		Tween tween = CreateTween();
		tween
			.SetTrans(Tween.TransitionType.Back)
			.SetEase(Tween.EaseType.In)
			.TweenProperty(MenuNode, "position", new Vector2(0, DisplayServer.ScreenGetSize().Y), 0.4);
		
		CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.In)
			.TweenProperty(MenuNode, "color", new Color(0, 0, 0, 0), 0.4);
		
		await ToSignal(tween, "finished");
	}
}
