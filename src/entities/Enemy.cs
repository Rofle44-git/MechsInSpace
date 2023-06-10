using Godot;

public partial class Enemy : CharacterBody2D {
	const int Speed = 250;
	Node2D Player;
	Vector2 DeltaPos;
	[Export] public int Damage = 10;
	[Export] private int Health = 30;
	[Export] private int Loot = 10;
	[Export] private PackedScene DeathEffect;

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

	public void Hurt(int amount) {
		if (Health-amount <= 0) Die();
		Health -= amount;
	}

	public void Die() {
		Global.Money += Loot;
		Node2D effect = DeathEffect.Instantiate<Node2D>();
		effect.GlobalPosition = GlobalPosition;
		AddSibling(effect);
		QueueFree();
	}
}
