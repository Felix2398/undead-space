using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    
    private int currentWave = 0;
    public int enemiesAlive = 0;
    private bool waitingForNextWave = false;
    
    public float spawnRadius = 10f;
    
    static WaveSpawner instance;
    
    public int enemiesPerBurst = 8; // Anzahl der Feinde pro Schub
    public float timeBetweenBursts = 4f; // Zeit in Sekunden zwischen den Schüben

    public float standardSpeed = 0.8f;
    public float fastSpeed = 2f;
    // Wahrscheinlichkeit, dass ein Zombie schneller ist (z.B. 20%)
    public float fastZombieProbability = 0.2f;

    public GameObject[] powerUpPrefabs;

    public int maxAmountPowerUps = 2;
    public int powerUpsSpawned = 0;

    private int maxEnemiesAlive = 100;
    
    public static WaveSpawner GetInstance() {
        return instance;
    }
    
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    private void Start()
    {
        StartNextWave();
    }
    
    void Update()
    {
    }

    IEnumerator DisplayNextRoundWarning()
    {
        // Warte 3 Sekunden
        yield return new WaitForSeconds(3);

        GameManager.GetInstance().DisplayNextWaveWarning();
    }
    
    void StartNextWave()
    {
        waitingForNextWave = true;
        StartCoroutine(WaitAndStartNextWave());
    }
    
    IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(5f);
        currentWave++;

        GameManager.GetInstance().UpdateCurrentWaveLabel(currentWave);

        waitingForNextWave = false;
        int totalEnemiesToSpawn = currentWave * 10;
        int bursts = Mathf.CeilToInt((float)totalEnemiesToSpawn / enemiesPerBurst);
        
        for (int i = 0; i < bursts; i++)
        {
            int enemiesThisBurst = Mathf.Min(enemiesPerBurst, totalEnemiesToSpawn - (i * enemiesPerBurst));
            StartCoroutine(SpawnBurst(enemiesThisBurst));
            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
    
    // IEnumerator SpawnBurst(int enemies)
    // {
    //     for (int i = 0; i < enemies; i++)
    //     {
    //         SpawnEnemy();
    //         yield return null; // Optional: Wartezeit zwischen dem Spawnen einzelner Feinde in einem Schub
    //     }
    // }

    IEnumerator SpawnBurst(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            if (enemiesAlive < maxEnemiesAlive)
            {
                SpawnEnemy();
                yield return null; // Optional: Wartezeit zwischen dem Spawnen einzelner Feinde in einem Schub
            }
            else
            {
                // Warte, bis ein Zombie stirbt, bevor ein neuer gespawnt wird
                yield return new WaitWhile(() => enemiesAlive >= maxEnemiesAlive);
                i--; // Stellt sicher, dass wir die Schleife nicht fortsetzen, ohne einen Zombie zu spawnen
            }
        }
    }
    
    void SpawnEnemy()
    {
        Vector3 spawnPoint = RandomPointAroundPlayer(spawnRadius);
        if (spawnPoint != Vector3.zero)
        {
            GameObject obj = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

            var agent = obj.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                float selectedSpeed = (Random.value < fastZombieProbability) ? fastSpeed : standardSpeed;
                agent.speed = selectedSpeed;
            }

            enemiesAlive++;
        }
    }
    
    public void OnEnemyDeath(Vector3 deathPosition)
    {
        enemiesAlive--;

        CheckIfAllEnemiesDeath();

        if (Random.Range(1, 101) <= 90 && powerUpsSpawned <= maxAmountPowerUps)
        {
            SpawnPowerUp(deathPosition);
        }
    }

    Vector3 RandomPointAroundPlayer(float radius) {

        Vector3 spawnPoint = Vector3.zero;
        int attempts = 0;
        const int maxAttempts = 10; // Maximale Anzahl von Versuchen, um einen gültigen Spawn-Punkt zu finden
        const float minDistanceToPlayer = 5f; // Mindestabstand vom Spieler, um zu verhindern, dass Gegner zu nahe spawnen

        do {

            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += player.transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas)) {
                float distance = Vector3.Distance(hit.position, player.transform.position);
                if (distance >= minDistanceToPlayer) {
                    spawnPoint = hit.position;
                    break; // Ein gültiger Spawn-Punkt wurde gefunden
                }
            }
            attempts++;
        } while (attempts < maxAttempts && spawnPoint == Vector3.zero);

        return spawnPoint; // Gibt den gefundenen Punkt zurück oder Vector3.zero, falls kein gültiger Punkt gefunden wurde
    }

    void SpawnPowerUp(Vector3 spawnPosition)
    {
    if (powerUpPrefabs.Length == 0) return;
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUpPrefab = powerUpPrefabs[randomIndex];
                spawnPosition.y = 0.2f;
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        powerUpsSpawned++;
    }

    public void OnPowerUp() {
        powerUpsSpawned--;
    }

    private void CheckIfAllEnemiesDeath() {

        if (enemiesAlive <= 0 && !waitingForNextWave)
        {
            StartNextWave();
            StartCoroutine(DisplayNextRoundWarning());
        }
    }
}
