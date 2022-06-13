using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideScrollerMovement : MonoBehaviour
{
    [Header("Animator and spriteRenderer")]
    [SerializeField] Animator animatorOBJ;
    [SerializeField] string AnimationID = "Move";
    // [SerializeField] string AnimationIDSlide = "Slide";
    // [SerializeField] string AnimationIDDash = "Dash";
    // [SerializeField] SpriteRenderer spriteR;

    [Header("Settings")]
    public float speed = 6;
    private float lastDirection = 0;
    private float actualTimeBetweenKeypress = 0;
    private int numberOfInputs = 0;
    private bool keyWasPressed = false;
    private bool isDashing = false;
    private bool isSliding = false;
    private float actualTimeInSlide = 0;
    
    [Header("Input keys")]
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public KeyCode down = KeyCode.DownArrow;

    [Header("Dash")]
    public float dashSpeed = 12;
    public float timeInDash = 2f;
    private float actualTimeInDash = 0;
    public bool removeDashIfChangeDirection = false;
    public float timeBetweenKeypress = 0.5f;
    public Rigidbody2D myRigidBody;
    public bool noGravityOnDash;
    public string groundTag = "Ground";

    public UnityEvent dashStart;
    public UnityEvent dashEnd;

    [Header("Slide")]
    public float timeInSlide = 0.5f;
    public bool removeSlideIfChangeDirection = false;
    public UnityEvent slideStart;
    public UnityEvent slideEnd;

    [Header("Ground")]
    List<int> collidedWithID = new List<int>();
    bool canGroundSlide = false;
    bool dashedOnMidair = false;

    private float moveHorizontal;
    private float startingGravityScale = 0;
    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        moveHorizontal = 0;
        moveHorizontal += Input.GetKey(left) ? -1 : 0;
        moveHorizontal += Input.GetKey(right) ? 1 : 0;

        /*
        if(moveHorizontal != 0)
        {
            spriteR.flipX = moveHorizontal > 0;
        }
        */
        bool isMoving = moveHorizontal != 0;
        animatorOBJ?.SetBool(AnimationID, isMoving);
        
        Vector3 lastPosition = transform.position;
        float actualSpeed = GetSpeedAndMovement(moveHorizontal, out moveHorizontal);
        transform.position += new Vector3(actualSpeed, 0, 0) * Time.deltaTime * moveHorizontal;
    }

    private float GetSpeedAndMovement(float movingFactor, out float finalMove)
    {
        finalMove = moveHorizontal;
        if (isSliding)
        {
            return Sliding(movingFactor);
        }
        else
        {
            if (isDashing)
            {
                CheckContinuousDash(movingFactor);
                return Dashing(movingFactor);
            }
            else
            {
                return DetectIfDashing(movingFactor);
            }
        }
    }

    private float Sliding(float movingFactor)
    {
        float newDirection = Mathf.Sign(movingFactor);
        actualTimeInSlide += Time.deltaTime;
        if (actualTimeInSlide >= timeInSlide || (removeSlideIfChangeDirection && lastDirection != newDirection))
        {
            EndSlide();
        }
        // Finish with timer
        return dashSpeed;
    }

    private float Dashing(float movingFactor)
    {
        if (canGroundSlide == false) dashedOnMidair = true;
        float newDirection = Mathf.Sign(movingFactor);
        actualTimeInDash += Time.deltaTime;
        if (Input.GetKeyDown(down) && isSliding == false)
        {
            StartSlide();
            return dashSpeed;

        }
        if (actualTimeInDash >= timeInDash || (removeDashIfChangeDirection && lastDirection != newDirection))
        {
            EndDash();
        }
        // Finish with timer
        return dashSpeed;
    }

    private float DetectIfDashing(float movingFactor)
    {
        if (dashedOnMidair == true) return speed;

        // Timer to ignore dash
        actualTimeBetweenKeypress += Time.deltaTime;
        // Count input
        if (movingFactor == 0 && keyWasPressed)
        {
            actualTimeBetweenKeypress = 0;
            keyWasPressed = false;
        }
        if (movingFactor != 0 && keyWasPressed == false)
        {
            numberOfInputs += 1;
            keyWasPressed = true;
            actualTimeBetweenKeypress = 0;
        }
        // Reset dash timer
        if (actualTimeBetweenKeypress >= timeBetweenKeypress)
        {
            numberOfInputs = 0;
            actualTimeBetweenKeypress = 0;
        }
        // 2 inputs = dash
        if (numberOfInputs >= 2)
        {
            StartDash(movingFactor);
            return dashSpeed;
        }

        return speed;
    }

    private void CheckContinuousDash(float movingFactor)
    {
        if (canGroundSlide == false) return;
        // Timer to ignore dash
        actualTimeBetweenKeypress += Time.deltaTime;
        // Count input
        if (movingFactor == 0 && keyWasPressed)
        {
            actualTimeBetweenKeypress = 0;
            keyWasPressed = false;
        }
        if (movingFactor != 0 && keyWasPressed == false)
        {
            numberOfInputs += 1;
            keyWasPressed = true;
            actualTimeBetweenKeypress = 0;
        }
        // Reset dash timer
        if (actualTimeBetweenKeypress >= timeBetweenKeypress)
        {
            numberOfInputs = 0;
            actualTimeBetweenKeypress = 0;
        }
        // 2 inputs = dash
        if (numberOfInputs >= 2)
        {
            StartDash(movingFactor);
        }
    }

    private void StartDash(float movingFactor)
    {
        Debug.Log("Start Dash");
        if (noGravityOnDash && myRigidBody != null)
        {
            if (myRigidBody.gravityScale != 0)
            {
                startingGravityScale = myRigidBody.gravityScale;
            }
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
            myRigidBody.gravityScale = 0;
        }
        dashStart?.Invoke();
        // animatorOBJ?.SetBool(AnimationIDDash, true);
        isDashing = true;
        actualTimeBetweenKeypress = 0;
        actualTimeInDash = 0;
        numberOfInputs = 0;
        lastDirection = Mathf.Sign(movingFactor);
    }

    private void EndDash()
    {
        Debug.Log("End Dash");
        dashEnd?.Invoke();
        // animatorOBJ?.SetBool(AnimationIDDash, false);
        isDashing = false;
        actualTimeInDash = 0;

        if (noGravityOnDash && myRigidBody != null)
        {
            myRigidBody.gravityScale = startingGravityScale;
        }
    }

    private void StartSlide()
    {
        Debug.Log("Start Slide");
        actualTimeInSlide = 0;
        slideStart?.Invoke();
        // animatorOBJ?.SetBool(AnimationIDSlide, true);
        isSliding = true;
    }

    private void EndSlide()
    {
        Debug.Log("End Slide");
        slideEnd?.Invoke();
        // animatorOBJ?.SetBool(AnimationIDSlide, true);
        isSliding = false;
        actualTimeInSlide = 0;
        EndDash();
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag(groundTag))
        {
            dashedOnMidair = false;
            collidedWithID.Add(collisionData.transform.GetInstanceID());
            canGroundSlide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag(groundTag))
        {
            collidedWithID.Remove(collisionData.transform.GetInstanceID());
            if (collidedWithID.Count == 0)
            {
                canGroundSlide = false;
            }
        }
    }
}
