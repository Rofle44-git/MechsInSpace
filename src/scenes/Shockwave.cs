using Godot;

public partial class Shockwave : ColorRect {
	ShaderMaterial Shader;
	public Vector2 TargetPosition = new Vector2(500, 500);

	public override void _Ready() {
		Shader = ((ShaderMaterial)Material);
		Shader.SetShaderParameter("center", new Vector2(
			TargetPosition.X/DisplayServer.WindowGetSize().X,
			TargetPosition.Y/DisplayServer.WindowGetSize().Y
		));
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Sine)
			.TweenMethod(new Callable(this, "SetShockwaveRadius"), 0.0f, 1.0f, 0.7)
			.Finished += () => QueueFree();
	}

	private void SetShockwaveRadius(float radius) {
		Shader.SetShaderParameter("radius", radius);
	}
}
