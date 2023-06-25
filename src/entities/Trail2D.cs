using Godot;

public partial class Trail2D : Line2D {
	[Export] Node2D Target;
	[Export(PropertyHint.Range, "0,4,0.025")] float Lifetime = 0.6f;
	[Export] bool Enabled = true;
	int FramesPerPoint;

	public override void _Ready() {
      ClearPoints();
		FramesPerPoint = ConfigManager.FramesPerPoint;
	}

	public override void _Process(double delta) {
		if (Engine.GetFramesDrawn() % FramesPerPoint == 0) {
			while (GetPointCount()>(int)(Lifetime/delta/FramesPerPoint)) RemovePoint(0);
			if (Enabled) AddPoint(Target.GlobalPosition);
		}
	}
}
