using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class RotateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Assign values to local variables captured in your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        float deltaTime = Time.DeltaTime;

        // This declares a new kind of job, which is a unit of work to do.
        // The job is declared as an Entities.ForEach with the target components as parameters,
        // meaning it will process all entities in the world that have both
        // Translation and Rotation components. Change it to process the component
        // types you want.



        Entities.ForEach((ref Rotation rotation, in RotateSpeedData speedData) => {
            rotation.Value = math.mul(rotation.Value, quaternion.RotateX(math.radians(speedData.rotateSpeed * deltaTime)));
            rotation.Value = math.mul(rotation.Value, quaternion.RotateY(math.radians(speedData.rotateSpeed * deltaTime)));
            rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(speedData.rotateSpeed * deltaTime)));
        }).Schedule();
    }
}
