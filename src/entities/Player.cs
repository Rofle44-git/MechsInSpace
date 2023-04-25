using Godot;

public partial class Player : CharacterBody2D {
	[Export]
	PackedScene StarterBullet;
	const int Speed = 300;
	const int CameraOffset = 60;
	Vector2 HalfScreenSize;
	Camera2D Camera;
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
		Vector2 mousePos = (GetViewport().GetMousePosition()-HalfScreenSize).Normalized()*CameraOffset;
		Rotation = Mathf.Atan2(mousePos.Y, mousePos.X);
		Camera.Offset = Camera.Offset.Lerp(mousePos, 0.1f);
	}

	public override void _PhysicsProcess(double delta) {
		Velocity = Velocity.Lerp(Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * Speed, 0.1f);
		MoveAndSlide();
	}

	public void SetBullet(PackedScene newBullet) {
		Bullet bulletInstance = newBullet.Instantiate<Bullet>();
		CurrentBullet = newBullet;
		ReloadTime = 1.0f/bulletInstance.ShotsPerSecond;
	}

	void SpawnProcess() {
		
	}
}

// TODO: make camera move further, relative to the distance from mouse to screen center