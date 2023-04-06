using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private Transform pipeSpawnPos;

    [Header("Difficulty Metrics")]
    [SerializeField] private float startInterval = 2f;
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float intervalDecreaseRate = 0.1f;
    [SerializeField] private bool spawnOnStart = true;

    [SerializeField] private float startPipeRanY = 3.5f;
    [SerializeField] private float currentPipeRanY = 3.5f;
    [SerializeField] private float minCurrentPipeRanY = 0.5f;
    [SerializeField] private float pipeSpeed = 8f;
    [SerializeField] private float currentInterval;

    private bool canDamage = true;

    private float timeSinceLastSpawn;
    private bool canSpawnPipes = true;
    public bool speedUpPipes = false;
    private float ogPipeSpeed;
    private Coroutine speedUpPipesRoutine;
    private Coroutine thisRoutine;
 

    public static PipeSpawner Instance { get; private set; }


    #region Getters

    public float GetPipeSpeed()
    {
        return pipeSpeed;
    }

    public bool GetCanDamage()
    {
        return canDamage;
    }

    #endregion

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
        currentInterval = startInterval;
        timeSinceLastSpawn = 0f;

        if (spawnOnStart)
        {
            SpawnPipe();
        }

        ogPipeSpeed = pipeSpeed;

        currentPipeRanY = startPipeRanY;
    }

    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopSpawningPipes;

        Actions.OnInvulnerable += PreventDamage;
        Actions.OnVulnerable += AllowDamage;

        Actions.OnSkipStart += SpeedUpPipes;
        Actions.OnSkipStartResetValues += ResetPipesToStartValues;
        Actions.OnSkipStartEnd += SlowPipes;

    }

    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopSpawningPipes;

        Actions.OnInvulnerable -= PreventDamage;
        Actions.OnVulnerable -= AllowDamage;

        Actions.OnSkipStart -= SpeedUpPipes;
        Actions.OnSkipStartResetValues -= ResetPipesToStartValues;
        Actions.OnSkipStartEnd -= SlowPipes;

    }

    private void Update()
    {
        if (!canSpawnPipes) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentInterval)
        {
            SpawnPipe();
            timeSinceLastSpawn = 0f;
        }

        currentInterval = Mathf.Max(minInterval, currentInterval - (intervalDecreaseRate * Time.deltaTime));
        float currentIntervalDecreaseRate = intervalDecreaseRate * 0.5f;
        currentPipeRanY = Mathf.Lerp(startPipeRanY, minCurrentPipeRanY, 1f - (currentInterval - minInterval) / (startInterval - minInterval));
        currentPipeRanY = Mathf.Max(0.5f, currentPipeRanY - (currentIntervalDecreaseRate * Time.deltaTime));

        if (speedUpPipes)
        {
            currentInterval = 0.75f;
            pipeSpeed *= 4f;
            Actions.OnInvulnerable();
            speedUpPipes = false;

            if (speedUpPipesRoutine != null) StopCoroutine(speedUpPipesRoutine);
            speedUpPipesRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2f));
        }
    }

    private void SpawnPipe()
    {
        /*float ranY = Random.Range(-pipeRanY, pipeRanY);
        Vector3 pipeRanSpawn = new Vector3(pipeSpawnPos.position.x, ranY, pipeSpawnPos.position.z);
        Instantiate(pipe, pipeRanSpawn, Quaternion.identity);*/

        float ranYScale = Mathf.Clamp(1f / currentInterval, 0.5f, 1f);
        float ranY = Random.Range(-currentPipeRanY * ranYScale, currentPipeRanY * ranYScale);
        Vector3 pipeRanSpawn = new Vector3(pipeSpawnPos.position.x, ranY, pipeSpawnPos.position.z);
        Instantiate(pipe, pipeRanSpawn, Quaternion.identity);
    }

    private IEnumerator StartVulnerableTimer(float delay)
    {
        while (true)
        {
            Debug.Log("Entered");

            yield return new WaitForSeconds(delay);

            Debug.Log("Calling On Vulnerable");
            Actions.OnVulnerable();
        }
    }

    private void StopSpawningPipes()
    {
        canSpawnPipes = false;
    }

    private void PreventDamage()
    {
        canDamage = false;
    }

    private void AllowDamage()
    {
        canDamage = true;
    }

    public void SpeedUpPipes()
    {

        if (thisRoutine != null)
            StopCoroutine(thisRoutine);

        speedUpPipes = true;
        thisRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2));

    }

    void ResetPipesToStartValues()
    {
        pipeSpeed = ogPipeSpeed;
       // currentInterval = 1.2f;
    }

    void SlowPipes()
    {
        Debug.Log("Slowin Pipes");
        pipeSpeed = ogPipeSpeed;
        currentInterval = 1.2f;
    }



}

