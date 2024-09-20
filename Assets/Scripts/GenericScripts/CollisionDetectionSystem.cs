using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
//want to update after we made a move
[UpdateAfter(typeof(TransformSystemGroup))]
[BurstCompile]
public partial struct CollisionDetectionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        //so we will not run if projectileTag does not exist
        state.RequireForUpdate<ProjectileTag>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        
        foreach (var (aabbA,tagA, entityA) in SystemAPI.Query<AABBComponent, ProjectileTag>().WithEntityAccess() )
        {
            foreach (var (aabbB,tagB,entityB) in SystemAPI.Query<AABBComponent, AsteroidTag>().WithEntityAccess())
            {
                if (AABBOverlapCheck(aabbA.Value,aabbB.Value))
                {
                    Debug.Log("COLLISION DETECTION  ");
                    ecb.DestroyEntity(entityA);
                    ecb.DestroyEntity(entityB);
                }
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

    [BurstCompile]
    bool AABBOverlapCheck(AABB a, AABB b)
    {
        float3 aMin = a.Center - a.Extents;
        float3 aMax = a.Center + a.Extents;
        float3 bMin = b.Center - b.Extents;
        float3 bMax = b.Center + b.Extents;
        
        return aMin.x <= bMax.x && aMax.x >= bMin.x && aMin.y <= bMax.y && aMax.y >= bMin.y;
    }
    
}
