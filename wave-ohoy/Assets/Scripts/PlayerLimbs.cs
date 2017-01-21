using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbs : MonoBehaviour {

    public enum WalkingState
    {
        StandingStill,
        WalkingLeft,
        WalkingRight,
    }

    public enum CharacterJumpState
    {
        None,
        Jumping,
        Falling,
        Landing
    }
    public WalkingState myCurrentCharacterWalkingState = WalkingState.StandingStill;
    public CharacterJumpState myCurrentCharacterJumpState = CharacterJumpState.None;
    private Rigidbody2D playerRigidBody2D;
    private List<SpriteRenderer> allSpriteRenderers;

    public float jumpHeight;
    public float movementSpeed;

    //Head
    public GameObject head;
    public GameObject headOrigin;
    private float headBobValue;
    public float headBobValueMax = 0.1f;
    private bool  headBobUp;
    public float headBobSpeed;

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
    public Vector3 feetRotationDistance;

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


    private bool jumping;
    private bool falling;
    private bool landing;

    public Vector3 jumpingOffset;
    public Vector3 fallingOffset;
    public Vector3 landingOffset;

    public float jumpingOffsetRecoverStrength;
    public float fallingOffsetRecoverStrength;
    public float landingOffsetRecoverStrength;

    private bool landingHandsReached;

    public float characterPreviousY;


    // Use this for initialization
    void Start () {

        playerRigidBody2D = GetComponent<Rigidbody2D>();
        allSpriteRenderers = new List<SpriteRenderer>();
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

        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            StartJump();
            playerRigidBody2D.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        }

        if (moving == false)
        {
            StopWalking();
        }
        */
        switch (myCurrentCharacterJumpState)
        {
            case CharacterJumpState.None:
                break;
            case CharacterJumpState.Jumping:
                leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position + jumpingOffset, Time.deltaTime * jumpingOffsetRecoverStrength);
                rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position + jumpingOffset, Time.deltaTime * jumpingOffsetRecoverStrength);

                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position + jumpingOffset, Time.deltaTime * jumpingOffsetRecoverStrength);
                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position + jumpingOffset, Time.deltaTime * jumpingOffsetRecoverStrength);

                if (characterPreviousY > transform.position.y)
                {
                    myCurrentCharacterJumpState = CharacterJumpState.Falling;
                }
                break;
            case CharacterJumpState.Falling:
                leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position + fallingOffset, Time.deltaTime * fallingOffsetRecoverStrength);
                rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position + fallingOffset, Time.deltaTime * fallingOffsetRecoverStrength);

                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position + fallingOffset, Time.deltaTime * fallingOffsetRecoverStrength);
                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position + fallingOffset, Time.deltaTime * fallingOffsetRecoverStrength);
                break;
            case CharacterJumpState.Landing:
                if (landingHandsReached == false)
                {
                    leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position + landingOffset, Time.deltaTime * landingOffsetRecoverStrength);
                    rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position + landingOffset, Time.deltaTime * landingOffsetRecoverStrength);

                    leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position + landingOffset, Time.deltaTime * landingOffsetRecoverStrength);
                    rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position + landingOffset, Time.deltaTime * landingOffsetRecoverStrength);


                    if (Vector3.Distance(leftHand.transform.position, leftHandOrigin.transform.position + landingOffset) < 0.01f)
                    {
                        landingHandsReached = true;
                    }
                }
                else
                {
                    leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position, Time.deltaTime * landingOffsetRecoverStrength);
                    rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position, Time.deltaTime * landingOffsetRecoverStrength);

                    leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position, Time.deltaTime * landingOffsetRecoverStrength);
                    rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position, Time.deltaTime * landingOffsetRecoverStrength);

                    if (Vector3.Distance(leftHand.transform.position, leftHandOrigin.transform.position) < 0.1f)
                    {
                        landingHandsReached = false;
                        jumping = false;
                        falling = false;
                        landing = false;
                        StopLanding();
                    }
                }
                break;
        }
        switch (myCurrentCharacterWalkingState)
        {
            case WalkingState.StandingStill:
                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, leftFootOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedFeet);
                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rightFootOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedFeet);

                rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedHands);
                leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandOrigin.transform.position, Time.deltaTime * gotoStandingStillSpeedHands);
                break;
            case WalkingState.WalkingLeft:
                leftFoot.transform.RotateAround(leftFootOrigin.transform.position, feetRotationDistance, leftFootAngle);
                rightFoot.transform.RotateAround(rightFootOrigin.transform.position, feetRotationDistance, rightFootAngle);
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
            case WalkingState.WalkingRight:
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
        }

        characterPreviousY = transform.position.y;
    }

    private void StopLanding()
    {
        myCurrentCharacterJumpState = CharacterJumpState.None;
    }

    public void StartJump()
    {
        if (jumping == true)
        {
            return;
        }
        landingHandsReached = false;
        falling = false;
        landing = false;
        moving = true;
        jumping = true;
        headBobSpeed = headBobSpeed * 10 ;
        myCurrentCharacterJumpState = CharacterJumpState.Jumping;
    }
    public void StartWalkLeft()
    {
        FlipSprites(true);
        moving = true;
        myCurrentCharacterWalkingState = WalkingState.WalkingLeft;

        leftFoot.transform.position = leftFootOrigin.transform.position + leftFootOffset;
        rightFoot.transform.position = rightFootOrigin.transform.position + rightFootOffset;
        leftHand.transform.position = leftHandOrigin.transform.position + leftHandOffset;
        rightHand.transform.position = rightHandOrigin.transform.position + rightHandOffset;
    }
    public void StartWalkRight()
    {
        FlipSprites(false);
        moving = true;
        myCurrentCharacterWalkingState = WalkingState.WalkingRight;
        leftFoot.transform.position = leftFootOrigin.transform.position + leftFootOffset;
        rightFoot.transform.position = rightFootOrigin.transform.position + rightFootOffset;
        leftHand.transform.position = leftHandOrigin.transform.position + leftHandOffset;
        rightHand.transform.position = rightHandOrigin.transform.position + rightHandOffset;
    }
    public void StopWalking()
    {
        myCurrentCharacterWalkingState = WalkingState.StandingStill;
        leftFootAngle = 0;
        rightFootAngle = 0;
    }
    private void FlipSprites(bool flipTo)
    {
        for (int i = 0; i < allSpriteRenderers.Count; i++)
        {
            allSpriteRenderers[i].flipX = flipTo;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ship")
        {
            if (myCurrentCharacterJumpState == CharacterJumpState.Falling)
            {
                headBobSpeed = headBobSpeed / 10    ;
            }
            myCurrentCharacterJumpState = CharacterJumpState.None;
            jumping = false;
            falling = false;
            landing = false;
        }
    }
}
