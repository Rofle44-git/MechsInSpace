using Godot;
using System;

public partial class Trail2D : Line2D {
	[Export(PropertyHint.Range, "0,1,0.025")] float Lifetime = 0.6f;
	[Export] bool Enabled = true;

	public override void _Ready() {
		// Clears points used as a preview
        ClearPoints();
	}

	public override void _Process(double delta) {
		if (Engine.GetFramesDrawn() % Config.FramesPerPoint == 0) {
			if (GetPointCount()	> (int)(Lifetime/delta/Config.FramesPerPoint)) RemovePoint(-1);
			if (Enabled) AddPoint(GlobalPosition);
		}
	}
}
