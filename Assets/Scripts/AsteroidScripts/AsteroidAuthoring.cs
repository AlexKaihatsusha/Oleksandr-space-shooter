using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Collider = Unity.Physics.Collider;
using SphereCollider = UnityEngine.SphereCollider;

public class AsteroidAuthoring : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float LifeTime = 3f;
    public float2 ShapeSize;
    public class AsteroidAuthoringBaker : Baker<AsteroidAuthoring>
    {
        public override void Bake(AsteroidAuthoring authoring)
        {
            //so our asteroid will move then we need to use Dynamic
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            //add speed component to asteroid
            AddComponent(entity, new AsteroidMoveSpeed
            {
                Value = authoring.MoveSpeed
            });
            
            //add tag to asteroid
            AddComponent<AsteroidTag>(entity);
            AddComponent(entity, new LifeTime
            {
                Value = authoring.LifeTime
            });
            AddComponent(entity, new ShapeSize
            {
                sizeX = authoring.ShapeSize.x,
                sizeY = authoring.ShapeSize.y,
            });
            AddComponent(entity, new AABBComponent{Value = new AABB{Center = float3.zero, Extents = float3.zero}});
        }
    }
}

