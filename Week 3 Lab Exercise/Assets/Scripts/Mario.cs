using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
    // Variables set in the inspector
    [SerializeField]
    float mWalkSpeed;
    [SerializeField]
    float mRunSpeed;
    [SerializeField]
    float mJumpForce;

    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    // Booleans used to coordinate with the animator's state machine
    bool mRunning;
    bool mMoving;
    bool mGrounded;
    bool mFalling;
    bool mJumping;

    // References to other components (can be from other game objects!)
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;
    Transform mSpriteChild;
    Transform mGroundCheck;

    // used to change direction
    Vector2 currentDirection = new Vector2(0, 0);
    float horizontalDirection = 0.0f;
    Vector3 updatedPosition;

    void Start ()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mSpriteChild = transform.Find ("MarioSprite");
        mGroundCheck = transform.Find ("GroundCheck");
    }

    void Update ()
    {
        CheckGrounded ();
        CheckFalling();
        CheckJumping();
        CheckRunning();
        MoveCharacter ();
        

        // Update animator's variables
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isMoving", mMoving);
        mAnimator.SetBool("isJumping", mJumping);
        mAnimator.SetBool("IsFalling", mFalling);

        // TODO: Tell animator if game object is grounded or not (use the variable "mGrounded")

        // TODO: Tell animator if game object is falling or not (use the variable "mFalling")
    }

    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        foreach(Collider2D col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                mGrounded = true;
                return;
            }
        }
        mGrounded = false;
    }

    private void MoveCharacter()
    {
        // TODO: Check if the player wants Mario to run (see input manager)
        //       and set the value of "mRunning" accordingly
        currentDirection.x = 0;
        
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && mGrounded)
        {
            mRunning = true;
        }

        if (horizontalDirection != 0.0f)
        {
            mMoving = true;
            UpdateHorizontalPosition();
            UpdateFaceDirection();
        }
        else
        {
            mMoving = false;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && mGrounded)
        {
            currentDirection.y = mJumpForce;
            mRigidBody2D.AddForce(currentDirection);
        }




        //FaceDirection(new Vector2(dir,0));




        // TODO: Make Mario move when the player presses Left or Right!
        //       Also, move Mario walk/run at the appropriate speed.
        //       Use the variables "mWalkSpeed" and "mRunSpeed"!
        //       Don't forget to flip Mario when necessary (use the FaceDirection() function)

        // TODO: If Mario is on the ground, allow him to jump!
        //       Make use of the "mGrounded" variable, whose value is being changed by the CheckGrounded() function
    }

    /// <summary>
    /// Updates FaceDirection
    /// </summary>
    private void UpdateFaceDirection()
    {
        currentDirection.x = horizontalDirection;
        currentDirection.y = 0;
        FaceDirection(currentDirection);
    }

    private void UpdateHorizontalPosition()
    {
        Vector3 updatedPosition = transform.position;
        updatedPosition.x += horizontalDirection * (mRunning ? mRunSpeed : mWalkSpeed) * Time.deltaTime;
        transform.position = updatedPosition;
    }

    private void CheckFalling()
    {
        mFalling = mRigidBody2D.velocity.y < 0.0f;
    }

    private void CheckRunning()
    {
        mRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void CheckJumping()
    {
        mJumping = mRigidBody2D.velocity.y > 0.0f;
    }

    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite (NOTE: Vector3.forward is positive Z in 3D. The Sprite is on XY plane!)
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back); 
        mSpriteChild.rotation = rotation3D;
    }
}


