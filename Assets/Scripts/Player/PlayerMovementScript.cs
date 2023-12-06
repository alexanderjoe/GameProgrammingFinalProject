using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    const string PLAYER_ANIMATION_STATE = "Player_States";
    Animator animator;
    float moveSpeed = 5.0f;
    Rigidbody2D rb;
    public float xDirection = 0.0f;
    float yDirection = 0.0f;
    bool fast;
    bool facingL;
    bool facingR;
    
    //set these from game, probably get set from trigger collider
    bool attack1;
    bool attack2;
    bool isHit;
    bool isDead;

    private enum AnimationStateEnum
    {
        Dead = -1,
        Idle = 0,
        Jump = 1,
        Run = 2,
        Sprint = 3,
        Hit = 4,
        Attack2 = 5,
        Attack1 = 6
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fast = false;
        attack1 = false;
        attack2 = false;
        isHit = false;
        isDead = false;
        facingR = true;
        facingL = false;
    }

    // Update is called once per frame
    void Update()
    {
        yDirection = Input.GetAxisRaw("Vertical");
        xDirection = Input.GetAxisRaw("Horizontal");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            fast = true;
        } else
        {
            fast = false;
            //Debug.Log("Set fast back to FALSE");
        }
        //Flip();
        SetAnimationState();
    }

    private void FixedUpdate()
    {
        if (fast)
        {
            rb.velocity = new Vector2(xDirection * (moveSpeed*2), yDirection * (moveSpeed*2));
        }
        else
        {
            rb.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);
        }

        if(xDirection < 0.0f && !facingL)
        {
            facingL = true;
            facingR = false;
            Flip();
        } else if(xDirection > 0.0f && !facingR)
        {
            facingR = true;
            facingL = false;
            Flip();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public float getSpeed()
    {
        return moveSpeed;
    }

    public void setSpeed(float spd)
    {
        moveSpeed = spd;
    }

    private void SetAnimationState()
    {
        AnimationStateEnum player_animation_state;
        //Debug.Log("Setting player animation state to: ");
        if (xDirection == 0.0f)
        {
            player_animation_state = AnimationStateEnum.Idle;
            //Debug.Log(" Idle \n");
        }
        else if (attack1)
        {
            player_animation_state = AnimationStateEnum.Attack1;
            //Debug.Log(" attack1 \n");
        }
        else if (attack2)
        {
            player_animation_state = AnimationStateEnum.Attack2;
            //Debug.Log(" attack2 \n");
        }
        else if (isHit)
        {
            player_animation_state = AnimationStateEnum.Hit;
            //Debug.Log(" ishit \n");
        } else if (fast)
        {
            player_animation_state = AnimationStateEnum.Sprint;
            //Debug.Log(" sprinting \n");
        } else if (isDead)
        {
            player_animation_state= AnimationStateEnum.Dead;
            //Debug.Log(" dead \n");
        }
        else
        {
            player_animation_state = AnimationStateEnum.Run;
            //Debug.Log(" running \n");
        }

        animator.SetInteger(PLAYER_ANIMATION_STATE, (int)player_animation_state);
    }

    private void Flip()
    {
        //I think this is all I need
        //animator.transform.Rotate(0, 180, 0);

        if (facingL)
        {
            facingL = false;
            facingR = true;
            animator.transform.Rotate(0, 180, 0);
        } else if (facingR)
        {
            facingL = true;
            facingR = false;
            animator.transform.Rotate(0, 180, 0);
        }
    }
}
