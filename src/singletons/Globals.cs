using Godot;

public class Global {
    public delegate void CoinPickedUp();

    public static Node2D Player;
    public static int MaxLifetime = 3;
    public static int Coins = 0;
}