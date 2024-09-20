using Unity.Entities;
using Unity.Mathematics;
public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float2 SpawnPosition;
    public float MaxX;
    public float MinX;
    public float NextSpawnTime;
    public float SpawnRate;
    //public Random Random;
}

public struct WaveData : IComponentData
{
    public bool WaveIsStarted;
    public float WaveDuration;
    public float TimeBetweenWaves;
    public float CurrentWaveTime;
    public float NextStartTime;
}