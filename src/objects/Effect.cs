using System.Linq;
using Godot;

public partial class Effect : Node2D {
	public override async void _Ready() {
		Godot.Collections.Array<Node> Parts = GetChildren();
		double Longest = ((GpuParticles2D)Parts[0]).Lifetime;
		foreach (GpuParticles2D Part in Parts.Cast<GpuParticles2D>()) {
			Part.Emitting = true;
			Longest = (Part.Lifetime > Longest) ? Part.Lifetime : Longest;
		}
		await ToSignal(GetTree().CreateTimer(Longest), "timeout");
		QueueFree();
	}

    public static Effect Spawn(PackedScene effectScene, Vector2? position = null, float rotation = 0.0f)
    {
		position ??= Vector2.Zero;
		Effect newEffect = effectScene.Instantiate<Effect>();
		newEffect.Position = (Vector2)position;
		newEffect.Rotation = rotation;
		return newEffect;
	}
}