using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PingPongMovement : MonoBehaviour
{
    public HorizontalDirection initialDirectionOfTravel;

    /// <summary>
    /// Whether the object is mirrored horizontally, by an instant change of rotation, when its direction of travel changes.
    /// </summary>
    public bool objectHorizontallyFlips = true;

    /// <summary>
    /// How far the object moves. The object moves (range / 2) in each direction from its starting position.
    /// </summary>
    public float range = 1f;

    public float speed = 10f;

    // For debugging
    //public Vector2 insp0;

    private const float LEFT = -1f;
    private const float RIGHT = 1f;
    private float directionOfTravel = 0f;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        SetDirection(initialDirectionOfTravel);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 revisedVelocity = Vector2.zero;
        Quaternion currentRotation = transform.rotation;
        Quaternion revisedRotation = Quaternion.identity;

        if (currentPosition.x < (startPosition.x - (range / 2)))
        {
            SetDirection(HorizontalDirection.Right);
        }
        else if (currentPosition.x > (startPosition.x + (range / 2)))
        {
            SetDirection(HorizontalDirection.Left);
        }

        revisedVelocity = new Vector2(directionOfTravel * speed, 0);

        //insp0 = revisedVelocity;

        rb.velocity = revisedVelocity;
    }

    private void SetDirection(HorizontalDirection direction)
    {
        float rotation = 0;
        switch (direction)
        {
            case HorizontalDirection.Left:
                this.directionOfTravel = LEFT;
                rotation = -360;
                break;
            case HorizontalDirection.Right:
                this.directionOfTravel = RIGHT;
                rotation = +180;
                break;
        }

        if (this.objectHorizontallyFlips)
        {
            transform.Rotate(Vector3.down * rotation);
        }
    }
}
