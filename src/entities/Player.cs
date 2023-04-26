using Godot;
using System;

public partial class Player : CharacterBody2D {
	const int Speed = 300;
	int Health;
	Vector2 HalfScreenSize;
	Vector2 CameraOffset;
	Camera2D Camera;
	KinematicCollision2D LatestCollision;
	GodotObject LatestCollider;
	Marker2D BulletSpawn;
	PackedScene CurrentBullet;
	float ReloadTime;
	bool AllowShooting = true;
	ProgressBar Health1;
	ProgressBar Health2;
	Shaker HealthShaker;
	[Export] PackedScene StarterBullet;
	[Export] int StartHealth;
	[Export] int FramesPerHealthRegeneration;

	public override void _Ready() {
		Camera = GetNode<Camera2D>("Camera2D");
		BulletSpawn = GetNode<Marker2D>("BulletSpawn");
		Health1 = GetNode<ProgressBar>("Control/Bottom");
		Health2 = GetNode<ProgressBar>("Control/Top");
		HealthShaker = GetNode<Shaker>("Control/Shaker");
		Health = StartHealth;
		Health1.Value = Health;
		Health2.Value = Health;
		HalfScreenSize = GetViewportRect().Size/2;
		SetBullet(StarterBullet);
	}

	public override async void _UnhandledInput(InputEvent @event) {
		if (@event is InputEventMouseButton) {
			switch (((InputEventMouseButton)@event).ButtonIndex) {
				case MouseButton.Left:
					if (AllowShooting) {
						Bullet BulletInstance = CurrentBullet.Instantiate<Bullet>();
						BulletInstance.GlobalPosition = BulletSpawn.GlobalPosition;
						BulletInstance.GlobalRotation = BulletSpawn.GlobalRotation;
						AddSibling(BulletInstance);
						AllowShooting = false;
						await ToSignal(GetTree().CreateTimer(ReloadTime), "timeout");
						AllowShooting = true;
					}
					break;
			}
		}
	}

	public override void _Process(double delta) {
		CameraOffset = (GetViewport().GetMousePosition()-HalfScreenSize).Normalized()*60;
		Rotation = Mathf.Atan2(CameraOffset.Y, CameraOffset.X);
		Camera.Offset = Camera.Offset.Lerp(CameraOffset, 0.12f);

		Health2.Value = Mathf.Lerp(Health2.Value, Health, 2.4f*delta);
	}

	public override void _PhysicsProcess(double delta) {
		Velocity = Velocity.Lerp(Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * Speed, 0.1f);
		MoveAndSlide();

		LatestCollision = GetLastSlideCollision();
		if (LatestCollision != null) {
			LatestCollider = LatestCollision.GetCollider();
			if (LatestCollider is Enemy) {
				((Enemy)LatestCollider).SelfDestruct();
				Damage(((Enemy)LatestCollider).Damage);
			}
		}
	}

	void SetBullet(PackedScene newBullet) {
		Bullet bulletInstance = newBullet.Instantiate<Bullet>();
		CurrentBullet = newBullet;
		ReloadTime = 1.0f/bulletInstance.ShotsPerSecond;
	}

	void Damage(int amount) {
		Health -= amount;
		HealthShaker.Start();
		Health1.Value = Health;
	}

	void Heal(int amount) {
		Health += 1;
	}
}