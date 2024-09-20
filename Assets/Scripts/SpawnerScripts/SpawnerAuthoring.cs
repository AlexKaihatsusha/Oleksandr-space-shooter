using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoting : MonoBehaviour
{
    [Header("Spawner data")]
    public GameObject Prefab = null;
    public float SpawnRate = 0f;
    public float MaxX = 10f;
    public float MinX = 0f;
    public float2 SpawnPosition;
    [Header("Wave data")]
    public float WavesDuration;
    public float TimeBetweenWaves;
    
   

    //creating baker for spawner
    class SpawnerBaker : Baker<SpawnerAuthoting>
    {
        public override void Bake(SpawnerAuthoting authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
          
            //set default data using our sturct
            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.SpawnPosition,
                MaxX = authoring.MaxX,
                MinX = authoring.MinX,
                NextSpawnTime = 0f,
                SpawnRate = authoring.SpawnRate,
            });
            AddComponent(entity, new WaveData
            {
                WaveDuration = authoring.WavesDuration,
                TimeBetweenWaves = authoring.TimeBetweenWaves,
                CurrentWaveTime = 0f,
                NextStartTime = 0f,
            });
        }
    }
}
