using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{

    Rigidbody2D rb2d;
    public int strikeSpeed = 500;

    public GameObject ArrowDirection;
    Transform arrowTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Declare Rigidbody2D
        arrowTransform = ArrowDirection.transform; // Declare arrowTransformation
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); 
        }

        if (rb2d.velocity.magnitude < 0.2f) // Set arrow when magnitude is less than 0.2 in value.
        {
            ArrowDirection.SetActive(true); // Sets the arrow
        }
        else
        {
            ArrowDirection.SetActive(false); // Disables the arrow
        }
    }

    // Shoot is called when an input is selected
    public void Shoot()
    {
        Vector3 worldToMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Finds the mouse position on the world
        Vector2 dir = (Vector2)((worldToMousePos - transform.position)); // sets direction by calculating the difference between the location of mouse to position of striker
        dir.Normalize(); // vector keeps the same direction but its length is 1.0.
        rb2d.AddForce(dir * strikeSpeed); // Direction * Speed of Striker = Movement; Just to see if moving the striker works.
    }

}

// Notes: For talcum powder I can set the linear drag and increase it.