using Godot;

public partial class SaveManager : Node {
	const string SaveLocation = "user://save.cfg";
	public static ConfigFile File = new ConfigFile();
	public static int Coins = 0;

	public override void _Ready() {
		Load();
	}

	public static void Load() {
		if (File.Load(SaveLocation) != Error.Ok) File.Save(SaveLocation);
		Coins = (int)File.GetValue("Collectables", "Coins", Coins);
	}

	public static void Save() {
		File.SetValue("Collectables", "Coins", Coins);
		File.Save(SaveLocation);
	}
}