using Godot;

public partial class Enemy : Entity {
	[Export] public int Damage = 10;
	[Export] private int Loot = 10;
	[Export] private PackedScene DeathEffect;
	Node2D Player;
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

	public override void _OnDeath() {
		Global.PlaySoundAt(GlobalPosition, ContentManager.EnemyDeath0);
		Node2D effect = DeathEffect.Instantiate<Node2D>();
		effect.GlobalPosition = GlobalPosition;
		AddSibling(effect);
		Global.SpawnCoin(Loot, GetGlobalTransformWithCanvas().Origin);
		base._Die();
	}
}
