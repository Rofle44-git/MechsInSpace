using Godot;

public partial class AnimatedButton : Button
{
	[Export] Vector2 ExpandAmount = new(64f, 0f);
	Vector2 OriginalSize;

	public override void _Ready()
	{
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		OriginalSize = Size;
	}

	private void OnMouseEntered() { GetDefaultTween().TweenProperty(this, (NodePath)"size", OriginalSize + ExpandAmount, 0.3); }

	private void OnMouseExited() { GetDefaultTween().TweenProperty(this, "size", OriginalSize, 0.3); }

	private Tween GetDefaultTween()
	{
		Tween expandTween = CreateTween();
		expandTween.SetEase(Tween.EaseType.Out)
				.SetTrans(Tween.TransitionType.Back);
		return expandTween;
	}
}
