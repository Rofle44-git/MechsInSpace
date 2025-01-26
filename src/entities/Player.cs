using Godot;

public partial class Player : Entity
{
	[Export] public ShakeableCamera2D Camera;
	[Export] PackedScene CurrentBullet;
	[Export] PackedScene DeathEffect;
	[Export] ushort FramesPerHealthRegeneration;
	[ExportSubgroup("Dependencies")]
	[Export] AudioStreamPlayer2D ShotSFX;
	[Export] AudioStreamPlayer2D HitSFX;
	[Export] AudioStreamPlayer2D DeathSFX;
	[Export] ProgressBar Health1;
	[Export] ProgressBar Health2;
	[Export] Shaker HealthShaker;
	[Export] Shaker SpriteShaker;
	[Export] Marker2D BulletSpawn;
	Vector2 HalfScreenSize;
	Vector2 CameraPan;
	KinematicCollision2D Collision;
	Node Collider;

	public override void _Ready()
	{
		Health1.Value = Health2.Value = HP;
		Health1.MaxValue = Health2.MaxValue = MaxHP;
		HalfScreenSize = GetViewportRect().Size / 2;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (!Global.IsPlayerAlive) return;
		if (!Input.IsActionJustPressed((StringName)"shoot")) return;
		ShotSFX.Play();
		AddSibling(Bullet.Spawn(CurrentBullet, GlobalPosition, GlobalRotation));
	}

	public override void _Process(double delta)
	{
		Health2.Value = Mathf.Lerp(Health2.Value, HP, 6.0f * delta);
		if (!Global.IsPlayerAlive) return;
		CameraPan = (GetViewport().GetMousePosition() - HalfScreenSize).Normalized() * 60;
		Rotation = Mathf.Atan2(CameraPan.Y, CameraPan.X);
		Camera.Pan = Camera.Offset.Lerp(CameraPan, (float)(7.2f * delta));
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!Global.IsPlayerAlive) return;
		Camera.Zoom = Vector2.One + Vector2.One * Global.Tensity * 0.5f;

		Velocity = Velocity.Lerp(Input.GetVector((StringName)"ui_left", (StringName)"ui_right", (StringName)"ui_up", (StringName)"ui_down") * Speed, (float)(6.0f * delta));
		MoveAndSlide();

		if (Engine.GetPhysicsFrames() % FramesPerHealthRegeneration == 0) Heal(1);

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			Collision = GetSlideCollision(i);
			if (Collision == null) { continue; }
			Collider = (Node)Collision.GetCollider();

			if (Collider is Enemy enemy)
			{
				Harm(enemy.Damage); Collider.QueueFree();
			}
		}
	}

	public override void OnHeal()
	{
		base.OnHeal();
		Health1.Value = HP;
		Global.HealthScale = HP / MaxHP;
	}

	public override void OnHarm()
	{
		base.OnHarm();
		HitSFX.Play();
		HealthShaker.Start();
		SpriteShaker.Start();
		Camera.Shake();
		Health1.Value = HP;
		Global.HealthScale = HP / MaxHP;
	}

	public override void OnDeath()
	{
		base.OnDeath();
		HealthShaker.Start();
		SpriteShaker.Start();
		Health1.Value = HP;
		Global.HealthScale = HP / MaxHP;
		Global.IsPlayerAlive = false;
		DeathSFX.Play();
		Global.SpawnShockwave(GetGlobalTransformWithCanvas().Origin);
		AddSibling(Effect.Spawn(DeathEffect, GlobalPosition));
		Camera.Shake(2);
		EmitSignal(SignalName.Death);
	}
}
