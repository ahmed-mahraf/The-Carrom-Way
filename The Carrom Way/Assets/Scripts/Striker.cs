using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class Striker : MonoBehaviour
{
    public event Action UpdatePlayerTurn;

    [Header("--- Striker Settings ---")]
    public int strikerSpeed = 25;
    [Space]

    [Header("--- Arrow Settings ---")]
    [SerializeField]
    private GameObject arrow;

    [Range(2, 40)]
    public float arrowRotationSpeed;
    [Range(0.1f, 1.5f)]
    public float arrowRotationSmoothness;
    [Space]

    [Header("--- PowerCircle Settings ---")]
    [SerializeField]
    private GameObject powerCircle;
    [Range(0.4f, 1.8f)]
    public float circleScaleMin;
    [Range(0.4f, 1.8f)]
    public float circleScaleMax;

    [Space]
    [Header("UI REFRENCES")]
    public SliderPos slider;

    [Space]
    [Header("--- Child Refrences ---")]
    [SerializeField]
    private GameObject directionPoint;
    new Rigidbody2D rigidbody;
    Collider2D col;
    AudioSource audi;
    [Header("---- private Variables -----")]
    Vector3 worldMousePos;
    Vector3 StrikerInitialPosition;
    [Header("---- boolean Variables -----")]
    bool shotDone = false;
    bool isAiming = true;

    [HideInInspector]
    public int playerID;

    public string puckColor { get; private set; }


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audi = GetComponent<AudioSource>();
        StrikerInitialPosition = transform.position;
        powerCircle.SetActive(false);
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
            rigidbody.drag += Time.deltaTime * 2;//2


            if (rigidbody.IsSleeping())
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(SliderPos.instance.getValue, transform.position.y, transform.position.z), 0.15f);

        }


    }

    void updateStrikerPosition()
    {

    }
    private void OnMouseDown()
    {
        powerCircle.SetActive(true);
        arrow.SetActive(true);
        isAiming = false;
    }



    private void OnMouseDrag()
    {
        float scale;

        scale = Vector2.Distance((Vector2)worldMousePos, (Vector2)transform.position);
        scale = Mathf.Clamp(scale, circleScaleMin, circleScaleMax);
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = worldMousePos - (arrow.transform.position);


        float angle = 90 - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, arrowRotationSmoothness);

        powerCircle.transform.localScale = Vector3.Lerp(powerCircle.transform.localScale, new Vector3(scale, scale, scale), 0.25f);
        strikerSpeed = (int)(powerCircle.transform.localScale.x * 22f);
    }

    private void OnMouseUp()
    {
        powerCircle.SetActive(false);
        arrow.SetActive(false);
        Shoot();

    }


    void Shoot()
    {
        GameTracker.instance.ClearList();
        Vector2 dir = (Vector2)((directionPoint.transform.position - transform.position));
        dir.Normalize();
        rigidbody.AddForce(dir * strikerSpeed, ForceMode2D.Impulse);
        shotDone = true;

    }
    // THIS WILL set to true to false depends on if the player has potted the correct piece
    // i-e Player 1 potted white piece and not the black
    public bool pottedPiece { get; set; }

    private void Reset()
    {
        transform.position = new Vector3(transform.position.x, StrikerInitialPosition.y, transform.position.z);
        shotDone = false;
        rigidbody.drag = 0;
        isAiming = true;
        pottedPiece = false;

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
