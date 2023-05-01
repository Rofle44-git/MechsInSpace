using Godot;

public partial class Bullet : CharacterBody2D {
    [Export(PropertyHint.Range, "0,30,0.1,radians")] public float Inaccuracy = 0.02f;
    [Export(PropertyHint.Range, "0,1000,1")] public int Damage = 20;
    [Export(PropertyHint.Range, "1,60,1")] public int ShotsPerSecond = 5;
    [Export(PropertyHint.Range, "0,10000,100")] public int MaxDistance = 3000;
    [ExportSubgroup("Effects")]
    [Export] PackedScene ShootEffect;
    [Export] PackedScene HitEffect;
    public Vector2 HitPos;
    private bool Expired;

    public override void _Ready() {
        GetNode<LifetimeComponent>("LifetimeComponent").Connect("Expire", Callable.From(OnExpired));
    }

    public override void _EnterTree() {
        if (ShootEffect == null) return;
        Node2D Effect = ShootEffect.Instantiate<Node2D>();
        Effect.GlobalPosition = GlobalPosition;
        Effect.GlobalRotation = GlobalRotation;
        AddSibling(Effect);
    }

    public override void _ExitTree() {
        if (HitEffect == null || Expired) return;
        Node2D Effect = HitEffect.Instantiate<Node2D>();
        Effect.GlobalPosition = HitPos;
        AddSibling(Effect);
    }

    private void OnExpired() {
        Expired = true;
        QueueFree();
    }
}