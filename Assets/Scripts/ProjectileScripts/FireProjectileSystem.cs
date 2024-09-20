
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;



[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]

public partial struct FireProjectileSystem : ISystem
{
    
    public void OnUpdate(ref SystemState state)
    {
        //buffer for our commands
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        foreach (var (projectilePrefab, transform, lifetime) in SystemAPI.Query<ProjectilePrefab, LocalTransform,ProjectileLifeTime>().WithAll<FireProjectileTag>())
        {
            var newProjectile = ecb.Instantiate(projectilePrefab.Value);
            var projectileTransform = LocalTransform.FromPositionRotation(transform.Position + new float3(0f,1f,0f), transform.Rotation);
            ecb.SetComponent(newProjectile, projectileTransform);  
            
            ecb.AddComponent(newProjectile, new LifeTime {Value = lifetime.Value});
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
