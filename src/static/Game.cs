using Godot;
using System;

public partial class Game : Node2D {
	[Export] Node2D Player;

	public override void _Ready() {
		Global.Player = Player;
	}
}
