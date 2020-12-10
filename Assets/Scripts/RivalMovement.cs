using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RivalMovement : MonoBehaviour
{
    static public RivalMovement Rival;
    // Start is called before the first frame update
    public float speedMultiplier = 5f;
    public float jumpHeight = 20f;

    [Header("Internal State")]
    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private bool hitWall = false;
    [SerializeField]
    private bool performJump = false;
    [SerializeField]
    private bool performAutoJump = false;
    [SerializeField]
    public HorizontalDirection currentDirection = HorizontalDirection.Right;

    private Rigidbody2D rb;

    void Start()
    {
        if (Rival == null)
        {
            Rival = this;
        }
        rb = GetComponent<Rigidbody2D>();
        //InvokeRepeating("AutoJump", 2f, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        /*
        if (hitWall)
        {
            return;
        }
        */

        Vector2 position = transform.position;

        float xMovement = 0.5f;

        position.x += (xMovement * (currentDirection == HorizontalDirection.Left ? -1f : 1f)) * speedMultiplier * Time.deltaTime;
        transform.position = position;

        bool jumpNow = performJump || performAutoJump;

        if (jumpNow && !isJumping && canJump)
        {
            performJump = false;
            performAutoJump = false;
            isJumping = true;
            canJump = false;
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Rival: Enter Collision with " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "StoryFloor":
                isJumping = false;
                canJump = true;
                break;
            case "StoryWall":
                hitWall = true;
                isJumping = false;
                canJump = false;
                break;

            case "Stairwell":
                break;
            case "Elevator":
                break;

            case "StoryCeiling":
            case "Player":
            case "Crowd":
            case "IceMachine":
            case "VendingMachine":
            // WetFloorSign doesn't have any colliders at the time of writing this comment but whatever
            case "WetFloorSign":
                break;

            case "Obstacle":
            case "LuggageCart":
            default:
                performJump = true;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Rival: Exit Collision with " + other.gameObject.tag);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Rival: Enter Trigger with " + other.gameObject.tag);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Rival: Exit Trigger with " + other.gameObject.tag);
    }

    void AutoJump()
    {
        if (canJump)
        {
            Debug.Log("Rival: Requesting auto jump");
            performAutoJump = true;
        }
    }
}
