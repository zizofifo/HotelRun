using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayerControlled = false;
    public float speedMultiplier = 1; 

    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        position.x += xMovement * speedMultiplier * Time.deltaTime;

        if (spacePressed && !isJumping && canJump)
        {
            isJumping = true;
            canJump = false;
            position.y += 1f;
        }

        transform.position = position;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "StoryFloor")
        {
            Debug.Log("Collision handled");
            
            if (isJumping)
            {
                isJumping = false;
                canJump = true;
            }
        }
    }
}
