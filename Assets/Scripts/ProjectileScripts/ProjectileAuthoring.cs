using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float ProjectileSpeed;
    public float2 ShapeSize;
    public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new ProjectileMoveSpeed
            {
                Value = authoring.ProjectileSpeed
            });
            AddComponent(entity, new ShapeSize
            {
                sizeX = authoring.ShapeSize.x,
                sizeY = authoring.ShapeSize.y,
            });
            AddComponent(entity, new AABBComponent{Value = new AABB{Center = float3.zero, Extents = float3.zero}});
            AddComponent<ProjectileTag>(entity);
        }
    }
}
