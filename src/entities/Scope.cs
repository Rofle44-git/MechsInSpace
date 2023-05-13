using Godot;
using System;

public partial class Scope : Node2D {
	[Export] Color ScopeColor = Color.FromHsv(0.0f, 1.0f, 1.0f, 0.4f);
	[Export] Node2D From;

	public override void _Draw() {
		DrawLine(From.GlobalPosition, GetGlobalMousePosition(), ScopeColor, 4.0f, true);
	}

    public override void _Process(double delta) {
		QueueRedraw();
	}
}
