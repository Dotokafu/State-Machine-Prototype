using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
  private enum State
    {
        Walking,
        Knockback,
        Dead   
    }
    [SerializeField] private float groundCheckDistance, wallCheckDistance,movementSpeed,maxHealth,knockbackDuration,touchDamageCooldown,touchDamage,touchDamageWidth,touchDamageHeight ;

    [SerializeField] private Vector2 knockbackSpeed ;

    [SerializeField] private Transform groundCheck, WallCheck,touchDamageCheck;

    [SerializeField] private LayerMask GroundLayer,PlayerLayer;

    [SerializeField] private GameObject hitParticle,deathChunckParticle,deathBloodParticle;

    private bool groundDetected, wallDetected;

    private int facingDirection,damageDirection;

    private float currentHealth,knockbackStartTime, lastTouchDamageTime;

    private float[] attackDetails  = new float[2];

    private Vector2 movement,touchDamageBotLeft,touchDamageTopRight;

    private State currentState;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;



    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();
        facingDirection = 1;
        currentHealth = maxHealth;
    }


    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
        }
    }

    #region WalkingState
    private void EnterWalkingState()
    {

    }
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,GroundLayer);
        wallDetected = Physics2D.Raycast(groundCheck.position, transform.right, wallCheckDistance, GroundLayer);

        CheckTouchDamage();
        if (!groundDetected|| wallDetected) 
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.linearVelocity.y);
            aliveRb.linearVelocity = movement;
        }
    }
    private void ExitWalkingState()
    {

    }
    #endregion

    #region KnockbackState
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time; 
        movement.Set(knockbackSpeed.x*damageDirection, knockbackSpeed.y);
        aliveRb.linearVelocity = movement;
        aliveAnim.SetBool("knockback",true);
    }
    private void UpdateKnockbackState()
    {
         if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }
    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("knockback",false);
    }
    #endregion
    #region DeadState 
    private void EnterDeadState()
    {
        Instantiate(deathChunckParticle, alive.transform.position, deathChunckParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }
    #endregion


    private void Damage(float[] attackDetails) 
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }
    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime+ touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth/2),touchDamageCheck.position.y -(touchDamageHeight/2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));


            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight,PlayerLayer);

            if (hit != null) 
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;

                hit.SendMessage("Damage", attackDetails);
            }
        }
    }
    private void SwitchState(State state) 
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,new Vector2(groundCheck.position.x, groundCheck.position.y-groundCheckDistance));

        Gizmos.DrawLine(WallCheck.position,new Vector2(WallCheck.position.x+wallCheckDistance,WallCheck.position.y));  
        
      
    }
}
