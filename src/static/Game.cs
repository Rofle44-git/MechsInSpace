using Godot;

public partial class Game : Node2D {
	[Export] public Player Player;
	[Export] public HUD HUD;

	public override void _Ready() {
		Global.Game = this;
		Global.Player = Player;
		Global.HUD = HUD;
	}
}
