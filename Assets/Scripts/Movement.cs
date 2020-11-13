using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    static public Movement M;
    // Start is called before the first frame update
    public bool isPlayerControlled = true;
    public float speedMultiplier = 5f;
    public float jumpHeight = 8f;
    public bool isMotivated = false;

    private float startSpeed;



    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    private Rigidbody rb;

    void Awake()
    {
        if (M == null)
        {
            M = this;
        }

        startSpeed = speedMultiplier;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isPlayerControlled)
        {
            MovePlayer();
        }

    }

    void MovePlayer()
    {
        Vector2 position = transform.position;

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetKey(KeyCode.Space);


        if (VendingMachine.VM.inProximity == true)
        {
            if (isMotivated == false)
            {
                isMotivated = true;
                PowerUp();
                Invoke("PowerDown", 5f);
            }
        }



        if (IceMachine.IM.isCrossed == true && isMotivated == true)
        {
            Stun();
        }

        if (Crowd.C.inCrowd == true)
        {
            position.x += xMovement * (speedMultiplier / 2) * Time.deltaTime;
        }

        else if (Crowd.C.inCrowd == false)
        {
            position.x += xMovement * speedMultiplier * Time.deltaTime;
        }

        transform.position = position;

        if (spacePressed && !isJumping && canJump)
        {
            isJumping = true;
            canJump = false;
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
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

    void PowerUp()
    {
        speedMultiplier = speedMultiplier * 2;
    }

    void PowerDown()
    {
        if (isMotivated == true)
        {
            speedMultiplier = speedMultiplier / 2;
            isMotivated = false;
        }
    }

    void Stun()
    {
        isMotivated = false;
        float noSpeed = 0;

        speedMultiplier = noSpeed;
        canJump = false;
        Invoke("setStartSpeed", 3f);

    }

    void setStartSpeed()
    {
        speedMultiplier = startSpeed;
        canJump = true;
    }
}
