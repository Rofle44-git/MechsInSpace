using Godot;

public partial class Player : Entity {
	const int Speed = 300;
	[Export] PackedScene StarterBullet;
	[Export] ushort FramesPerHealthRegeneration;
	[Export] AudioStreamPlayer2D ShotSFX;
	[Export] AudioStreamPlayer2D HitSFX;
	Vector2 HalfScreenSize;
	Vector2 CameraOffset;
	Camera2D Camera;
	KinematicCollision2D Collision;
	Node Collider;
	Marker2D BulletSpawn;
	PackedScene CurrentBullet;
	ProgressBar Health1;
	ProgressBar Health2;
	Shaker HealthShaker;
	Shaker SpriteShaker;

	public override void _Ready() {
		Health1 = GetNode<ProgressBar>("FloatingHUD/ShakeContainer/Top");
		Health2 = GetNode<ProgressBar>("FloatingHUD/ShakeContainer/Bottom");
		Health1.Value = Health2.Value = HP;
		Health1.MaxValue = Health2.MaxValue = MaxHP;

		HealthShaker = GetNode<Shaker>("FloatingHUD/ShakeContainer/Shaker");
		SpriteShaker = GetNode<Shaker>("Sprite2D/Shaker");
		
		Camera = GetNode<Camera2D>("Camera2D");
		HalfScreenSize = GetViewportRect().Size/2;

		BulletSpawn = GetNode<Marker2D>("BulletSpawn");
		SetBullet(StarterBullet);
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (!Input.IsActionJustPressed("shoot")) return;
		ShotSFX.Play();
		Bullet BulletInstance = CurrentBullet.Instantiate<Bullet>();
		BulletInstance.GlobalPosition = BulletSpawn.GlobalPosition;
		BulletInstance.GlobalRotation = BulletSpawn.GlobalRotation;
		AddSibling(BulletInstance);
	}

	public override void _Process(double delta) {
		CameraOffset = (GetViewport().GetMousePosition()-HalfScreenSize).Normalized()*60;
		Rotation = Mathf.Atan2(CameraOffset.Y, CameraOffset.X);
		Camera.Offset = Camera.Offset.Lerp(CameraOffset, (float)(7.2f*delta));

		Health2.Value = Mathf.Lerp(Health2.Value, HP, 6.0f*delta);
	}

	public override void _PhysicsProcess(double delta) {	
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
		Health1.Value = HP;
		Global.HealthScale = HP/(float)MaxHP;
	}

	public override void _OnDeath() {
		Global.PlayerAlive = false;
		base._Die();
	}
}
