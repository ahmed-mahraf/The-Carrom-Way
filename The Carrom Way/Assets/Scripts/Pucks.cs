using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pucks : MonoBehaviour
{
    Rigidbody2D rb2d;
    bool isHit = false;
    public string color;
    public string GetColor { get { return color; } }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (isHit)
        {
            rb2d.drag += Time.deltaTime * 1.5f;//0.7
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit)
            return;

        string name = collision.gameObject.name;
        if (name.Contains("Puck") || name.Contains("Striker"))
        {
            isHit = true;
            Invoke("Reset", 4f);
        }
    }
    public void Reset()
    {
        rb2d.drag = 0f;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        rb2d.rotation = 0f;
        isHit = false;
    }
}
