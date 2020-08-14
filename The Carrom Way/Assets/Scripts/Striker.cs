using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class Striker : MonoBehaviour
{
    public event Action UpdatePlayerTurn;

    [Header("Striker")]
    public int strikerSpeed = 25;
    [Space]

    [Header("Arrow")]
    [SerializeField]
    private GameObject arrow;

    [Range(2, 40)]
    public float arrowRotationSpeed;
    [Range(0.1f, 1.5f)]
    public float arrowRotationSmoothness;
    [Space]

    [Header("Power Icon")]
    [SerializeField]
    private GameObject powerIcon;
    [Range(0.4f, 1.8f)]
    public float iconScaleMin;
    [Range(0.4f, 1.8f)]
    public float iconScaleMax;

    [Space]
    [Header("Canvas UI")]
    public PositionSlider slider;

    [Space]
    [Header("Direction")]
    [SerializeField]
    private GameObject directionPoint;
    new Rigidbody2D rb2d;
    Collider2D col;
    AudioSource audi;
    [Header("Private")]
    Vector3 worldMousePos;
    Vector3 StrikerInitialPosition;
    [Header("Boolean")]
    bool shotDone = false;
    bool isAiming = true;

    [HideInInspector]
    public int playerID;

    public string puckColor { get; private set; }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audi = GetComponent<AudioSource>();
        StrikerInitialPosition = transform.position;
        powerIcon.SetActive(false);
        arrow.SetActive(false);

        if (playerID == (int)Players.player1)
        {
            puckColor = "White";
        }
        else
        {
            puckColor = "Black";
        }

    }

    void Update()
    {

        if (shotDone)
        {
            rb2d.drag += Time.deltaTime * 2; //2

            if (rb2d.IsSleeping())
            {
                Reset();
            }
        }
        else
        {
            if (isAiming)
                col.isTrigger = true;
            else
                col.isTrigger = false;
            transform.position = Vector3.Lerp(transform.position, new Vector3(PositionSlider.instance.getValue, transform.position.y, transform.position.z), 0.15f);
        }
    }

    

    private void OnMouseDown()
    {
        powerIcon.SetActive(true);
        arrow.SetActive(true);
        isAiming = false;
    }
    
    private void OnMouseDrag()
    {
        float scale;

        scale = Vector2.Distance((Vector2)worldMousePos, (Vector2)transform.position);
        scale = Mathf.Clamp(scale, iconScaleMin, iconScaleMax);
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = worldMousePos - (arrow.transform.position);


        float angle = 90 - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, arrowRotationSmoothness);

        powerIcon.transform.localScale = Vector3.Lerp(powerIcon.transform.localScale, new Vector3(scale, scale, scale), 0.25f);
        strikerSpeed = (int)(powerIcon.transform.localScale.x * 22f);
    }

    void updateStrikerPosition()
    {

    }

    private void OnMouseUp()
    {
        powerIcon.SetActive(false);
        arrow.SetActive(false);
        Shoot();

    }
    
    void Shoot()
    {
        GameTracker.instance.ClearList();
        Vector2 dir = (Vector2)((directionPoint.transform.position - transform.position));
        dir.Normalize();
        rb2d.AddForce(dir * strikerSpeed, ForceMode2D.Impulse);
        shotDone = true;

    }
    // THIS WILL set to true to false depends on if the player has potted the correct piece
    // i-e Player 1 potted white piece and not the black
    public bool puckToHole { get; set; }

    private void Reset()
    {
        transform.position = new Vector3(transform.position.x, StrikerInitialPosition.y, transform.position.z);
        shotDone = false;
        rb2d.drag = 0;
        isAiming = true;
        if (!puckToHole)
        {
            if (!Settings.instance.isPracticeMode)
                TurnPlayed();
        }
        else
        {
            puckToHole = false;
        }

        GameTracker.instance.ReplaceFouledPiece();

        // changing the Opacity - 
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        sr.color = newColor;


    }

    void TurnPlayed()
    {
        GameManager.instance.ChangeTurn();
    }
}
