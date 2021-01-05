﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
[UpdateAfter(typeof(PickupSystem))]
public class DeleteEntitySystem : SystemBase
{
    protected override void OnUpdate()
    {
        this.Dependency = OnUpdate(this.Dependency);
    }
    protected JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.TempJob);

        Entities.WithAll<DeleteTag>().ForEach((Entity entity) =>
        {
            commandBuffer.DestroyEntity(entity);
        }).Run();

        commandBuffer.Playback(EntityManager);
        commandBuffer.Dispose();

        return default;
    }
}