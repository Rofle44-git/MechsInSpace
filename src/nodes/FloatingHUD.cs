using Godot;

public partial class FloatingHUD : Control
{
	private Vector2 Offset;
	private Node2D Parent;

	public override void _Ready()
	{
		Offset = Position;
		Parent = GetParent<Node2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = GlobalPosition.Lerp(Parent.Position + Offset, (float)(20.0f * delta));
	}
}
