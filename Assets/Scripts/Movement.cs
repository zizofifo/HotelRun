using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    static public Movement Player;
    public bool isPlayerControlled = true;
    public float speedMultiplier = 5f;
    public float jumpHeight = 8f;

    [Header("Set dynamically")]
    public bool isMotivated = false;
    public bool inCrowd = false;
    public bool isStunned = false;

    private float startSpeed;

    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    private Rigidbody2D _rb;
    private Animator anim;

    public Rigidbody rb
    {
        get
        {
            return _rb;
        }
        set
        {
            _rb = value;
        }
    }

    void Awake()
    {
        if (Player == null)
        {
            Player = this;
        }
        PlayerCam.POI = gameObject;
        startSpeed = speedMultiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        }

    }

    void MovePlayer()
    {
        Vector2 position = transform.position;
        Vector2 velocity = rb.velocity;

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetKey(KeyCode.Space);

        velocity.x += xMovement * speedMultiplier * Time.deltaTime;

        rb.velocity = velocity;

        if (spacePressed && !isJumping && canJump)
        {
            isJumping = true;
            canJump = false;
            _rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player: Enter Collision with " + other.gameObject.tag);

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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player: Enter Trigger with " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
                case "IceMachine":
                    if (isMotivated)
                    {
                        Stun();
                    }
                    break;
                case "Crowd":
                    speedMultiplier /= 2;
                    inCrowd = true;
                    break;
                case "Stairwell":
                    canJump = false;
                    break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Player: Exit Collision with " + other.gameObject.tag);

        /*
        switch (other.gameObject.tag)
        {
        }
        */
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player: Exit Trigger with " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
                case "Crowd":
                    speedMultiplier *= 2;
                    inCrowd = false;
                    break;
                case "Stairwell":
                    canJump = true;
                    break;
        }
    }

    public void PowerUp()
    {
        if (!isMotivated)
        {
            isMotivated = true;
            speedMultiplier = speedMultiplier * 2;
            Invoke("PowerDown", 5f);
        }
    }

    public void PowerDown()
    {
        if (isMotivated)
        {
            speedMultiplier = speedMultiplier / 2;
            isMotivated = false;
        }
    }

    public void Stun()
    {
        if (isStunned)
        {
            return;
        }
        float noSpeed = 0;

        isStunned = true;
        anim.SetBool("hasSlipped", true);
        speedMultiplier = noSpeed;
        canJump = false;
        Invoke("setStartSpeed", 3f);

    }

    void setStartSpeed()
    {
        speedMultiplier = startSpeed;
        isStunned = false;
        anim.SetBool("hasSlipped", false);
        canJump = true;
    }
}
