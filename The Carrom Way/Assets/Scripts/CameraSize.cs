using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    public SpriteRenderer board; // Declares the board as sprite

    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = board.size.x * Screen.height / Screen.width * 0.5f; // Orthographic size of board
        print("Board Size: " + board.size.x);
        print("screen Height: " + Screen.height);
        print("screen Width: " + Screen.width);
        print("Screen.height / Screen.width = " + (Screen.height / Screen.width));
        print("graphic.size.x * Screen.height / Screen.width  * 0.5f = " + orthoSize);
        Camera.main.orthographicSize = orthoSize; // Camera will fit the board
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
