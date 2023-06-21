using Godot;

public partial class Global : Node {
    public static PackedScene Coin = ResourceLoader.Load<PackedScene>("res://src/entities/coin.tscn");
    public static Vector2 CoinGoalPos = new Vector2(986, 13);
    public static Game Game;
    public static Player Player;
    public static HUD HUD;
    public static int MaxLifetime = 3;
    public static int Coins = 0;

    public static void SpawnCoin(int amount, Vector2 position) {
		for (int i = 0; i < amount; i++) {
            Node2D coin = Coin.Instantiate<Node2D>();
            coin.GlobalPosition = position;
            Game.HUD.CoinsContainer.AddChild(coin);
        }
    }

    public static void _OnCoinCollected() {
        Coins ++; HUD.UpdateCoinCounter();
    }
}