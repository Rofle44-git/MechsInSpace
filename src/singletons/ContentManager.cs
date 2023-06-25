using Godot;

public partial class ContentManager : Node {
	public static Godot.Collections.Dictionary Content = new Godot.Collections.Dictionary();
	public static PackedScene MainMenu;
	public static PackedScene GameScene;
	public static PackedScene SettingsScene;
	public static PackedScene AboutScene;
	public static PackedScene Coin;
	public static PackedScene Shockwave;
	public static AudioStreamOggVorbis EnemyDeath0;
}