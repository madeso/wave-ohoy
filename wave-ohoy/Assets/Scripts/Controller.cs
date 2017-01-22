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

    public AudioClip jumpSound;
    public AudioClip drowningSound;
    public AudioClip yellingSound;

    AudioSource ass;

    public bool isDead = false;

	void Start () {
        myRb = GetComponent<Rigidbody2D>();
        myPlayerLimbs = GetComponent<PlayerLimbs>();

        myJumpsLeft = 0;
        myIsOnShip = false;

        myShip = GameObject.FindGameObjectWithTag("ship").GetComponent<Rigidbody2D>();

        ass = GetComponent<AudioSource>();
	}
	
	void Update () {
        myRb.AddForce(new Vector2(
            Input.GetAxis("Horizontal" + myPlayerIndex),
            0f
        ) * 1f * myPlayerLimbs.movementSpeed, ForceMode2D.Impulse);

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
            myRb.AddForce(new Vector2(0f, myPlayerLimbs.jumpHeight), ForceMode2D.Impulse);
            myJumpsLeft--;
            myPlayerLimbs.StartJump();

            ass.clip = jumpSound;
            ass.pitch = 0.8f + 0.4f * Random.value;
            ass.Play();

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


    public void ManOverboard()
    {
        if(Random.value < 0.25f)
        {
            ass.clip = yellingSound;
            ass.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ass.clip = drowningSound;
        ass.pitch = 0.8f + 0.4f * Random.value;
        ass.Play();

        GameObject[] pirates = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < pirates.Length; i++)
        {
            if(pirates[i] != this)
            {
                pirates[i].SendMessage("ManOverboard");
            }
        }

        isDead = true;

        GameObject.Find("GameManager").SendMessage("Drown");

    }
}
