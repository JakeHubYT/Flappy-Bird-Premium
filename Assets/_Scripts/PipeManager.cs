using System.Collections;
using UnityEngine;

public enum PipeSpeedState
{
    Normal,
    Fast,
    Tight
}

public class PipeManager : MonoBehaviour
{
   

    [Header("Normal Pipe Settings")]
    [SerializeField] private float normalPipeSpeed = 5f;
    [SerializeField] private float normalInterval = 2f;
    [SerializeField] private float normalPipeRanY = 5f;


    [Header("Fast Pipe Settings")]
    [SerializeField] private float fastPipeSpeed = 10f;
    [SerializeField] private float fastInterval = 1f;
    [SerializeField] private float fastPipeRanY = 10f;

    [Header("Tight Pipe Settings")]
    [SerializeField] private float tightPipeSpeed = 5f;
    [SerializeField] private float tightInterval = 2f;
    [SerializeField] private float tightPipeRanY = 5f;
    [SerializeField] private float tightObstacleDuration = 5f;



    [Header("Pipe Switch Timing")]
    [SerializeField] private float howLongBetweenState = 2f;
    [SerializeField] private float stateChangesEvery = 5;
    private float defaultDuration;

    [SerializeField] PipeSpeedState currentPipeSpeedState = PipeSpeedState.Normal;

    [Header("Other")]
    [SerializeField] private GameObject tightPipe;
    [SerializeField] private GameObject normalPipe;
    [SerializeField] private Transform pipeSpawnPos;
    private GameObject currentPipe;



    [Header("Current Values")]
    [SerializeField] private float currentPipeRanY = 3.5f;
    [SerializeField] private float currentInterval;
    [SerializeField] private float pipeSpeed = 10f;


    [SerializeField] private bool canSpawnPipes = true;
    [SerializeField] private bool canSpawnPipesDuringInterval = true;


    private Vector3 lastSpawnedObjectPos;



    private bool canDamage = true;
    private bool spawnOnStart = true;
    private float timeSinceLastSpawn;
  
    public bool speedUpPipes = false;
    bool slowDownPipes = true;
    private Coroutine speedUpPipesRoutine;
    private Coroutine thisRoutine;
   

    public static PipeManager Instance { get; private set; }


    #region Getters

    public float GetPipeSpeed() => pipeSpeed;
    public bool GetCanDamage() => canDamage;

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

