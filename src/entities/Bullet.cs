using Godot;

public partial class Bullet : CharacterBody2D
{
	[Export(PropertyHint.Range, "0,30,0.1,radians")] public float Inaccuracy = 0.02f;
	[Export(PropertyHint.Range, "0,1000,1")] public int Damage = 20;
	[Export(PropertyHint.Range, "0,10000,100")] public int MaxDistance = 3000;
	[ExportSubgroup("Effects")]
	[Export] PackedScene ShootEffect;
	[Export] PackedScene HitEffect;
	[ExportSubgroup("SFX")]
	[Export] public AudioStreamRandomizer ShotSFXs;
	[Export] bool ConstantSFX;
	public Vector2 HitPos;
	private bool Expired;

	public static Bullet Spawn(PackedScene bullet, Vector2 position, float rotation)
	{
        Bullet newBullet = bullet.Instantiate<Bullet>();
		newBullet.Position = position;
		newBullet.Rotation = rotation;
		return newBullet;
    }

	public void Despawn()
	{
		AddSibling(Effect.Spawn(HitEffect, HitPos, Rotation - Mathf.Pi));
		QueueFree();
	}

	public override void _EnterTree()
	{
		if (ShootEffect == null) return;
		AddSibling(Effect.Spawn(ShootEffect, GlobalPosition, GlobalRotation));
	}
}