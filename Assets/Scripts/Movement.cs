﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    static public Movement Player;
    public bool isPlayerControlled = true;
    public float speedMultiplier = 5f;
    public float jumpHeight = 8f;
    public float speedCap = 15f;

    [Header("Set dynamically")]
    public Vector2 velocity;
    public bool isMotivated = false;
    public bool inCrowd = false;
    public bool isStunned = false;

    public bool canWarp = false;
    public bool isBeingWarped = false;

    public bool isElectrocuted = false;
    public bool isUsingTowel = false;

    public int sodaCans;
    public int towels;


    private float startSpeed;

    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool canJump = true;
    private Rigidbody2D _rb;
    private Animator anim;
    private Vector2? oldVelocity;
    private RigidbodyConstraints2D? oldRigidbodyConstraints2D;

    public Rigidbody2D rb
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
        if (isElectrocuted)
        {
            return;
        }

        Vector2 position = transform.position;
        velocity = _rb.velocity;

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        bool spacePressed = Input.GetKey(KeyCode.Space);
        bool qPressed = Input.GetKey(KeyCode.Q);
        bool ePressed = Input.GetKey(KeyCode.E);

        if (qPressed)
        {
            PowerUp();
        }

        if (ePressed)
        {
            TowelUp();
        }

        velocity.x = xMovement * speedMultiplier;

        velocity.x = Mathf.Clamp(velocity.x, -speedCap, speedCap);

        if (spacePressed && !isJumping && canJump)
        {
            isJumping = true;
            canJump = false;
            velocity.y = jumpHeight;
        }

        if (xMovement == 0 && !isJumping)
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, 0.01f);
        }

        if (isStunned)
        {
            velocity = Vector2.zero;
        }

        _rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player: Enter Collision with " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "StoryFloor":
            case "LuggageCart":
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
            case "Elevator":
            case "Stairwell":
                canWarp = true;
                break;
            case "CeilingLamp":
                CeilingLamp ceilingLamp;

                if (!other.gameObject.TryGetComponent<CeilingLamp>(out ceilingLamp))
                {
                    return;
                }

                if (ceilingLamp.isBroken)
                {
                    Electrocute();
                }
                break;
            case "VendingMachine":
                VendingMachine vendingMachine;

                if (!other.gameObject.TryGetComponent<VendingMachine>(out vendingMachine))
                {
                    return;
                }

                sodaCans += vendingMachine.Vend();
                break;
            case "RolledTowel":
                RolledTowel towel;

                if (!other.gameObject.TryGetComponent<RolledTowel>(out towel))
                {
                    return;
                }

                towels += towel.PickUp();
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
                canWarp = false;
                break;

        }
    }

    public void PowerUp()
    {
        if (isMotivated || sodaCans <= 0 || isUsingTowel)
        {
            return;
        }

        --sodaCans;

        isMotivated = true;
        speedMultiplier = speedMultiplier * 2;
        Invoke("PowerDown", 5f);
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

        if (isUsingTowel)
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

    void Electrocute()
    {
        isElectrocuted = true;

        oldVelocity = _rb.velocity;
        _rb.velocity = Vector2.zero;

        oldRigidbodyConstraints2D = _rb.constraints;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;

        anim.SetBool("hasBeenElectrocuted", true);
        Invoke("UnElectrocute", 1.5f);
    }

    void UnElectrocute()
    {
        anim.SetBool("hasBeenElectrocuted", false);

        _rb.constraints = (RigidbodyConstraints2D)oldRigidbodyConstraints2D;
        oldRigidbodyConstraints2D = null;

        Vector2 restoredVelocity = (Vector2)oldVelocity;
        restoredVelocity = new Vector2(restoredVelocity.x, -Mathf.Abs(restoredVelocity.y / 1.5f));
        _rb.velocity = restoredVelocity;
        oldVelocity = null;

        isElectrocuted = false;
    }

    void TowelUp()
    {
        if (isUsingTowel || towels <= 0)
        {
            return;
        }

        isUsingTowel = true;
        --towels;
        Invoke("UnTowelUp", 3f);
    }

    void UnTowelUp()
    {
        isUsingTowel = false;
    }
}
