using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform GroundCheck;

    public LayerMask GroundLayer;

    public float Speed = 10f;
    public float JumpForce = 16f;
    public float GroundCheckRadius;



    
    private Rigidbody2D playerRb;
    private Animator animator;
    private InputSystem_Actions actions;

    private bool isFacingRight =true;
    private bool isGrounded =false;
    private bool isWalking =false;
    private bool canJump = true;
    private bool canFlip =true;



    private Vector2 moveInput;
    private void OnEnable()
    {
        actions.Player.Enable();
    }
    private void OnDisable()
    {
        actions.Player.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        actions = new InputSystem_Actions();
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        turnCheck();
        CheckJump();
        UpdateAnimations();
        
    }
    private void FixedUpdate()
    {
        Move();
        CheckCollisions();
        
    }
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking",isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", playerRb.linearVelocity.y);
    }
    private void HandleInput()
    {

        moveInput = actions.Player.Move.ReadValue<Vector2>();

        if (actions.Player.Jump.WasPressedThisFrame())
        { 
            Jump();
        }
       
    }
    #region Movement
    private void Move()
    {
        if (moveInput != Vector2.zero)
        {
            playerRb.linearVelocity = new Vector2(moveInput.x * Speed ,playerRb.linearVelocity.y);
        }
        else
        {
            playerRb.linearVelocity = new Vector2(0, playerRb.linearVelocity.y);
        }

    }
    private void turnCheck()
    {
        if (isFacingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput.x > 0) {
           Flip();
        }
        if (Mathf.Abs(playerRb.linearVelocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else { isWalking = false; }
    }
    private void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private void DisableFlip()
    {
        canFlip = false;
    }
    private void EnableFlip()
    {
        canFlip = true; 
    }
    private void Jump()
    {
        if ( canJump)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, JumpForce);
        }
        
            
        
    }
    private void CheckJump()
    {
        if (isGrounded && playerRb.linearVelocity.y <= 0.01)
        {
            canJump = true;
        }
        else { canJump = false; }
    }
    #endregion

    private void CheckCollisions()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position,GroundCheckRadius,GroundLayer);
      
    }
    
}

