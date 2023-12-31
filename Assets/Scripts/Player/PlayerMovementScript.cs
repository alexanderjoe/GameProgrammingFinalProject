using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public AudioClip[] walkClips;
    public AudioClip[] runClips;
    private AudioSource _audioSource;

    const string PLAYER_ANIMATION_STATE = "Player_States";
    Animator animator;
    public float moveSpeed = 5.0f;
    Rigidbody2D rb;
    public float xDirection = 0.0f;
    public float yDirection = 0.0f;
    bool fast;
    bool facingR = true;

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
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            fast = true;
        }
        else
        {
            fast = false;
        }

        SetAnimationState();

        if (xDirection != 0 || yDirection != 0)
        {
            if (fast)
            {
                PlayRunSound();
            }
            else
            {
                PlayWalkSound();
            }
        }
    }

    void PlayWalkSound()
    {
        var clip = walkClips[Random.Range(0, walkClips.Length)];
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(clip);
    }

    void PlayRunSound()
    {
        var clip = runClips[Random.Range(0, runClips.Length)];
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(clip);
    }

    private void FixedUpdate()
    {
        if (fast)
        {
            rb.velocity = new Vector2(xDirection * (moveSpeed * 2), yDirection * (moveSpeed * 2));
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

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float spd)
    {
        moveSpeed = spd;
    }

    private void SetAnimationState()
    {
        AnimationStateEnum player_animation_state = AnimationStateEnum.Idle;

        /*
         * fast = false;
        attack1 = false;
        attack2 = false;
        isHit = false;
        isDead = false;
        facingR = true;
        */
        //Debug.Log("Setting player animation state from  attack1: " + attack1);//+" attack2: "+attack2);
        if (xDirection == 0.0f && yDirection == 0.0f && !attack1 && !attack2)
        {
            player_animation_state = AnimationStateEnum.Idle;
            //Debug.Log(" Idle \n");
        }

        if (!fast && (xDirection != 0 || yDirection != 0))
        {
            player_animation_state = AnimationStateEnum.Run;
            //Debug.Log(" running \n");
        }

        if (isHit)
        {
            player_animation_state = AnimationStateEnum.Hit;
            //Debug.Log(" ishit \n");
        }

        if (fast)
        {
            player_animation_state = AnimationStateEnum.Sprint;
            //Debug.Log(" sprinting \n");
        }

        if (isDead)
        {
            player_animation_state = AnimationStateEnum.Dead;
            //Debug.Log(" dead \n");
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