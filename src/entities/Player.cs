using Godot;
using System;

public partial class Player : CharacterBody2D {
	const int Speed = 300;
	int Health;
	Vector2 HalfScreenSize;
	Vector2 CameraOffset;
	Camera2D Camera;
	KinematicCollision2D Collision;
	GodotObject Collider;
	Marker2D BulletSpawn;
	PackedScene CurrentBullet;
	float ReloadTime;
	bool AllowShooting = true;
	ProgressBar Health1;
	ProgressBar Health2;
	Shaker HealthShaker;
	Shaker SpriteShaker;
	[Export] PackedScene StarterBullet;
	[Export] int MaxHealth;
	[Export] ulong FramesPerHealthRegeneration;

	public override void _Ready() {
		Health = MaxHealth;
		Health1 = GetNode<ProgressBar>("FloatingHUD/ShakeContainer/Top");
		Health2 = GetNode<ProgressBar>("FloatingHUD/ShakeContainer/Bottom");
		Health1.Value = Health2.Value = Health;
		Health1.MaxValue = Health2.MaxValue = MaxHealth;
		HealthShaker = GetNode<Shaker>("FloatingHUD/ShakeContainer/Shaker");
		SpriteShaker = GetNode<Shaker>("Sprite2D/Shaker");
		Camera = GetNode<Camera2D>("Camera2D");
		HalfScreenSize = GetViewportRect().Size/2;
		BulletSpawn = GetNode<Marker2D>("BulletSpawn");
		SetBullet(StarterBullet);
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (!Input.IsActionJustPressed("shoot")) return;
		Bullet BulletInstance = CurrentBullet.Instantiate<Bullet>();
		BulletInstance.GlobalPosition = BulletSpawn.GlobalPosition;
		BulletInstance.GlobalRotation = BulletSpawn.GlobalRotation;
		AddSibling(BulletInstance);
				AllowShooting = false;
				await ToSignal(GetTree().CreateTimer(ReloadTime), "timeout");
				AllowShooting = true;
				break;
			} break;
		}
	}

	public override void _Process(double delta) {
		CameraOffset = (GetViewport().GetMousePosition()-HalfScreenSize).Normalized()*60;
		Rotation = Mathf.Atan2(CameraOffset.Y, CameraOffset.X);
		Camera.Offset = Camera.Offset.Lerp(CameraOffset, (float)(7.2f*delta));

		Health2.Value = Mathf.Lerp(Health2.Value, Health, 6.0f*delta);
	}

	public override void _PhysicsProcess(double delta) {	
		Velocity = Velocity.Lerp(Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * Speed, (float)(6.0f*delta));
		MoveAndSlide();

		if (Engine.GetPhysicsFrames() % FramesPerHealthRegeneration == 0) Heal(1);
	
		for (int i = 0; i < GetSlideCollisionCount(); i++) {
			Collision = GetSlideCollision(i);
			if (Collision != null) {
				Collider = Collision.GetCollider();
				if (Collider is Enemy) {
					((Enemy)Collider).SelfDestruct();
					Damage(((Enemy)Collider).Damage);
				}
			}
		}
	}

	void SetBullet(PackedScene newBullet) {
		Bullet bulletInstance = newBullet.Instantiate<Bullet>();
		CurrentBullet = newBullet;
		ReloadTime = 1.0f/bulletInstance.ShotsPerSecond;
	}

	void Damage(int amount) {
		if (Health+amount <= 0) Die();
		Health -= amount;
		HealthShaker.Start();
		SpriteShaker.Start();
		Health1.Value = Health;
	}

	void Heal(int amount) {
		if (Health+amount > MaxHealth) return;
		Health += amount;
		Health1.Value = Health;
	}
	
	void Die() {
		QueueFree();
		// TODO: Add neat effects
	}
}
