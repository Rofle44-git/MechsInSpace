using Godot;

public partial class LifetimeComponent : Node {
	[Signal] public delegate void ExpireEventHandler();
	[Export] int LifetimeOverride = 0;
	Timer LifetimeTimer;

    public override void _Ready() {
		Expire += () => GetParent<Node>().QueueFree();
		LifetimeTimer = new Timer();
		LifetimeTimer.WaitTime = (LifetimeOverride == 0) ? Global.MaxLifetime : LifetimeOverride;
		AddChild(LifetimeTimer);
		LifetimeTimer.Timeout += () => EmitSignal("Expire");
		LifetimeTimer.Start();
    }
}
