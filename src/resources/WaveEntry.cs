using Godot;

public partial class WaveEntry : Resource {
	[Export] public PackedScene TargetEnemy;
	[ExportSubgroup("Quantity")]
	[Export(PropertyHint.Range, "-16,16,0.1")] double StartPopulation = 0.6;
	[Export(PropertyHint.Range, "0,16,0.1")] double LinearGrowth = 0.4;
	[Export(PropertyHint.Range, "0,2,0.01")] double ExponentialGrowth = 1.7;
	int CachedEnemyCount = 0;
	bool IsSetUp = false;
	string EnemyName = "";

	public void Setup() {
		EnemyName = TargetEnemy.Instantiate().Name;
		IsSetUp = true;
	}

	public int GetEnemyCount(int wave) {
		CachedEnemyCount = (int)(Mathf.Pow((double)wave, ExponentialGrowth)*LinearGrowth+StartPopulation);
		return CachedEnemyCount;
	}

	// <summary> Must be called only after GetEnemyCount() for accurate results </summary>
	public string GetEnemyCountString() {
		if (!IsSetUp) Setup();
		return EnemyName + "(" + Mathf.Clamp(CachedEnemyCount, 0, int.MaxValue).ToString() + ")";
	}
	
	/* public override void _PhysicsProcess(double delta) {
		if (Enabled && IsInstanceValid(Global.Player)) {
			if (SpawnQueue > 0) {
				SpawnQueue --;
				Instance = TargetEnemy.Instantiate<Node2D>();
				Instance.Position = new Vector2(1, 0).Rotated((float)GD.RandRange(0, Mathf.Tau))*(float)GD.RandRange(MinRange, MaxRange) + Global.Player.Position;
				Enemies.AddChild(Instance);
			}
			else if (Enemies.GetChildCount() == 0) {
				// TODO: Timer and effect for next wave transition
				NextWave();
			}
		}
	} */

	/* private void NextWave() {
		Wave ++;
		SpawnQueue = GetEnemyCount(Wave);
		GD.PrintRich("Spawning [b]", EnemyName, "(", SpawnQueue, "[/b])");
	} */

}
