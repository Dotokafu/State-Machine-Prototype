using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb {  get; private set; }
    public Animator animator { get; private set; }

    public GameObject aliveGO {  get; private set; }


    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();
    }

}
