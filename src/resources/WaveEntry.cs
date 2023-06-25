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
}
