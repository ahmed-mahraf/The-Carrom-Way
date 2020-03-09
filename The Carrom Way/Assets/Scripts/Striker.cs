using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{
    Rigidbody2D rb2d;
    public LineRenderer visualizerLine;
    Vector2 startPos;
    Vector2 dir;
    Vector3 worldToMousePos;
    Transform selfTransform;
    public int strikerSpeed = 1000;
    bool isStruck = false;
    public bool posIsSet = false;
    public bool HostPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Declare Rigidbody2D
        startPos = transform.position;
        selfTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        worldToMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Finds the mouse position on the world

        dir = (Vector2)((worldToMousePos - transform.position)); // sets direction by calculating the difference between the location of mouse to position of striker
        dir.Normalize(); // vector keeps the same direction but its length is 1.0.

        if (!isStruck && !posIsSet)
        {
            if (HostPlayer == true)
            {
                selfTransform.position = new Vector2(Mathf.Clamp(worldToMousePos.x, -3, 3), startPos.y);
            }
            else
            {
                selfTransform.position = new Vector2(Mathf.Clamp(worldToMousePos.x, -3, 3), -startPos.y);
            }
        }

        if (posIsSet && rb2d.velocity.magnitude == 0)
        {
            visualizerLine.SetPosition(0, selfTransform.position);
            visualizerLine.SetPosition(1, worldToMousePos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!posIsSet)
            {
                posIsSet = true;
            }
        }

        // ---- FIXED UPDATE ---- //

        if (Input.GetMouseButtonUp(0) && rb2d.velocity.magnitude == 0)
        {
            Shoot();
        }

        if (rb2d.velocity.magnitude < 0.2f && rb2d.velocity.magnitude != 0) // Set arrow when magnitude is less than 0.2 in value.
        {
            resetStriker();
        }

    }

    public void resetStriker()
    {
        switchPlayer();
        rb2d.velocity = Vector2.zero;
        isStruck = false;
        posIsSet = false;
    }

    // Shoot is called when an input is selected
    public void Shoot()
    {
        rb2d.AddForce(dir * GameManager.Instance.CalculateStrikerForce()); // Direction * Speed of Striker = Movement; Just to see if moving the striker works.
        isStruck = true;
    }

    public bool switchPlayer()
    {
        if (HostPlayer == true)
        {
            HostPlayer = false;
            return false;
        }
        else
        {
            HostPlayer = true;
            return true;
        }
    }
}

// Notes: For talcum powder I can set the linear drag and increase it.