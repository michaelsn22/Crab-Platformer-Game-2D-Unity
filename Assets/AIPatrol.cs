using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{

    public float walkSpeed;
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public LayerMask groundLayer;
    //public Collider2D bodyCollider;

    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol){
            Patrol();
        }
    }
    void FixedUpdate(){
        if(mustPatrol){
            //return true if contains ground
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
    // || bodyCollider.IsTouchingLayers(groundLayer
    void Patrol() {
        if(mustTurn){
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    
    void Flip(){
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
