using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
       
    }
    public void OnDestroy(ref SystemState state)
    {
        
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ( var(spawner,waveData) in SystemAPI.Query<RefRW<Spawner>,RefRW<WaveData>>())
        {
            HandleWave(ref state, spawner, waveData);
        }
    }

    private void HandleWave(ref SystemState state,  RefRW<Spawner> spawner, RefRW<WaveData> waveData)
    {
        
        var currentTime = SystemAPI.Time.ElapsedTime;
        if (waveData.ValueRO.WaveIsStarted)
        {
            waveData.ValueRW.CurrentWaveTime += SystemAPI.Time.DeltaTime;
            if (waveData.ValueRO.CurrentWaveTime >= waveData.ValueRO.WaveDuration)
            {
                waveData.ValueRW.WaveIsStarted = false;
                waveData.ValueRW.NextStartTime =waveData.ValueRO.TimeBetweenWaves + (float)currentTime;
            }
            else
            {
                Spawn(ref state, spawner);
            }
        }
        else
        {
            if (currentTime >= waveData.ValueRO.NextStartTime)
            {
                waveData.ValueRW.WaveIsStarted = true;
                waveData.ValueRW.CurrentWaveTime = 0f;
            }
        }
    }
    private void Spawn(ref SystemState state, RefRW<Spawner> spawner)
    {
        if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            //instantiate the entity
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            //declare new position
            float3 pos = new float3(Random.Range(spawner.ValueRO.MinX, spawner.ValueRO.MaxX), spawner.ValueRO.SpawnPosition.y, 0f);
            //set data(in our case just position) to newEntity
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
           
            //update next spawn time
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        }
    }
}
