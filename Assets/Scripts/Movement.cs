using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    static public Movement M;
    // Start is called before the first frame update
    public bool isPlayerControlled = true;
    public float speedMultiplier = 1f; 
    public float jumpHeight = 1f;
    public bool isMotivated = false;



    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;

    void Awake()
    {
        if (M == null)
        {
            M = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerControlled)
        {
            MovePlayer();
        }

        if (VendingMachine.VM.inProximity = true)
        {
            isMotivated = true;
        }
    }

    void MovePlayer()
    {
        Vector2 position = transform.position;

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetKey(KeyCode.Space);



        if (IceMachine.IM.isCrossed == true)
        {
            float halfSpeed = speedMultiplier / 2;
            position.x += xMovement * halfSpeed * Time.deltaTime;
        }

        else
        {
            position.x += xMovement * speedMultiplier * Time.deltaTime;
        }

        if (spacePressed && !isJumping && canJump)
        {
            isJumping = true;
            canJump = false;
            position.y += jumpHeight;
        }

        transform.position = position;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");

        switch (other.gameObject.tag)
        {
            case "StoryFloor":
                if (isJumping)
                {
                    isJumping = false;
                    canJump = true;
                }
                break;
        }
    }
}
