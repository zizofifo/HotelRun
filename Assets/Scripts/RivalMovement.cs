using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedMultiplier = 5f;
    public float jumpHeight = 20f;


    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private bool hitWall = false;
    [SerializeField]
    private bool performJump = false;
    private Rigidbody rb;

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
            MovePlayer();
    }

    void MovePlayer()
    {
        if (hitWall)
        {
            return;
        }

        Vector2 position = transform.position;

        float xMovement = 0.5f;


        position.x += xMovement * speedMultiplier * Time.deltaTime;
        transform.position = position;

        if (performJump && !isJumping && canJump)
        {
            performJump = false;
            isJumping = true;
            canJump = false;
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Rival collision");

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
            case "Player":
                break;
            case "Obstacle":
            default:
                performJump = true;
                break;
        }
    }
}