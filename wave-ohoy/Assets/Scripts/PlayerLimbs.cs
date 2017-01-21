using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbs : MonoBehaviour {

    public enum PirateType
    {
        LightPirate,
        MediumPirate,
        HeavyPirate,
        TallPirate
    }

    public PirateType myPirateType;

    public GameObject head;
    public GameObject headOrigin;

    private float headBobValue;
    public float headBobValueMax = 0.1f;
    private bool  headBobUp;
    public float headBobSpeed;

    public float jumpHeight;
    public float movementSpeed;
    public Rigidbody2D playerRigidBody2D;

    List<SpriteRenderer> allSpriteRenderers;



    //Feet
    public float footCircleSpeed;
    public Vector3 leftFootOffset;
    public Vector3 rightFootOffset;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject leftFootOrigin;
    public GameObject rightFootOrigin;
    public float gotoStandingStillSpeedFeet;
    private bool moving;
    private float leftFootAngle;
    private float rightFootAngle;

    //Hands
    public float handCircleSpeed;
    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandOrigin;
    public GameObject rightHandOrigin;
    public float gotoStandingStillSpeedHands;
    private float leftHandAngle;
    private float rightHandAngle;

    public enum Walking
    {
        StandingStill,
        WalkingLeft,
        WalkingRight,
        Jumping,
        Falling,
        Landing
    }

    public Walking myCurrentWalking;
    // Use this for initialization
    void Start () {

        switch (myPirateType)
        {
            case PirateType.LightPirate:

                break;
            case PirateType.MediumPirate:

                break;
            case PirateType.HeavyPirate:

                break;
            case PirateType.TallPirate:

                break;
        }


        playerRigidBody2D = GetComponent<Rigidbody2D>();
        allSpriteRenderers = new List<SpriteRenderer>();
        //allSpriteRenderers.Add(GetComponent<UnityJellySprite>().m_Sprite);
        for (int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                allSpriteRenderers.Add(sr);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (headBobUp)
        {
            if (headBobValue >= headBobValueMax)
            {
                headBobUp = false;
            }
            else
            {                
                headBobValue += Time.deltaTime * headBobSpeed;
            }
        }
        else
        {
            if (headBobValue <= -headBobValueMax)
            {
                headBobUp = true;
            }
            else
            {
                headBobValue -= Time.deltaTime * headBobSpeed;
            }
        }
        head.transform.position = headOrigin.transform.position + new Vector3(0, headBobValue, 0);
        moving = false;
        /*
        if (Input.GetKey(KeyCode.A) == true)
        {
            StartWalkLeft();
            playerRigidBody2D.AddForce(Vector3.left * movementSpeed, ForceMode2D.Impulse);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StartWalkRight();
            playerRigidBody2D.AddForce(Vector3.right * movementSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidBody2D.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        }

        if(moving == false)
        {
            StopWalking();
        }
        */
        switch (myCurrentWalking)
        {
            case Walking.StandingStill:
                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedFeet);
                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedFeet);

                rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedHands);
                leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedHands);
                break;
            case Walking.WalkingLeft:
                leftFoot.transform.RotateAround(leftFootOrigin.transform.position, new Vector3(0, 0, 1), leftFootAngle);
                rightFoot.transform.RotateAround(rightFootOrigin.transform.position, new Vector3(0, 0, 1), rightFootAngle);
                leftFootAngle += footCircleSpeed;
                rightFootAngle += footCircleSpeed;
                leftFoot.transform.up = Vector3.up;
                rightFoot.transform.up = Vector3.up;

                leftHand.transform.RotateAround(leftHandOrigin.transform.position, new Vector3(0, 0, 1), leftHandAngle);
                rightHand.transform.RotateAround(rightHandOrigin.transform.position, new Vector3(0, 0, 1), rightHandAngle);
                leftHandAngle += handCircleSpeed;
                rightHandAngle += handCircleSpeed;
                leftHand.transform.up = Vector3.up;
                rightHand.transform.up = Vector3.up;
                break;
            case Walking.WalkingRight:
                leftFoot.transform.RotateAround(leftFootOrigin.transform.position, new Vector3(0, 0, 1), leftFootAngle);
                rightFoot.transform.RotateAround(rightFootOrigin.transform.position, new Vector3(0, 0, 1), rightFootAngle);
                leftFootAngle -= footCircleSpeed;
                rightFootAngle -= footCircleSpeed;
                leftFoot.transform.up = Vector3.up;
                rightFoot.transform.up = Vector3.up;

                leftHand.transform.RotateAround(leftHandOrigin.transform.position, new Vector3(0, 0, 1), leftHandAngle);
                rightHand.transform.RotateAround(rightHandOrigin.transform.position, new Vector3(0, 0, 1), rightHandAngle);
                leftHandAngle -= handCircleSpeed;
                rightHandAngle -= handCircleSpeed;
                leftHand.transform.up = Vector3.up;
                rightHand.transform.up = Vector3.up;
                break;
            case Walking.Jumping:

                break;
            case Walking.Falling:

                break;
            case Walking.Landing:

                break;
        }
    }
    public void StartJump()
    {
        //offsetta nedåt
    }
    public void StartFall()
    {
        //offsetta uppåt
    }
    public void Land()
    {
        //offsetta nedåt och sedan till normal
    }
    public void StartWalkLeft()
    {
        FlipSprites(true);
        moving = true;
        myCurrentWalking = Walking.WalkingLeft;

        leftFoot.transform.position = leftFootOrigin.transform.position + leftFootOffset;
        rightFoot.transform.position = rightFootOrigin.transform.position + rightFootOffset;
        leftHand.transform.position = leftHandOrigin.transform.position + leftHandOffset;
        rightHand.transform.position = rightHandOrigin.transform.position + rightHandOffset;
    }
    public void StartWalkRight()
    {
        FlipSprites(false);
        moving = true;
        myCurrentWalking = Walking.WalkingRight;
        leftFoot.transform.position = leftFootOrigin.transform.position + leftFootOffset;
        rightFoot.transform.position = rightFootOrigin.transform.position + rightFootOffset;
        leftHand.transform.position = leftHandOrigin.transform.position + leftHandOffset;
        rightHand.transform.position = rightHandOrigin.transform.position + rightHandOffset;
    }
    public void StopWalking()
    {
        moving = false;
        myCurrentWalking = Walking.StandingStill;
    }
    private void FlipSprites(bool flipTo)
    {
        for (int i = 0; i < allSpriteRenderers.Count; i++)
        {
            allSpriteRenderers[i].flipX = flipTo;
        }
    }
}
