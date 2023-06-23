using Godot;

public partial class Global : Node {
    public static PackedScene Coin = ResourceLoader.Load<PackedScene>("res://src/entities/coin.tscn");
    public static Vector2 CoinGoalPos = new Vector2(986, 13);
    public static Game Game;
    public static Player Player;
    public static HUD HUD;
    public static int MaxLifetime = 3;
    public static int Coins = 0;
    public static Godot.Collections.Dictionary Content = new Godot.Collections.Dictionary();
    public static AudioStreamOggVorbis EnemyDeath0 = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/sounds/enemy_death0.ogg");

    public static void LoadContent() {
        GD.PrintRich("Scanning [b]content[/b]");
        foreach (string contentType in DirAccess.GetDirectoriesAt("res://content")) {
            GD.PrintRich("Registering [b]", contentType, "[/b]");
            foreach (string file in DirAccess.GetFilesAt("res://content".PathJoin(contentType))) {
                GD.PrintRich("Found [b]", contentType + "." + file, "[/b]");
                Content.Add(contentType + "." + file.TrimSuffix(".tscn"), ResourceLoader.Load("res://content".PathJoin(contentType).PathJoin(file)));
            }
        }
    }

    public static void SpawnCoin(int amount, Vector2 position) {
		for (int i = 0; i < amount; i++) {
            Node2D coin = Coin.Instantiate<Node2D>();
            coin.GlobalPosition = position;
            Game.HUD.CoinsContainer.AddChild(coin);
        }
    }

    public static void PlaySoundAt(Vector2 globalPosition, AudioStreamOggVorbis sound) {
        AudioStreamPlayer2D tempAudioPlayer = new AudioStreamPlayer2D();
        tempAudioPlayer.GlobalPosition = globalPosition;
        tempAudioPlayer.Stream = sound;
        tempAudioPlayer.Autoplay = true;
        Game.AddChild(tempAudioPlayer);
        tempAudioPlayer.Finished += () => tempAudioPlayer.QueueFree();
    }

    public static void _OnCoinCollected() {
        Coins ++; HUD.UpdateCoinCounter();
    }
}