using Godot;
using System;

public partial class Player : CharacterBody2D {
	[Export] PackedScene StarterBullet;
	const int Speed = 300;
	Vector2 HalfScreenSize;
	Vector2 CameraOffset;
	Camera2D Camera;
	KinematicCollision2D LatestCollision;
	GodotObject LatestCollider;
	Marker2D BulletSpawn;
	PackedScene CurrentBullet;
	float ReloadTime;
	bool AllowShooting = true;

	public override void _Ready() {
		HalfScreenSize = GetViewportRect().Size/2;
		Camera = GetNode<Camera2D>("Camera2D");
		BulletSpawn = GetNode<Marker2D>("BulletSpawn");
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
	}

	public override void _PhysicsProcess(double delta) {
		Velocity = Velocity.Lerp(Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * Speed, 0.1f);
		MoveAndSlide();

		LatestCollision = GetLastSlideCollision();
		LatestCollider = LatestCollision.GetCollider();
		if (LatestCollision != null) {
			if (LatestCollider is Enemy) {
				((Enemy)LatestCollider).SelfDestruct();
			}
		}
	}

	public void SetBullet(PackedScene newBullet) {
		Bullet bulletInstance = newBullet.Instantiate<Bullet>();
		CurrentBullet = newBullet;
		ReloadTime = 1.0f/bulletInstance.ShotsPerSecond;
	}
}