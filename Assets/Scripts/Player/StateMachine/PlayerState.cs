using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    private string animBoolName;
    protected float startTime;

    public PlayerState(Player player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;   
    }
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.Animator.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        player.Animator.SetBool(animBoolName, false);
    }


    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }


}
