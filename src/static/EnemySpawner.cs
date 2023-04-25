using Godot;

public partial class EnemySpawner : Node {
    [Export] Node2D Enemies;
    [Export] PackedScene TargetEnemy;
    [ExportSubgroup("Quantity")]
    [Export(PropertyHint.Range, "1,16,0.1")] float StartPopulation = 0.6f;
    [Export(PropertyHint.Range, "0,16,0.5")] float LinearGrowth = 0.6f;
    [Export(PropertyHint.Range, "0,2,0.01")] float ExponentialGrowth = 1.8f;
    [ExportSubgroup("Range")]
    [Export] float MinRange = 570.0f;
    [Export] float MaxRange = 1000.0f;
    int Wave = 0;
    int SpawnQueue = 0;
    Node2D Instance;

    public override void _PhysicsProcess(double delta) {
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

    private void NextWave() {
        Wave ++;
        SpawnQueue = (int)(Mathf.Pow((float)Wave, ExponentialGrowth)*LinearGrowth+StartPopulation);
    }
}
