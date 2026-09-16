using UnityEngine;
[CreateAssetMenu(fileName ="NewPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.2f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("InAir State")]
    public float cayoteTime = 0.2f;
    public float variableJumpHeightMultiplier=0.5f;

    [Header("Wall Slide State")]
    public float wallSlideSpeed = 3f;

    [Header("Attack State")]

    public float attackVelocity = 10f;
    public float attackDamage;
    public float attackRadius;
    public LayerMask damagableLayer;

    



    [Header("Check Veriables")]
    public float GroundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask groundLayer;

}
