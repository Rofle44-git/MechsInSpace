using Godot;

public partial class SaveManager : Node {
	const string SaveLocation = "user://save.cfg";
	public static ConfigFile File = new ConfigFile();

	public static void Load() {
		if (File.Load(SaveLocation) != Error.Ok) File.Save(SaveLocation);
		Global.Coins = (int)File.GetValue("Collectables", "Coins", Global.Coins);
	}

	public static void Save() {
		File.SetValue("Collectables", "Coins", Global.Coins);
		File.Save(SaveLocation);
	}
}