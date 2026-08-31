using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private float maxHealth,knockbackSpeedX,knockbackSpeedY,knockbackDuration,knockbackDeathSpeedX,knockbackDeathSpeedY,deathTorque;

    [SerializeField] private bool applyKnockback;

    [SerializeField] private GameObject hitParticle;

    private float currentHealth,knockbackStart;

    private int playerFacingDirection;
    private bool playerOnLeft,knockback;

    private PlayerController playerController;
    private GameObject aliveGO,brokenTopGO,brokenBottomGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBottom;
    private Animator aliveAnim;



    private void Start()
    {
        currentHealth = maxHealth;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        aliveGO = transform.Find("Alive").gameObject;
        brokenBottomGO = transform.Find("BrokenBottom").gameObject;
        brokenTopGO = transform.Find("BrokenTop").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent <Rigidbody2D>();
        rbBrokenBottom = brokenBottomGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBottomGO.SetActive(false);
    }
    private void Update()
    {
        CheckKnockback();
    }


    private void Damage(AttackDetails details)
    {
        currentHealth -= details.damageAmount;

        if (details.position.x < aliveGO.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else { playerFacingDirection = -1; }

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft =false;
        }

        aliveAnim.SetBool("playerOnLeft" , playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockback && currentHealth > 0.0f) 
        {
            Knockback();
        }

        if(currentHealth < 0.0f)
        {
            Die();
        }
    }
    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;

        rbAlive.linearVelocity = new Vector2 (knockbackSpeedX * playerFacingDirection, knockbackSpeedY);    
    }
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.linearVelocity = new Vector2(0f, rbAlive.linearVelocity.y);
        }
    }
    private void Die()
    {
        aliveGO.SetActive(false);
        brokenBottomGO.SetActive(true);
        brokenTopGO.SetActive(true);


        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBottomGO.transform.position = aliveGO.transform.position;    

        rbBrokenTop.linearVelocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenBottom.linearVelocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.AddTorque(deathTorque *-playerFacingDirection,ForceMode2D.Impulse);
    }
}
