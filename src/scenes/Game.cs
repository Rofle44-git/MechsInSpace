using Godot;

public partial class Game : Node2D {
	[Export] public Player Player;
	[Export] public HUD HUD;
	[Export] private AudioStreamPlayer BGMus;
	[Export] private Node2D Vignette;
	float MusicPitchShift = 0;

	public override void _Ready() {
		Global.Game = this;
		Global.Player = Player;
		Global.HUD = HUD;
	}

    public override void _PhysicsProcess(double delta) {
        Global.Tensity = (float)Mathf.Lerp(Global.Tensity, (1f-Global.HealthScale), 0.01);
		if (Global.PlayerAlive) {
			BGMus.PitchScale = 0.2f*Global.Tensity+1f;
			Vignette.SelfModulate = new Color(1f, 1f, 1f, 0.8f*Global.Tensity);
			return;
		}
		BGMus.PitchScale = (float)Mathf.Lerp(BGMus.PitchScale, 0f, 0.01);
    }
}
