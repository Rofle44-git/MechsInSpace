using Godot;

public partial class Enemy : CharacterBody2D {
	[Export] public int Damage = 10;
	Node2D Player;
	const int Speed = 250;
	Vector2 DeltaPos;

	public override void _Ready() {
		Player = Global.Player;
	}

	public override void _PhysicsProcess(double delta) {
		if (!IsInstanceValid(Player)) return;
		DeltaPos = Player.Position-Position;
		Rotation = Mathf.Atan2(DeltaPos.Y, DeltaPos.X);
		Velocity = Speed * new Vector2(1.0f, 0.0f).Rotated(Rotation);
		MoveAndSlide();
	}

	public void SelfDestruct() {
		QueueFree();
	}
}