        currentPipe = normalPipe;
        currentPipeSpeedState = PipeSpeedState.Normal;
    }


    private void Start()
    {
        timeSinceLastSpawn = 0f;

        if (spawnOnStart)
        {
            SpawnPipe();
        }
       defaultDuration = stateChangesEvery;
     
       StartCoroutine(CallFunctionAfterDelay());
   
    }

    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopSpawningPipes;

        Actions.OnInvulnerable += PreventDamage;
        Actions.OnVulnerable += AllowDamage;

        Actions.OnSkipStart += SpeedUpPipes;
        Actions.OnSkipStartResetValues += SlowPipes;
        Actions.OnSkipStartEnd += SlowPipes;

    }

    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopSpawningPipes;

        Actions.OnInvulnerable -= PreventDamage;
        Actions.OnVulnerable -= AllowDamage;

        Actions.OnSkipStart -= SpeedUpPipes;
        Actions.OnSkipStartEnd -= SlowPipes;
        Actions.OnSkipStartResetValues -= SlowPipes;


    }

    private void Update()
    {
        if (!canSpawnPipes) return;
        if (!canSpawnPipesDuringInterval) return;



        if (!speedUpPipes && slowDownPipes)
        {
            Debug.Log("IN");
            switch (currentPipeSpeedState)
            {
                case PipeSpeedState.Normal:
                    currentInterval = normalInterval;
                    pipeSpeed = normalPipeSpeed;
                    currentPipeRanY = normalPipeRanY;
                    currentPipe = normalPipe;

                    stateChangesEvery = defaultDuration;

                    break;
                case PipeSpeedState.Fast:
                    currentInterval = fastInterval;
                    pipeSpeed = fastPipeSpeed;
                    currentPipeRanY = fastPipeRanY;
                    currentPipe = normalPipe;

                    stateChangesEvery = defaultDuration;

                    break;
                case PipeSpeedState.Tight:

                   
                    currentInterval = tightInterval;
                    pipeSpeed = tightPipeSpeed;
                    currentPipeRanY = tightPipeRanY;
                    currentPipe = tightPipe;
                    stateChangesEvery = tightObstacleDuration;

                    break;
            }
        }

        if (speedUpPipes)
        {
            currentInterval *= 1;
            pipeSpeed *= 4f;
            Actions.OnInvulnerable();
            speedUpPipes = false;
            slowDownPipes= false;
            

            if (speedUpPipesRoutine != null) StopCoroutine(speedUpPipesRoutine);
            speedUpPipesRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2f));
        }



        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentInterval)
        {
            timeSinceLastSpawn = 0f;
            SpawnPipe();
        }

    }


    IEnumerator CallFunctionAfterDelay()
    {
        Debug.Log("Called");
        yield return new WaitForSeconds(stateChangesEvery); // wait
        SetSpeedBasedOnTimer();
        // call your function here

    }

    void SetSpeedBasedOnTimer()
    {
        /*  if (currentPipeSpeedState == PipeSpeedState.Tight)
              SetPipeSpeedState(PipeSpeedState.Normal);

          else if (currentPipeSpeedState == PipeSpeedState.Normal)
              SetPipeSpeedState(PipeSpeedState.Fast);

          else if (currentPipeSpeedState == PipeSpeedState.Fast)
              SetPipeSpeedState(PipeSpeedState.Tight);*/

        PipeSpeedState randomState = (PipeSpeedState)Random.Range(0, System.Enum.GetValues(typeof(PipeSpeedState)).Length);

        if(randomState == PipeSpeedState.Tight)
            stateChangesEvery = tightObstacleDuration;
        SetPipeSpeedState(randomState);
    }

    public void SetPipeSpeedState(PipeSpeedState speedState)
    {
        currentPipeSpeedState = speedState;
        canSpawnPipesDuringInterval = false;
        StartCoroutine(EnablePipeSpawningAfterDelay());
    }


    private void SpawnPipe()
    {
        float ranYScale = Mathf.Clamp(1f / currentInterval, 0.5f, 1f);
        float ranY = Random.Range(-currentPipeRanY * ranYScale, currentPipeRanY * ranYScale);
        Vector3 pipeRanSpawn = new Vector3(pipeSpawnPos.position.x, ranY, pipeSpawnPos.position.z);
        Instantiate(currentPipe, pipeRanSpawn, Quaternion.identity);
        lastSpawnedObjectPos = pipeRanSpawn;
    }


    private IEnumerator EnablePipeSpawningAfterDelay()
    {
        yield return new WaitForSeconds(howLongBetweenState);

        if(howLongBetweenState > 1f)
        howLongBetweenState = howLongBetweenState - .25f;

        canSpawnPipesDuringInterval = true;
        StartCoroutine(CallFunctionAfterDelay());

    }

    private IEnumerator StartVulnerableTimer(float delay)
    {
        while (true)
        {
           

            yield return new WaitForSeconds(delay);

           // Debug.Log("Calling On Vulnerable");
            Actions.OnVulnerable();
        }
    }

    public void SpeedUpPipes()
    {
        if (thisRoutine != null)
            StopCoroutine(thisRoutine);

        speedUpPipes = true;
        thisRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2));

    }

    void SlowPipes()
    {
        /*currentInterval *= 4f;
        pipeSpeed *= .25f;*/
        slowDownPipes = true;
        currentPipeSpeedState = PipeSpeedState.Normal;

    }

    private void StopSpawningPipes() => canSpawnPipes = false;

    private void PreventDamage() => canDamage = false;
    private void AllowDamage() => canDamage = true;

 

}

