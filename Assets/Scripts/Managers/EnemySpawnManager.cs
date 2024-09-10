using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player; 
    private int minEnemies = 5; 
    private int maxEnemies = 10; 
    private float spawnRadius = 20f; 
    private float playerMinDistance = 10f;
    private int enemyCount;
    public int EnemyCount => enemyCount;

    private void Start()
    {
        SpawnZombies();
    }

    private void SpawnZombies()
    {
        enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition;
            do
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
                spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
                spawnPosition += player.position;

            } while (Vector3.Distance(spawnPosition, player.position) < playerMinDistance);

            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            var zombieAI = zombie.GetComponent<Enemy>();
            if (zombieAI != null)
            {
                zombieAI.player = player;
            }
        }
    }

}
