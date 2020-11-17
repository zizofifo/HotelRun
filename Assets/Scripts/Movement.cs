using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    static public Movement Player;
    //static public GameObject playerCam;
    // Start is called before the first frame update
    public GameObject PlayerSprite;
    public bool isPlayerControlled = true;
    public float speedMultiplier = 5f;
    public float jumpHeight = 8f;

    [Header("Set dynamically")]
    public bool isMotivated = false;
    public bool inCrowd = false;
    public bool crossedIceMachine = false;

    private float startSpeed;
    private bool powerUpCheck = false;



    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    private Rigidbody rb;
    private Animator anim;

    void Awake()
    {
        if (Player == null)
        {
            Player = this;
        }
        PlayerSprite.transform.position = transform.position;
        //playerCam = PlayerSprite;
        startSpeed = speedMultiplier;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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
            PlayerCam.POI = PlayerSprite;
        }

    }

    void MovePlayer()
    {
        Vector2 position = transform.position;

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetKey(KeyCode.Space);


        if (isMotivated == true)
        {
            if (powerUpCheck == false)
            {
                powerUpCheck = true;
                PowerUp();
                Invoke("PowerDown", 5f);
            }
        }



        if (crossedIceMachine == true && powerUpCheck == true)
        {
            Stun();
        }

        if (inCrowd == true)
        {
            position.x += xMovement * (speedMultiplier / 2) * Time.deltaTime;
        }

        else if (inCrowd == false)
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
        if (powerUpCheck == true)
        {
            speedMultiplier = speedMultiplier / 2;
            powerUpCheck = false;
        }
    }

    void Stun()
    {
        powerUpCheck = false;
        float noSpeed = 0;

        anim.SetBool("hasSlipped", true);
        speedMultiplier = noSpeed;
        canJump = false;
        Invoke("setStartSpeed", 3f);

    }

    void setStartSpeed()
    {
        speedMultiplier = startSpeed;
        anim.SetBool("hasSlipped", false);
        canJump = true;
    }
}
