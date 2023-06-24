using Godot;

public partial class EnemySpawner : Node {
    [Export] bool Enabled = true;
    [Export] Node2D Enemies;
    [Export] PackedScene TargetEnemy;
    [ExportSubgroup("Quantity")]
    [Export(PropertyHint.Range, "-16,16,0.1")] double StartPopulation = 0.6;
    [Export(PropertyHint.Range, "0,16,0.1")] double LinearGrowth = 0.4;
    [Export(PropertyHint.Range, "0,2,0.01")] double ExponentialGrowth = 1.7;
    [ExportSubgroup("Range")]
    [Export] float MinRange = 570.0f;
    [Export] float MaxRange = 1000.0f;
    int Wave = 0;
    int SpawnQueue = 0;
    Node2D Instance;

    public override void _PhysicsProcess(double delta) {
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
    }

    private void NextWave() {
        Wave ++;
        SpawnQueue = GetEnemyCount(Wave);
        GD.Print("Spawning ", Enemies.Name, "(", SpawnQueue, ")");
    }

    public int GetEnemyCount(int wave) {
        return (int)(Mathf.Pow((double)wave, ExponentialGrowth)*LinearGrowth+StartPopulation);
    }
}
