using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


public class PrimaryPlayerController : MonoBehaviour
{

    public float normalSpeed = 6;
    public float normalAcceleration = 64;
    public float normalPassiveDeceleration = 32;

    public float normalGravityScale = 3;
    public float jumpHoldGravityScale = 2;
    public float jumpReleaseGravityScale = 4;
    public float jumpVelocity = 10;
    public int maxAirJumps = 1;

    public float crouchMaxSpeed = 2;
    public float crouchAcceleration = 16;
    public float crouchPassiveDeceleration = 8;
    public float crouchSlideDeceleration = 12;
    public float crouchSlideBoostSpeed = 10;

    public bool glideEnabled = true;
    public float maxGlideFallSpeed = 2;
    public float glideDecceleration = 8;

    private bool facingRight = true;
    private int airJumps = 0;
    private bool isCrouching = false;




    public UnityEngine.InputSystem.InputActionReference jump;
    public UnityEngine.InputSystem.InputActionReference leftRight;
    public UnityEngine.InputSystem.InputActionReference crouch;

    

    private Rigidbody2D body;

    GameObject originalGameObject;

    private PlayerAnimationManager playerAnimationManager;

    Collider2D footCollider;  //Collider to check if player is touching the ground
    Collider2D primaryCollider;  //Collider used for interaction when not crouched
    Collider2D unCrouchTestCollider;  //Collider to test if the space above a crouched player is safe to uncrouch
    Collider2D crouchedCollider;  //Collider used for interaction while crouching
    GameObject characterRootNode;
    
    void OnDisable()
    {
        jump.ToInputAction().started -= onJumpKeyDown;
        crouch.ToInputAction().started -= onCrouchKeyDown;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Creating new player object!");

        originalGameObject = GameObject.Find("Hero");

        footCollider = originalGameObject.transform.Find("footCollider").gameObject.GetComponent<Collider2D>();
        primaryCollider = originalGameObject.GetComponent<Collider2D>();
        unCrouchTestCollider = originalGameObject.transform.Find("unCrouchTestCollider").gameObject.GetComponent<Collider2D>();
        crouchedCollider = originalGameObject.transform.Find("crouchedCollider").gameObject.GetComponent<Collider2D>();
        characterRootNode = originalGameObject.transform.Find("characterRootNode").gameObject;

        playerAnimationManager = gameObject.GetComponentInChildren<PlayerAnimationManager>();

        Debug.Assert(footCollider != null);
        Debug.Assert(primaryCollider != null);
        Debug.Assert(unCrouchTestCollider != null);
        Debug.Assert(crouchedCollider != null);
        Debug.Assert(characterRootNode != null);

        Debug.Assert(playerAnimationManager != null);

        leftRight.ToInputAction().Enable();

        body = GetComponent<Rigidbody2D>();

        jump.ToInputAction().Enable();
        jump.ToInputAction().started += onJumpKeyDown;

        crouch.ToInputAction().Enable();
        crouch.ToInputAction().started += onCrouchKeyDown;

    }

    // Update is called once per frame
    void Update()
    {

        //Rotate the character mesh
        float targetAngle = facingRight ? 0 : 180;
        Vector3 oldRotation = characterRootNode.transform.rotation.eulerAngles;
        float rotationDifference = targetAngle - oldRotation.y;
        float rotationDurration = 0.2f;
        float rotationSpeed = 180*(1/rotationDurration);
        Quaternion newRotation = Quaternion.Euler(oldRotation.x,oldRotation.y+Mathf.Clamp(rotationDifference, -rotationSpeed*Time.deltaTime, rotationSpeed*Time.deltaTime),oldRotation.z);
        characterRootNode.transform.rotation = newRotation;

        //Quaternion newRotation = Quaternion.Euler(oldRotation.x,oldRotation.y+roationDifference*Time.deltaTime*Mathf.PI/rotationSpeed,oldRotation.z);
        //characterRootNode.transform.rotation = newRotation;

    }

    void FixedUpdate() {

        //Check is player is on ground to reset double (or more) jump
        bool inAir = true;
        if(checkGroundCollision()) {
            airJumps = maxAirJumps;
            inAir = false;
        }

        //Handle glide behavior by dampening vertical velocity
        if(airJumps==0 && glideEnabled && body.velocity.y < -maxGlideFallSpeed && jump.ToInputAction().ReadValue<float>()>0.5) {
            float correctionalVerticalAcceleration = -maxGlideFallSpeed - body.velocity.y;
            float clampedCorrectionalVerticalAcceleration = Mathf.Max(-maxGlideFallSpeed, correctionalVerticalAcceleration);
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + clampedCorrectionalVerticalAcceleration);
        }

        //Update crouching status from input and a safety check
        if(crouch.ToInputAction().ReadValue<float>() > 0.5) {
            if(!isCrouching) {
                isCrouching = true;
                primaryCollider.isTrigger = true;
                crouchedCollider.isTrigger = false;
            }
        } else if(isCrouching && !checkGroundCollisionFromCollider(unCrouchTestCollider)) {
            isCrouching = false;
            primaryCollider.isTrigger = false;
            crouchedCollider.isTrigger = true;
        }
        
        //Change gravity scale based on vertical valocity and jump key status.  Allows for variable height jumps
        if(body.velocity.y>0) {
            if(jump.ToInputAction().ReadValue<float>()>0.5) {
                body.gravityScale = jumpHoldGravityScale;
            } else {
                body.gravityScale = jumpReleaseGravityScale;
            }
        } else {
            body.gravityScale = normalGravityScale;
        }

