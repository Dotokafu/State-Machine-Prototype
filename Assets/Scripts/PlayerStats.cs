using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject deathChunckParticle,deathBloodParticle;

    private float currentHealth;

    private GameManager gameManager;


    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void DecreseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(deathChunckParticle, transform.position, deathChunckParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);

        gameManager.Respawn();
        Destroy(gameObject);
    }
}
