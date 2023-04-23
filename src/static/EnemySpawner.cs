using Godot;

public partial class EnemySpawner : Node {
    [Export(PropertyHint.NodePathValidTypes, "Node2D")] NodePath Player;
    //[Export] WaveEnemyList Enemies = new WaveEnemyList();
    [Export] int StartWave = 1;
    [Export] int EndWave = -1;
    int Wave;

    public override void _Ready() {
        Wave = StartWave;

    //    for (int I = 1; I < 11; I++) {
    //        GD.Print("--Wave ", I, "--\nSpawns: ", GetSpawnList(I));
    //    }
    }

    //Godot.Collections.Array<Enemy> GetSpawnList(int wave) {
    //    Godot.Collections.Array<Enemy> Return = new Godot.Collections.Array<Enemy>{};
    //    foreach (WaveEnemy waveEnemy in Enemies.List) {
    //        if (waveEnemy.CanSpawn(wave)) {
    //            for (int I = 0; I < waveEnemy.GetSpawnCount(wave); I++) {
    //                Return.Add(waveEnemy.TargetEnemy);
    //            }
    //        }
    //    }
    //    return Return;
    //}
}
