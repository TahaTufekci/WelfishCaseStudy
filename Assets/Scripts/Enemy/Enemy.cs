using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float moveSpeed = 3.5f;
    private int maxHealth = 100;
    private int currentHealth;
    private NavMeshAgent navMeshAgent;

    public Slider healthSlider;
    private float attackDistance = 2f;
    private int damageToPlayer = 50; 
    private bool hasAttacked = false;
    private bool isDead = false;

    [SerializeField] AudioClip enemyAttack,enemyDie;
    AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(player.position);
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (navMeshAgent != null && player != null && GameManager.Instance.CurrentGameState != GameState.Lose)
        {
            navMeshAgent.SetDestination(player.position);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackDistance && !hasAttacked)
            {
                AttackPlayer();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(isDead) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToPlayer);
            hasAttacked = true;
            audioSource.PlayOneShot(enemyAttack);
        }
    }

    private void Die()
    {
        GameManager.Instance.OnEnemyDied?.Invoke();
        StartCoroutine(PlayDeathSoundAndDestroy());
    }

    private IEnumerator PlayDeathSoundAndDestroy()
    {
        audioSource.PlayOneShot(enemyDie);
        yield return new WaitForSeconds(enemyDie.length);

        Destroy(gameObject);
    }
}
