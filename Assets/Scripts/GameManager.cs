using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score=0;

    public GameObject ballPrefab;
    public TextMeshProUGUI scoreLabel;

    private Entity ballEntityPrefab;
    private EntityManager entityManager;
    private BlobAssetStore blobAssetStore;

    private void Awake()
    {
        // singleton pattern
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // initialize stuffs to convert game object to entity
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);

    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        Entity newBallEntity = entityManager.Instantiate(ballEntityPrefab);

        Translation ballTranslationData = new Translation
        {
            Value = new Unity.Mathematics.float3(0f, 5f, 0f)
        };
        entityManager.AddComponentData(newBallEntity, ballTranslationData);
    }

    public void IncreaseScore()
    {
        score++;
        scoreLabel.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
