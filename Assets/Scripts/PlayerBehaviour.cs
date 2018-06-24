using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    //A reference to the Rigidbody component
    private Rigidbody rb;

    public enum MobileHorizMovement { Accelerometer, ScreenTouch}

    public MobileHorizMovement HorizMovement = MobileHorizMovement.Accelerometer;

    [Header("Swipe Properties")]
    [Tooltip("How far will the player moe upon swiping")]
    public float swipeMove = 2f;

    [Tooltip("How far must the player swipe before we will execute the action (in pixel space)")]
    public float minSwipeDistance = 2f;

    private Vector2 touchStart;

    // How fast the ball moves left/right
    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    // How fast the ball moves forwards automatically
    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0,10)]
    public float rollSpeed = 5;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	}

    float CalculateMovement(Vector3 pixelPos)
    {
        var worldPos = Camera.main.ScreenToViewportPoint(pixelPos);
        float xMove = worldPos.x < 0.5f ? -1 : 1;
        return xMove * dodgeSpeed;
    }

    void SwipeTeleport(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            var touchEnd = touch.position;
            var x = touchEnd.x - touchStart.x;

            if (Mathf.Abs(x) < minSwipeDistance)
            {
                return;
            }

            Vector3 moveDirection = x < 0?Vector3.left:Vector3.right;

            RaycastHit hit;
            if (!rb.SweepTest(moveDirection, out hit, swipeMove))
            {
                rb.MovePosition(rb.position + (moveDirection * swipeMove));
            }
        }
    }

    static void TouchObjects(Vector3 pixelPos)
    {
        Ray touchRay = Camera.main.ScreenPointToRay(pixelPos);
        RaycastHit hit;

        if (Physics.Raycast(touchRay, out hit))
        {
            //call the playertouch function if it exists on a compoment attached to this object
            hit.transform.SendMessage("PlayerTouch", SendMessageOptions.DontRequireReceiver);
        }
    }
    // Update is called once per frame
    void Update ()
	{
        //check if we're moving the side

	    float horizontalSpeed = 0;
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
	    if (Input.GetMouseButton(0))
	    {
	        TouchObjects(Input.mousePosition);
	        horizontalSpeed = CalculateMovement(Input.mousePosition);
	    }
#elif UNITY_IOS || UNITY_ANDROID
	    if (HorizMovement == MobileHorizMovement.Accelerometer)
	    {
	        horizontalSpeed = Input.acceleration.x * dodgeSpeed;
	    }
        if (Input.touchCount > 0)
	    {
	        Touch myTouch = Input.touches[0];
            TouchObjects(myTouch.position);
            if (HorizMovement == MobileHorizMovement.ScreenTouch)
	        {
	            horizontalSpeed = CalculateMovement(myTouch.position);
	        }
	        SwipeTeleport(myTouch);
	    }
#endif
        rb.AddForce(horizontalSpeed, 0, rollSpeed);
	}
}
