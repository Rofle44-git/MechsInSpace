using Godot;
using System.Threading.Tasks;

public partial class WaveController : Node {
	[Signal] public delegate void WaveStartingEventHandler(int wave);
	[Export] bool Enabled = true;
	[Export] Node2D Enemies;
	[Export] Godot.Collections.Array<WaveEntry> WaveEntries = new Godot.Collections.Array<WaveEntry>();
	[ExportSubgroup("Spawn Range")]
	[Export] float MinRange = 570.0f;
	[Export] float MaxRange = 1000.0f;
	Godot.Collections.Array<Enemy> SpawnQueue = new Godot.Collections.Array<Enemy>();
	Enemy Instance;
	int Wave;
	int QueueIndex;
	bool Queueing = false;

	public override void _PhysicsProcess(double delta) {
		if (!Enabled || !Global.IsPlayerAlive) {return;}
		// If queue is still full
		if (SpawnQueue.Count > 0) {
			QueueIndex = (int)GD.RandRange(0, SpawnQueue.Count-1);
			Instance = SpawnQueue[QueueIndex];
			Instance.Position = new Vector2(1, 0).Rotated((float)GD.RandRange(0, Mathf.Tau))*(float)GD.RandRange(MinRange, MaxRange) + Global.Player.Position;
			Enemies.AddChild(Instance);
			SpawnQueue.RemoveAt(QueueIndex);
		}
		else if (Enemies.GetChildCount() == 0 && !Queueing) {
			// Wave finished
			GD.Print("Wave ", Wave, " completed");			
			Wave ++;
			StartGeneratingWaveQueue(Wave);
			EmitSignal(SignalName.WaveStarting, Wave);
		}
	}

	private async void StartGeneratingWaveQueue(int wave) {
		Queueing = true;
		GD.Print("Queueing enemy waves...");
		SpawnQueue = await GenerateWaveQueue(wave);
		GD.Print("Finished queueing enemy waves");
		Queueing = false;
	}

	private async Task<Godot.Collections.Array<Enemy>> GenerateWaveQueue(int wave) {
		Godot.Collections.Array<Enemy> queue = new Godot.Collections.Array<Enemy>();
		foreach (WaveEntry waveEntry in WaveEntries) {
			for (int i = 0; i < waveEntry.GetEnemyCount(wave); i++) {
				queue.Add(waveEntry.TargetEnemy.Instantiate<Enemy>());
			}
			GD.Print("Queued ", waveEntry.GetEnemyCountString());
			await Task.Delay(1);
		}
		return queue;
	}
}
