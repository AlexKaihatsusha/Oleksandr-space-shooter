using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Rendering;
using UnityEngine;

[UpdateAfter(typeof(TransformSystemGroup))]
[BurstCompile]
public partial struct AABBMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (aabb, transform, shapeSize,entity) in SystemAPI.Query<RefRW<AABBComponent>, RefRO<LocalTransform>, RefRO<ShapeSize>>().WithEntityAccess())
        {
            var newAABBData = new AABB
            {
                Center = transform.ValueRO.Position,
                Extents = new float3(shapeSize.ValueRO.sizeX,shapeSize.ValueRO.sizeY, 0f)
            };
            state.EntityManager.SetComponentData(entity,new AABBComponent
            {
                Value = newAABBData
            });
        }
    }
}
