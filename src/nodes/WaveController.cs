using Godot;
using System.Threading.Tasks;

public partial class WaveController : Node
{
	[Signal] public delegate void WaveStartingEventHandler(int wave);
	[Export] bool Enabled = true;
	[Export] Node2D Enemies;
	[Export] Godot.Collections.Array<WaveEntry> WaveEntries = [];
	[ExportSubgroup("Spawn Range")]
	[Export] float MinRange = 570.0f;
	[Export] float MaxRange = 1000.0f;
	Godot.Collections.Array<Enemy> SpawnQueue = [];
	Enemy Instance;
	int Wave;
	int QueueIndex;
	bool Queueing = false;

	public override void _PhysicsProcess(double delta)
	{
		// If we're disabled OR the player is dead
		if (!Enabled || !Global.IsPlayerAlive) return;
		// If the queue is still full
		if (SpawnQueue.Count > 0)
		{
			QueueIndex = GD.RandRange(0, SpawnQueue.Count - 1);
			Instance = SpawnQueue[QueueIndex];
			Instance.Position = new Vector2(1, 0).Rotated((float)GD.RandRange(0, Mathf.Tau)) * (float)GD.RandRange(MinRange, MaxRange) + Global.Player.Position;
			Enemies.AddChild(Instance);
			SpawnQueue.RemoveAt(QueueIndex);
		}
		// If no enemies present AND we're not already queueing
		else if (Enemies.GetChildCount() == 0 && !Queueing)
		{
			Wave++;
			StartGeneratingWaveQueue(Wave);
			EmitSignal(SignalName.WaveStarting, Wave);
		}
	}

	private async void StartGeneratingWaveQueue(int wave)
	{
		Queueing = true;
		SpawnQueue = await GenerateWaveQueue(wave);
		Queueing = false;
	}

	private async Task<Godot.Collections.Array<Enemy>> GenerateWaveQueue(int wave)
	{
		Godot.Collections.Array<Enemy> queue = [];
		foreach (WaveEntry waveEntry in WaveEntries)
		{
			for (int i = 0; i < waveEntry.GetEnemyCount(wave); i++)
			{
				queue.Add(waveEntry.TargetEnemy.Instantiate<Enemy>());
			}
			await Task.Delay(1);
		}
		return queue;
	}
}
