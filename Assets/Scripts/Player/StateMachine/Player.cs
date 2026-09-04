using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateVeriables
    public PlayerStateMachine StateMachine {  get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }




    [SerializeField] private PlayerData playerData;
    #endregion


    #region Components
    public Rigidbody2D rb {  get; private set; }
    public Animator Animator {  get; private set; }
    public PlayerInputHandler1 InputHandler { get; private set; }
    #endregion


    #region Other Vars
    public Vector2 currentVelocity { get; private set; }
    private Vector2 worksspace;
    public int FacingDirection { get; private set; }
    #endregion

    #region UnityCallBackFunctions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
 
        InputHandler = GetComponent<PlayerInputHandler1>();
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StateMachine.Initilazie(IdleState);
        FacingDirection = 1;
    }

    private void Update()
    {
        currentVelocity =rb.linearVelocity;
        StateMachine.CurrentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        worksspace.Set(velocity, currentVelocity.y);
        rb.linearVelocity = worksspace;
        currentVelocity =worksspace;    
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion


    #region Other Functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
