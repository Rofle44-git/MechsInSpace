using Godot;

public partial class Game : Node2D {
	[Export] public Player Player;
	[Export] public HUD HUD;
	[Export] AudioStreamPlayer BGMus;
	[Export] PauseMenu PauseMenu;
	[Export] Node2D Vignette;
	float MusicPitchShift = 0;

	public override void _Ready() {
		Global.Game = this;
		Global.Player = Player;
		Global.HUD = HUD;
		HUD.UpdateCoinCounter();
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsActionPressed("pause") && Global.IsPlayerAlive) {
			PauseMenu.Toggle();
		}
	}

	public override void _PhysicsProcess(double delta) {
		Global.Tensity = (float)Mathf.Lerp(Global.Tensity, (1f-Global.HealthScale), 0.01);
		if (Global.IsPlayerAlive) {
			BGMus.PitchScale = 0.2f*Global.Tensity+1f;
			Vignette.SelfModulate = new Color(1f, 1f, 1f, 0.8f*Global.Tensity);
			return;
		}
		BGMus.PitchScale = (float)Mathf.Lerp(BGMus.PitchScale, 0f, 0.01);
	}
}
