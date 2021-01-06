using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Entity ballEntity;
    public Unity.Mathematics.float3 offet;

    private EntityManager entityManager;

    private void Awake()
    {
        // singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ballEntity == null) return;

        Translation ballPosition = entityManager.GetComponentData<Translation>(ballEntity);
        transform.position = ballPosition.Value + offet;
    }
}
