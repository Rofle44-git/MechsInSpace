using Godot;
using System.Threading.Tasks;

public partial class LoadingScreen : Label
{
	private Godot.Collections.Dictionary Content = [];
	private PackedScene MainMenu;
	private PackedScene SettingsScene;
	private PackedScene AboutScene;
	private PackedScene GameScene;
	private PackedScene Coin;
	private PackedScene Shockwave;
	private AudioStreamOggVorbis EnemyDeath0;
	private bool IsLoading;

	public override void _Ready()
	{
		ConfigManager.Load();
		SaveManager.Load();
		StartLoadingContent();
	}

	private async void StartLoadingContent()
	{
		IsLoading = true;

		await Task.Run(LoadContent);

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

	private async Task LoadContent()
	{
		foreach (string contentType in DirAccess.GetDirectoriesAt("res://content"))
		{
			foreach (string file in DirAccess.GetFilesAt("res://content".PathJoin(contentType)))
			{
				ContentManager.Content.Add(contentType + "." + file.TrimSuffix(".tscn"), ResourceLoader.Load("res://content".PathJoin(contentType).PathJoin(file)));
				await Task.Delay(1);
			}
		}

		MainMenu = ResourceLoader.Load<PackedScene>("res://src/scenes/main_menu.tscn");
		SettingsScene = ResourceLoader.Load<PackedScene>("res://src/scenes/settings_menu.tscn");
		AboutScene = ResourceLoader.Load<PackedScene>("res://src/scenes/about_menu.tscn");
		GameScene = ResourceLoader.Load<PackedScene>("res://src/scenes/game.tscn");
		Coin = ResourceLoader.Load<PackedScene>("res://src/objects/coin.tscn");
		Shockwave = ResourceLoader.Load<PackedScene>("res://src/objects/shockwave.tscn");
		EnemyDeath0 = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets-raw/sounds/enemy_death0.ogg");

		IsLoading = false;
	}
}
