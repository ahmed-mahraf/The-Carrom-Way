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
        rb2d = GetComponent<Rigidbody2D>();
        arrowTransform = ArrowDirection.transform;
    }

    // Update is called once per frame
    void Update()
    {
        arrowTransform.LookAt(Input.mousePosition, arrowTransform.up);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("S has been pressed!");
            rb2d.AddForce(transform.up * strikeSpeed); // Transform Up * Speed of Striker = Movement; Just to see if moving the striker works.
        }

        if (rb2d.velocity.magnitude < 0.2f) // Set arrow when magnitude is less than 0.2 in value.
        {
            ArrowDirection.SetActive(true);
        }
        else
        {
            ArrowDirection.SetActive(false);
        }
    }
}

// Notes: For talcum powder I can set the linear drag and increase it.