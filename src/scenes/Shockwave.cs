using Godot;

public partial class Shockwave : ColorRect
{
	ShaderMaterial Shader;
	public Vector2 TargetPosition = new(500f, 500f);

	public override void _Ready()
	{
		Shader = (ShaderMaterial)Material;
		Shader.SetShaderParameter((StringName)"center", new Vector2(
			TargetPosition.X / DisplayServer.WindowGetSize().X,
			TargetPosition.Y / DisplayServer.WindowGetSize().Y
		));
		CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Sine)
			.TweenMethod(new Callable(this, (StringName)"SetShockwaveRadius"), 0.0f, 1.0f, 0.7)
			.Finished += QueueFree;
	}

	private void SetShockwaveRadius(float radius)
	{
		Shader.SetShaderParameter((StringName)"radius", radius);
	}
}
