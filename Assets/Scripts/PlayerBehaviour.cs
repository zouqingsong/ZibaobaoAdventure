using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    //A reference to the Rigidbody component
    private Rigidbody rb;

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
	
	// Update is called once per frame
	void Update ()
	{
        //check if we're moving the side
	    var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
        rb.AddForce(horizontalSpeed, 0, rollSpeed);
	}
}
