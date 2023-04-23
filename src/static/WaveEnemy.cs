using Godot;
using System;

public partial class WaveEnemy : Resource {
    [Export] public Enemy TargetEnemy;
    [ExportGroup("Quantity")]
    [Export] public int StartPopulation;
    [Export] public int LinearGrowth;
    [Export] public float ExponentialGrowth;
    [ExportGroup("Wave")]
    [Export] public int StartWave;
    [Export] public int NWave;
    [Export] public int EndWave;

    public WaveEnemy() {
        TargetEnemy = new Enemy();
        StartPopulation = 1;
        LinearGrowth = 2;
        ExponentialGrowth = 1.2f;
        StartWave = 1;
        NWave = 1;
        EndWave = -1;
    }

    public bool CanSpawn(int currentWave) {
        return (currentWave >= StartWave && currentWave < EndWave && currentWave % NWave == 0);
    }

    public int GetSpawnCount(int currentWave) {
        return (int)(
            MathF.Round(StartPopulation*MathF.Pow((int)ExponentialGrowth,(int)currentWave)*LinearGrowth)
        );
    }
}