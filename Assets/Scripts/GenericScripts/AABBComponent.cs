using Unity.Entities;
using Unity.Mathematics;


public struct AABBComponent : IComponentData
{
    public AABB Value;
}
