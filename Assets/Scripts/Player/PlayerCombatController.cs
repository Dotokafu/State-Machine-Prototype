using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool CombatEnabeled;
    [SerializeField] private float inputTimer,attack1Radius,attack1Damage,stunDamageAmount;
    [SerializeField] private Transform attack1HitboxPos;
    [SerializeField] private LayerMask DamagableLayer;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;
    private InputSystem_Actions actions;
    private Animator animator;
    private PlayerController playerController;
    private PlayerStats playerStats;

    private AttackDetails attackDetails ;
    private float lastInputTime = Mathf.NegativeInfinity;
    private void Awake()
    {
        actions =  new InputSystem_Actions();
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack",CombatEnabeled);
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    private void CheckCombatInput()
    {
        if(actions.Player.Attack.WasPressedThisFrame())
        {
            if (CombatEnabeled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    private void CheckAttacks() 
    {
        if (gotInput) 
        {
            //Perform attack1
            if (!isAttacking) { 
            
                gotInput =false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1",true);
                animator.SetBool("isAttacking",isAttacking);
                animator.SetBool("isFirstAttack",isFirstAttack);
            }
        }

        if(Time.time > lastInputTime + inputTimer)
        {
            //Wait for new Input
            gotInput= false;
        }
    }
    private void CheckAttackHitbox()
    {
        Collider2D[] detecetedObjects = Physics2D.OverlapCircleAll(attack1HitboxPos.position,attack1Radius,DamagableLayer);


        attackDetails.damageAmount=attack1Damage;
        attackDetails.position=transform.position;
        attackDetails.stunDamageAmount=stunDamageAmount;
        foreach (Collider2D collider in detecetedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }

    }
    private void FinnishAttack1()
    {
        isAttacking =false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1",false);

    }
    private void OnDrawGizmos()
    {
         Gizmos.DrawWireSphere(attack1HitboxPos.position,attack1Radius);
    }
    private void Damage(AttackDetails attackDetails)
    {
        int direction;

        playerStats.DecreseHealth(attackDetails.damageAmount);
        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        playerController.Knockback(direction);
    }
}
