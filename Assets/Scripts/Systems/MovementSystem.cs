using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class MovementSystem : SystemBase
{
    private Gyroscope m_Gyro;

    protected override void OnCreate()
    {
        if (!Application.isMobilePlatform) return;


        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float2 moveInput;

        if (!Application.isMobilePlatform)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
        }
        else
        {
            moveInput.x = -m_Gyro.attitude.x * 2.5f;
            moveInput.y = -m_Gyro.attitude.y * 2.5f;
        }

        Entities.ForEach((ref PhysicsVelocity vel, in SpeedData speedData) =>
        {
            vel.Linear.xz += moveInput * speedData.speed * deltaTime;
        }).Schedule();
    }
}
