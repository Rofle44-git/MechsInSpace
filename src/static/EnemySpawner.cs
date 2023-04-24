using Godot;

public partial class EnemySpawner : Node {
    [Export(PropertyHint.NodePathValidTypes, "Node2D")] NodePath Player;
    [Export] int StartWave = 1;
    [Export] int EndWave = -1;
    int Wave;

    public override void _Ready() {
        Wave = StartWave;
    }
}
