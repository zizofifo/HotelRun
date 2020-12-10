using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CeilingLamp : MonoBehaviour
{
    Animator anim;
    public bool isBroken = false;
    public bool hasJustBroken = false;
    public GameObject brokenBy;
    public bool isLit = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasJustBroken)
        {
            hasJustBroken = false;
        }

        if (isBroken)
        {
            return;
        }

        // Rival doesn't break lamps
        if (other.gameObject.tag == "Rival")
        {
            return;
        }

        Rigidbody2D otherRb;
        if (!other.gameObject.TryGetComponent<Rigidbody2D>(out otherRb))
        {
            return;
        }

        // Only electrocute if the colliding object jumps into it.
        if (otherRb.velocity.y > 0)
        {
            isBroken = true;
            hasJustBroken = true;
            isLit = false;
            anim.SetBool("hasBeenHit", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (hasJustBroken)
        {
            hasJustBroken = false;
        }
    }
}
