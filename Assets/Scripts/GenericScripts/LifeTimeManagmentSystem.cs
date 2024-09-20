using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;

public partial struct LifeTimeManagmentSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LifeTime>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        new LifeJob
        {
            ecb = ecb,
            DeltaTime = deltaTime
        }.Schedule();
        
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

public partial struct LifeJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public float DeltaTime;

    public void Execute(Entity entity,ref LifeTime lifeTime)
    {
        lifeTime.Value -= DeltaTime;
        if (lifeTime.Value <= 0)
        {
            ecb.AddComponent<IsDestroying>(entity);
        }
    }
}
