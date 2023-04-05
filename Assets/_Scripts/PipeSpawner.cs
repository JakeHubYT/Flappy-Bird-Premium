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

    public float interval;
    private float timeSinceLastSpawn;

    float pipeRanY = 3.5f;
    bool canSpawnPipes = true;
    public bool canDamage = true;

    public float pipeSpeed = 8;
    private bool speedUpPipes = false;
    private float ogPipeSpeed;
  

    Coroutine thisRoutine = null;

    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopSPawningPipes;

        Actions.OnInvulnerable += PreventDamage;
        Actions.OnVulnerable += AllowDamage;

        Actions.OnSkipStart += SpeedUpPipes;
        Actions.OnSkipStartEnd += SlowPipes;

       // Actions.OnDoubleLife += HasSecondLife;

    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopSPawningPipes;

        Actions.OnInvulnerable -= PreventDamage;
        Actions.OnVulnerable -= AllowDamage;

        Actions.OnSkipStart -= SpeedUpPipes;
        Actions.OnSkipStartEnd -= SlowPipes;

        //Actions.OnDoubleLife -= HasSecondLife;




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

        ogPipeSpeed = pipeSpeed;

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



        if (speedUpPipes)
        {

            interval = .75f;
            pipeSpeed = pipeSpeed * 4;
            Actions.OnInvulnerable();
            speedUpPipes = false;
        }
    }

    public void SpeedUpPipes()
    {
        
        if(thisRoutine != null)
        StopCoroutine(thisRoutine); 

        speedUpPipes = true;
        thisRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2));

    }

    void SlowPipes()
    {

        pipeSpeed = ogPipeSpeed;
        interval = 1.2f;
     
    }

    public void GotHitWhileDoubleLife()
    {

        if (thisRoutine != null)
            StopCoroutine(thisRoutine);

      
       // thisRoutine = StartCoroutine(StartVulnerableTimer(AbilityManager.currentAbility.coolDownTime + 2));
        thisRoutine = StartCoroutine(StartVulnerableTimer(2));


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

 


    private IEnumerator StartVulnerableTimer(float delay)
    {
        while (true)
        {

            Debug.Log("Entered");

            yield return new WaitForSeconds(delay);

            Debug.Log("Calling On Vulnerable");
            // Call the function
            Actions.OnVulnerable();

            /*   // Reset the timer if the repeat flag is set
               if (!repeat)
               {
                   // Disable the Timer component if the repeat flag is not set
                   enabled = false;
                   yield break;
               }*/
        }
    }

   
}

