using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class PickupSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem bufferSystem;
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        bufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate()
    {
        this.Dependency = OnUpdate(this.Dependency);
    }

    private JobHandle OnUpdate(JobHandle inputDeps)
    {
        TriggerJob triggerJob = new TriggerJob
        {
            speedEntities = GetComponentDataFromEntity<SpeedData>(),
            entitiesToDelete = GetComponentDataFromEntity<DeleteTag>(),
            commandBuffer = bufferSystem.CreateCommandBuffer()
        };

        return triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
    }

    private struct TriggerJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<SpeedData> speedEntities;
        [ReadOnly] public ComponentDataFromEntity<DeleteTag> entitiesToDelete;
        public EntityCommandBuffer commandBuffer;
        public void Execute(TriggerEvent triggerEvent)
        {
            TestEntityTrigger(triggerEvent.EntityA, triggerEvent.EntityB);
            TestEntityTrigger(triggerEvent.EntityB, triggerEvent.EntityA);
        }

        private void TestEntityTrigger(Entity entity1, Entity entity2)
        {
            if(speedEntities.HasComponent(entity1))
            {
                if (entitiesToDelete.HasComponent(entity2)) return;
                commandBuffer.AddComponent(entity2, new DeleteTag());
            }
        }
    }
}
