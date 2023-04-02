using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public Transform pipeSpawnPos;

    [Header("Difficulty Metrics")]
    public float startInterval = 2f;
    public float minInterval = 1f;
    public float intervalDecreaseRate = 0.1f;
    public bool spawnOnStart = true;

    private float interval;
    private float timeSinceLastSpawn;

    float pipeRanY = 3.5f;
    bool canSpawnPipes = true;
    public bool canDamage = true;

    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopSPawningPipes;
        Actions.OnInvulnerable += PreventDamage;
        Actions.OnVulnerable += AllowDamage;


    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopSPawningPipes;
        Actions.OnInvulnerable -= PreventDamage;
        Actions.OnVulnerable -= AllowDamage;



    }

    public static PipeSpawner Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        interval = startInterval;
        timeSinceLastSpawn = 0f;

        if (spawnOnStart)
        {
            Spawn();
        }
    }

    void Update()
    {
        if(!canSpawnPipes) { return; }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= interval)
        {
            Spawn();
            timeSinceLastSpawn = 0f;
        }

        // Decrease interval over time
        interval = Mathf.Max(minInterval, interval - (intervalDecreaseRate * Time.deltaTime));
    }


    private void Spawn()
    {
        float ranY = Random.Range(-pipeRanY, pipeRanY);
        Vector3 pipeRanSpawn = new Vector3(pipeSpawnPos.position.x, ranY, pipeSpawnPos.position.z);
        Instantiate(pipe, pipeRanSpawn, Quaternion.identity);
    }

    void StopSPawningPipes()
    {
        canSpawnPipes = false;
    }


    void PreventDamage()
    {
        canDamage = false;
    }

    void AllowDamage()
    {
        canDamage = true;
    }
}
