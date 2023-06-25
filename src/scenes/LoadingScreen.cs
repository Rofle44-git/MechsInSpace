using Godot;
using System.Threading.Tasks;

public partial class LoadingScreen : Label {
    private string CurrentFile;
	private Godot.Collections.Dictionary Content = new Godot.Collections.Dictionary();
	private PackedScene MainMenu;
	private PackedScene SettingsScene;
	private PackedScene AboutScene;
	private PackedScene GameScene;
	private PackedScene Coin;
	private PackedScene Shockwave;
	private AudioStreamOggVorbis EnemyDeath0;
    private bool IsLoading;

    public override void _Ready() {
        StartLoadingContent();
    }

    public override void _Process(double _delta) {
        Text = CurrentFile;
    }

    private async void StartLoadingContent() {
        IsLoading = true;

        await Task.Run(async () => {
            await LoadContent();
        });

		ContentManager.Content = Content;
		ContentManager.MainMenu = MainMenu;
		ContentManager.GameScene = GameScene;
		ContentManager.SettingsScene = SettingsScene;
		ContentManager.AboutScene = AboutScene;
		ContentManager.Coin = Coin;
        ContentManager.Shockwave = Shockwave;
		ContentManager.EnemyDeath0 = EnemyDeath0;

		GetTree().ChangeSceneToPacked(ContentManager.MainMenu);
    }

    private async Task LoadContent() {
        foreach (string contentType in DirAccess.GetDirectoriesAt("res://content")) {
            foreach (string file in DirAccess.GetFilesAt("res://content".PathJoin(contentType))) {
                CurrentFile = "Loading: " + contentType + "." + file;
                ContentManager.Content.Add(contentType + "." + file.TrimSuffix(".tscn"), ResourceLoader.Load("res://content".PathJoin(contentType).PathJoin(file)));
                await Task.Delay(1);
            }
        }

		CurrentFile = "Loading: game.tscn";
		MainMenu = ResourceLoader.Load<PackedScene>("res://src/scenes/main_menu.tscn");
        CurrentFile = "Loading: settings_menu.tscn";
		SettingsScene = ResourceLoader.Load<PackedScene>("res://src/scenes/settings_menu.tscn");
        CurrentFile = "Loading: about_menu.tscn";
		AboutScene = ResourceLoader.Load<PackedScene>("res://src/scenes/about_menu.tscn");
		CurrentFile = "Wow your HDD is slow.";
		GameScene = ResourceLoader.Load<PackedScene>("res://src/scenes/game.tscn");
		CurrentFile = "Loading: coin.tscn";
		Coin = ResourceLoader.Load<PackedScene>("res://src/objects/coin.tscn");
        CurrentFile = "Loading: shockwave.tscn";
        Shockwave = ResourceLoader.Load<PackedScene>("res://src/objects/shockwave.tscn");
		CurrentFile = "Loading: enemy_death0.ogg";
		EnemyDeath0 = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets-raw/sounds/enemy_death0.ogg");

        CurrentFile = "Done!";
        IsLoading = false;
    }
}
