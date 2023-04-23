using Godot;

public partial class Effect : Node2D {
    public override async void _Ready()
    {
        Godot.Collections.Array<Node> Parts = GetChildren();
        double Longest = ((GpuParticles2D)Parts[0]).Lifetime;
        foreach (GpuParticles2D Part in Parts) {
            Part.Emitting = true;
            Longest = (Part.Lifetime > Longest) ? Part.Lifetime : Longest;
        }
        await ToSignal(GetTree().CreateTimer(Longest), "timeout");
        QueueFree();
    }
}