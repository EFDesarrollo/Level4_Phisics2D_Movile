using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D pivote;
    public float reactionTime;

    private Camera cam;
    [SerializeField]
    private Rigidbody2D ballRb;
    private bool isSliding;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballRb == null)
            return;
        //TouchDragAndDrop();
        MouseDragAndDrop();
        //Debug.Log(worldTouchPosition);
    }
    private void ThrowBall()
    {
        // la bola reacciona a la fisica
        ballRb.isKinematic = false;
    }
    void MouseDragAndDrop()
    {
        if (!Input.GetMouseButton(0))
        {
            if (isSliding)
            {
                ThrowBall();
            }
            isSliding = false;
            return;
        }
        isSliding = true;
        Vector2 worldClickPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (worldClickPosition.y < -2.8) return;
        ballRb.position = worldClickPosition;
    }
    void TouchDragAndDrop()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isSliding)
            {
                ThrowBall();
            }
            isSliding = false;
            return;
        }
        // tomar control de la bola
        isSliding = true;
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 worldTouchPosition = new Vector2(cam.ScreenToWorldPoint(touchPosition).x, cam.ScreenToWorldPoint(touchPosition).y);
        if (worldTouchPosition.y <= -2.8)
            return;
        ballRb.position = worldTouchPosition;
    }
}
