using NUnit.Framework.Constraints;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public GameObject aliveGO { get; private set; }

    public AnimationToStateMachine animationToStateMachine { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform LedgeCheck;
    [SerializeField] private Transform PlayerCheck;
    [SerializeField] private Transform GroundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    public int lastDamageDirection { get; private set; }
    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;



    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();
        animationToStateMachine = aliveGO.GetComponent<AnimationToStateMachine>();


        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.linearVelocity.y);
        rb.linearVelocity = velocityWorkspace;
    }
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.linearVelocity = velocityWorkspace;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.groundLayer);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(LedgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.groundLayer);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(PlayerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.playerLayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(PlayerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.playerLayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(PlayerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.playerLayer);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, entityData.groundCheckRadius, entityData.groundLayer);
    }
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.linearVelocity.x, velocity);
        rb.linearVelocity = velocityWorkspace;
    }
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void Damage(AttackDetails attackDetails)
    {

        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.hopSpeed);

        Instantiate(entityData.hitParticle,aliveGO.transform.position,Quaternion.Euler(0f,0f,Random.Range(0f,360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }


        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if (currentHealth <= 0) { isDead = true; }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere((PlayerCheck.position) + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position) + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position) + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
    }



}
