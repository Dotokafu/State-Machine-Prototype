using UnityEngine;

public class Enemy2 : Entity
{
    public E2_moveState moveState {  get; private set; }

    public E2_IdleState idleState { get; private set; } 

    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }

    public E2_MeleeAttackState meleeAttackState { get; private set; }

    public E2_RangedAttackState rangedAttackState { get; private set; }

    public E2_DodgeState dodgeState { get; private set; }
    

    [SerializeField] private D_MoveState moveData;
    [SerializeField] private D_IdleState idleData;
    [SerializeField] private D_LookForPlayerState lookForPlayerData;
    [SerializeField]private D_PlayerDetectedState playerDetectedData;
    [SerializeField] private D_StunState stunData;
    [SerializeField] private D_DeadState deadData;
    [SerializeField]private D_MeleeAttackState meleeAttackData;
    [SerializeField]public D_DodgeState dodgeData;
    [SerializeField]private D_RangedAttackState rangedAttackData;

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_moveState(this, stateMachine, "move", moveData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadData, this);
        dodgeState = new E2_DodgeState(this, stateMachine,"dodge",dodgeData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine,"rangedAttack",rangedAttackPosition, rangedAttackData, this);  

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState) {
            stateMachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediatly(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
        
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position ,meleeAttackData.attackRadius);
    }
}
