using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakableWall : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    public void BreakWall()
    {
        boxCollider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        Destroy(gameObject,2f);
    }
}
