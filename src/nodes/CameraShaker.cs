using Godot;

public partial class CameraShaker : Node {
	public float StrengthMultiplier;

	public CameraShaker(float strengthMultiplier) {
		StrengthMultiplier = strengthMultiplier;
	}

	public override void _Ready() {
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Sine)
			.TweenProperty(this, "StrengthMultiplier", 0f, 0.7f)
			.Finished += () => QueueFree();
	}
}