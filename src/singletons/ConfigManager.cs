using Godot;

public partial class ConfigManager : Node {
	const string ConfigLocation = "user://config.cfg";
	public static ConfigFile File = new ConfigFile();
	public static int FramesPerPoint = 2;

	public override void _Ready() {
		Load();
	}

	public static void Load() {
		if (File.Load(ConfigLocation) != Error.Ok) File.Save(ConfigLocation);
		FramesPerPoint = (int)File.GetValue("Settings", "FramesPerPoint", FramesPerPoint);
	}

	public static void Save() {
		File.SetValue("Settings", "FramesPerPoint", FramesPerPoint);
		File.Save(ConfigLocation);
	}
}