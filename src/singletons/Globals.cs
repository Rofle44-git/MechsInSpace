using Godot;

public partial class Global : Node {
    public delegate void CoinPickedUp();

    public static PackedScene Coin = ResourceLoader.Load<PackedScene>("res://src/entities/coin.tscn");
    public static Node2D Player;
    public static int MaxLifetime = 3;
    public static int Coins = 0;
}