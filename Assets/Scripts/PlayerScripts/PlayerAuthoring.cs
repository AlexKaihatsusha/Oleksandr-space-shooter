using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject ProjectilePrefab;
    public float ProjectileLifeTime = 3f;
    class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            //since our player is movable then use TransformUsageFlags.Dynamic
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            
            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed
            });
            
            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);
            
            AddComponent(playerEntity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
            });
            
            AddComponent(playerEntity, new ProjectileLifeTime
            {
                Value = authoring.ProjectileLifeTime
            });
            
        }
    }
}
