using Godot;

public partial class Player : Entity {
	[Export] public ShakeableCamera2D Camera;
	[Export] PackedScene StarterBullet;
	[Export] PackedScene DeathEffect;
	[Export] ushort FramesPerHealthRegeneration;
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
	PackedScene CurrentBullet;

	public override void _Ready() {
		Health1.Value = Health2.Value = HP;
		Health1.MaxValue = Health2.MaxValue = MaxHP;
		HalfScreenSize = GetViewportRect().Size/2;
		SetBullet(StarterBullet);
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (!Global.IsPlayerAlive) return;
		if (!Input.IsActionJustPressed("shoot")) return;
		ShotSFX.Play();
		Bullet BulletInstance = CurrentBullet.Instantiate<Bullet>();
		BulletInstance.GlobalPosition = BulletSpawn.GlobalPosition;
		BulletInstance.GlobalRotation = BulletSpawn.GlobalRotation;
		AddSibling(BulletInstance);
	}

	public override void _Process(double delta) {
		Health2.Value = Mathf.Lerp(Health2.Value, HP, 6.0f*delta);
		if (!Global.IsPlayerAlive) return;
		CameraPan = (GetViewport().GetMousePosition()-HalfScreenSize).Normalized()*60;
		Rotation = Mathf.Atan2(CameraPan.Y, CameraPan.X);
		Camera.Pan = Camera.Offset.Lerp(CameraPan, (float)(7.2f*delta));
	}

	public override void _PhysicsProcess(double delta) {
		if (!Global.IsPlayerAlive) return;
		Camera.Zoom = Vector2.One+Vector2.One*Global.Tensity*0.5f;

		Velocity = Velocity.Lerp(Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * Speed, (float)(6.0f*delta));
		MoveAndSlide();

		if (Engine.GetPhysicsFrames() % FramesPerHealthRegeneration == 0) Heal(1);
	
		for (int i = 0; i < GetSlideCollisionCount(); i++) {
			Collision = GetSlideCollision(i);
			if (Collision == null) {continue;}
			Collider = (Node)Collision.GetCollider();
			
			if (Collider is Enemy) {
				Harm(((Enemy)Collider).Damage); Collider.QueueFree();
			}
		}
	}

	void SetBullet(PackedScene newBullet) {
		Bullet bulletInstance = newBullet.Instantiate<Bullet>();
		CurrentBullet = newBullet;
		ShotSFX.Stream = bulletInstance.ShotSFXs;
	}

	public override void _OnHeal() {
		Health1.Value = HP;
		Global.HealthScale = HP/(float)MaxHP;
	}

	public override void _OnHarm() {
		HitSFX.Play();
		HealthShaker.Start();
		SpriteShaker.Start();
		Camera.Shake();
		Health1.Value = HP;
		Global.HealthScale = HP/(float)MaxHP;
	}

	public override void _OnDeath() {
		HealthShaker.Start();
		SpriteShaker.Start();
		Health1.Value = HP;
		Global.HealthScale = HP/(float)MaxHP;
		Global.IsPlayerAlive = false;
		DeathSFX.Play();
		Global.SpawnShockwave(GetGlobalTransformWithCanvas().Origin);
		Effect effect = DeathEffect.Instantiate<Effect>();
		effect.GlobalPosition = GlobalPosition;
		AddSibling(effect);
		Camera.Shake(2);
		EmitSignal(SignalName.OnDeath);
	}
}
