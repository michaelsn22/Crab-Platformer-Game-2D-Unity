using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool guard = false;
    bool crouch = false;
    private GameObject[] players;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
        //if (Input.GetButtonDown("w")) {
        //    guard = true;
       // }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnLevelWasLoaded(int level){
        FindStartPos();
        players = GameObject.FindGameObjectsWithTag("Player");
        
        if(players.Length > 1){
            Destroy(players[0]);
        }
        
    }

    void FindStartPos(){
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
