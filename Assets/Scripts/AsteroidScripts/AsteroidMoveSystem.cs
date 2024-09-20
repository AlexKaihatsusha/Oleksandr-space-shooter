using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;



public partial struct AsteroidMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AsteroidTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, AsteroidMoveSpeed>())
        {
            transform.ValueRW.Position += -transform.ValueRO.Up() * moveSpeed.Value * deltaTime;
        }
    }
}
