using System.Runtime.InteropServices;
using System.Transactions;
using System.Xml.Serialization;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private float speed;
    private Rigidbody2D rb;
    private float travelDistance;
    private float xStartPos;
    [SerializeField] private float gravity;

    [SerializeField] private float damageRadius;

    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask PlayerLayer;

    [SerializeField] Transform damagePosition;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        isGravityOn = false ;
        rb.linearVelocity = transform.right * speed;

        xStartPos = transform.position.x;
    }
    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;
            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {

        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, PlayerLayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, GroundLayer);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - rb.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }
    public void FireProjectile(float speed,float travelDistance,float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position,damageRadius);    
    }
}
