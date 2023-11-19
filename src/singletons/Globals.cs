using Godot;

public partial class Global : Node {
	public static Vector2 CoinGoalPos = new Vector2(986, 13);
	public static Game Game;
	public static Player Player;
	public static HUD HUD;
	public static int MaxLifetime = 3;
	public static int Coins = 0;
	public static float HealthScale = 1.0f;
	public static float Tensity = 0.0f;
	public static bool IsPlayerAlive = true;

	public static void SpawnCoin(int amount, Vector2 position) {
		for (int i = 0; i < amount; i++) {
			Node2D coin = ContentManager.Coin.Instantiate<Node2D>();
			coin.GlobalPosition = position;
			Game.HUD.CoinsContainer.AddChild(coin);
		}
	}

	public static void SpawnShockwave(Vector2 position) {
		Shockwave shockwave = ContentManager.Shockwave.Instantiate<Shockwave>();
		shockwave.TargetPosition = position;
		HUD.Shockwaves.AddChild(shockwave);
	}

	public static void ShakeCamera(int level) {
		Player.Camera.Shake(level);
	}

	public static void PlaySoundAt(Vector2 globalPosition, AudioStreamOggVorbis sound) {
        AudioStreamPlayer2D tempAudioPlayer = new AudioStreamPlayer2D {
            GlobalPosition = globalPosition,
            Stream = sound,
            Autoplay = true
        };
        Game.AddChild(tempAudioPlayer);
		tempAudioPlayer.Finished += () => tempAudioPlayer.QueueFree();
	}

	public static void _OnCoinCollected() {
		Coins ++; HUD.UpdateCoinCounter();
	}
}