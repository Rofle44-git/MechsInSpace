using Godot;

public partial class CameraShaker(float strengthMultiplier) : Node
{
	public float StrengthMultiplier = strengthMultiplier;

	public override void _Ready()
	{
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Sine)
			.TweenProperty(this, "StrengthMultiplier", 0.0f, 0.7f)
			.Finished += QueueFree;
	}
}