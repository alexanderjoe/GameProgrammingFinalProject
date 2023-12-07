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
    bool facingR = true;
    bool deductTime = false;

    public float timeRemaining = 3.0f;

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
        }
        if (Input.GetKey(KeyCode.J))//KeyCode.Mouse0))
        {
            attack1 = true;
            attack2 = false;
            Debug.Log("ATTACK1");
            deductTime = true;
        }
        else if (Input.GetKey(KeyCode.K))//Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("ATTACK2");
            attack2 = true;
            attack1 = false;
            deductTime = true;
        }
        else if(timeRemaining <= 0)
        {
            attack1 = false;
            attack2 = false;
            timeRemaining = 3.0f;
            deductTime = false;
        }

        if (deductTime && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

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
        if (xDirection > 0 && !facingR)
        {
            Flip();
        }
        else if (xDirection < 0 && facingR)
        {
            Flip();
        }
        
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

        /*
         * fast = false;
        attack1 = false;
        attack2 = false;
        isHit = false;
        isDead = false;
        facingR = true;
        */
        Debug.Log("Setting player animation state from  attack1: "+attack1+" attack2: "+attack2);
        if (xDirection == 0.0f)
        {
            player_animation_state = AnimationStateEnum.Idle;
            Debug.Log(" Idle \n");
        }
        else if (attack1)
        {
            player_animation_state = AnimationStateEnum.Attack1;
            Debug.Log(" attack1 \n");
        }
        else if (attack2)
        {
            player_animation_state = AnimationStateEnum.Attack2;
            Debug.Log(" attack2 where enum is: "+AnimationStateEnum.Attack2);
        }
        else if (isHit)
        {
            player_animation_state = AnimationStateEnum.Hit;
            Debug.Log(" ishit \n");
        } else if (fast)
        {
            player_animation_state = AnimationStateEnum.Sprint;
            Debug.Log(" sprinting \n");
        } else if (isDead)
        {
            player_animation_state= AnimationStateEnum.Dead;
            Debug.Log(" dead \n");
        }
        else
        {
            player_animation_state = AnimationStateEnum.Run;
            Debug.Log(" running \n");
        }

        animator.SetInteger(PLAYER_ANIMATION_STATE, (int)player_animation_state);
    }

    private void Flip()
    {
        facingR = !facingR;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
