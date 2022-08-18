using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    //how far away the enemy needs to be to move to the next waypoint instead of current one.
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    [Header("Pathfinding Stuff")]
    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

    

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate() {
        if (TargetInDistance() && followEnabled){
            PathFollow();
        }
    }

    private void UpdatePath() {
        if (followEnabled && TargetInDistance() && seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow(){
        if (path == null)
        {
            return;
        }

        //reached end of path
        if (currentWaypoint >= path.vectorPath.Count){
            return;
        }

        //see if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        //Direction calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        //movement
        //private Vector2 currentVelocity;
        //rb.velocity = Vector2.SmoothDamp(body.velocity, force, ref currentVelocity, 0.5f);
        rb.AddForce(force);
        //float step = speed*Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target, step);

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance){
            currentWaypoint++;
        }

        //direction gfx handling
        if (directionLookEnabled){
            if (rb.velocity.x > 0.05f){
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f){
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                //flip if moving left or right
            }
        }
    }

    private bool TargetInDistance() {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }
}
