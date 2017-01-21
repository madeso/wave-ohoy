using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    Rigidbody2D myRb;
    public int myPlayerIndex;

    public int myMaxJumps;

    private int myJumpsLeft;

    PlayerLimbs myPlayerLimbs;

    Rigidbody2D myShip;
    private bool myIsOnShip;

	void Start () {
        myRb = GetComponent<Rigidbody2D>();
        myPlayerLimbs = GetComponent<PlayerLimbs>();

        myJumpsLeft = 0;
        myIsOnShip = false;

        myShip = GameObject.FindGameObjectWithTag("ship").GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        myRb.AddForce(new Vector2(
            Input.GetAxis("Horizontal" + myPlayerIndex),
            0f
        ) * 10f, ForceMode2D.Force);

        if(Input.GetAxis("Horizontal" + myPlayerIndex) < 0f)
        {
            myPlayerLimbs.StartWalkLeft();
        }
        else if(Input.GetAxis("Horizontal" + myPlayerIndex) > 0f)
        {
            myPlayerLimbs.StartWalkRight();
        }
        else
        {
            myPlayerLimbs.StopWalking();
        }

        if(Input.GetButtonDown("Jump"+myPlayerIndex) && myJumpsLeft > 0)
        {
            myRb.AddForce(new Vector2(0f, 4f), ForceMode2D.Impulse);
            myJumpsLeft--;
            myPlayerLimbs.StartJump();
            if(myIsOnShip)
            {
                myShip.AddTorque(-transform.position.x*20f, ForceMode2D.Impulse);
            }
        }
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.rigidbody.tag == "ship")
        {
            myIsOnShip = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.tag == "ship")
        {
            myJumpsLeft = myMaxJumps;
            myIsOnShip = true;
        }
    }
}