        //Get desired left/right velocity from the input system and the current velocity, compute difference
        float targetHorizontalVelocity = leftRight.ToInputAction().ReadValue<float>()*(isCrouching ? crouchMaxSpeed : normalSpeed);
        float currentHorizontalVelocity = body.velocity.x;
        float correctionalVelocity = targetHorizontalVelocity - currentHorizontalVelocity;
        float correctionalAcceleration = correctionalVelocity;  //Changed, was /Time.FixedTimeDelta, was not needed?  //May cause a divide by 0 error, if so, return early if fixedDeltaTime is 0

        //Debug.Log((correctionalVelocity + currentHorizontalVelocity)/targetHorizontalVelocity);

        //if(Mathf.Abs(correctionalVelocity) <= 0.5 && Mathf.Abs(targetHorizontalVelocity)<=0.15) {  //Prevent jitter when target speed is too close to actual speed
        //    body.velocity = new Vector2(targetHorizontalVelocity, body.velocity.y);
        //    correctionalAcceleration = 0;
        //}

        float clampedCorrectionalAcceleration;
        
        if(isCrouching && Mathf.Abs(currentHorizontalVelocity) > crouchMaxSpeed) {  //Player is crouched, but above max speed, this is a slide, deccelerate slightly
                clampedCorrectionalAcceleration = Mathf.Clamp(correctionalAcceleration,
                                                -crouchSlideDeceleration*Time.fixedDeltaTime,
                                                crouchSlideDeceleration*Time.fixedDeltaTime
                                                );
        } else { //player is not sliding
            if(Mathf.Abs(targetHorizontalVelocity)<Mathf.Abs(currentHorizontalVelocity)) { //Player wants to slow down, or change directions at a lower speed, use deceleration
                if(isCrouching) {
                    clampedCorrectionalAcceleration = Mathf.Clamp(correctionalAcceleration,
                                                    -crouchPassiveDeceleration*Time.fixedDeltaTime,
                                                    crouchPassiveDeceleration*Time.fixedDeltaTime
                                                    );
                } else {
                    clampedCorrectionalAcceleration = Mathf.Clamp(correctionalAcceleration,
                                                    -normalPassiveDeceleration*Time.fixedDeltaTime,
                                                    normalPassiveDeceleration*Time.fixedDeltaTime
                                                    );
                }
            } else {  // Player wants to speed up, use normal acceleration
                if(isCrouching) {
                    clampedCorrectionalAcceleration = Mathf.Clamp(correctionalAcceleration,
                                                    -crouchAcceleration*Time.fixedDeltaTime,
                                                    crouchAcceleration*Time.fixedDeltaTime
                                                    );
                } else {
                    clampedCorrectionalAcceleration = Mathf.Clamp(correctionalAcceleration,
                                                    -normalAcceleration*Time.fixedDeltaTime,
                                                    normalAcceleration*Time.fixedDeltaTime
                                                    );
                }
            }
        }

        //TODO: Check for dash status here
        body.velocity = new Vector2(body.velocity.x + clampedCorrectionalAcceleration, body.velocity.y);

        
        //Determine and set animation
        if(inAir) {
            playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.FALL);
        } else {
            if(body.velocity.y >= jumpVelocity*0.75) {
                //Do nothing, this is likely the beginning of a jump
            } 
            else if(Mathf.Abs(currentHorizontalVelocity)>=crouchMaxSpeed/2 || Mathf.Abs(targetHorizontalVelocity)>0.2f) {  //If the player is moving or trying to move
                //Player is moving or trying to move
                if(isCrouching) {
                    if(Mathf.Abs(currentHorizontalVelocity) >= 1.1*crouchMaxSpeed) {
                        playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.SLIDE);
                    } else {
                        playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.CROUCH_RUN);
                    }
                } else {
                    playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.RUNNING);
                }
            } else {
                //Player is standing still
                if(isCrouching) {
                    playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.CROUCH);
                } else {
                    playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.IDLE);
                }
            }
        }

        //Check direction, apply to local flag
        if(Mathf.Abs(currentHorizontalVelocity)>=crouchMaxSpeed/2) {
            facingRight = currentHorizontalVelocity > 0;
        } else if(Mathf.Abs(targetHorizontalVelocity)>0.1f) {
            facingRight = targetHorizontalVelocity > 0;
        }

    }

    bool checkGroundCollision() {
        return checkGroundCollisionFromCollider(footCollider);
    }

    bool checkGroundCollisionFromCollider(Collider2D source) {
        bool isOnGround = false;
        List<Collider2D> footHitResults = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        source.OverlapCollider(filter, footHitResults);
        foreach(Collider2D result in footHitResults) {
            if(result.gameObject.tag.Equals("Ground") && !result.isTrigger) {
                isOnGround = true;
            }
        }
        return isOnGround;
    }

    void onJumpKeyDown(InputAction.CallbackContext context) {
        if(checkGroundCollision()) {
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
            playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.JUMP);
        } else if(airJumps > 0) {
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
            playerAnimationManager.setMovementAnimation(PlayerAnimationManager.animationNames.JUMP);
            airJumps--;
        }
    }

    void onCrouchKeyDown(InputAction.CallbackContext context) {
        if(Mathf.Abs(body.velocity.x) >= normalSpeed*0.75 && checkGroundCollision()) {
            body.velocity = new Vector2(body.velocity.x>0 ? crouchSlideBoostSpeed : -crouchSlideBoostSpeed,body.velocity.y);
        }
    }

}
