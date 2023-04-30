using Godot;
using System;

public partial class LifetimeComponent : Node
{
	[Export] int LifetimeOverride = 0;  // If 0, will use the global maximum lifetime
	Timer LifetimeTimer;

	public override void _Ready() {
		LifetimeTimer = new Timer();
		LifetimeTimer.WaitTime = (LifetimeOverride == 0) ? Global.MaxLifetime : LifetimeOverride;
		LifetimeTimer.Timeout += () => OnLifetimeTimerTimeout();
		LifetimeTimer.Start();
	}

    private void OnLifetimeTimerTimeout() {
		GetParent().QueueFree();
    }
}
