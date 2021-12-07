using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private float moveSpeed = 8f;

    [SerializeField]
    private float jumpForce = 20f;

    [SerializeField]
    private Transform groundCheckPoint;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private BulletController shotToFire;

    [SerializeField]
    private Transform shotPoint;

    [SerializeField]
    public float dashSpeed;

    [SerializeField]
    public float dashTime;

    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private SpriteRenderer afterImage;

    [SerializeField]
    private float afterImageLifetime;

    [SerializeField]
    private float timeBetweenAfterImages;

    [SerializeField]
    private Color afterImageColour;

    [SerializeField]
    private float waitAfterDashing = 0.25f;

    [SerializeField]
    private GameObject standing;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Animator ballAnim;

    [SerializeField]
    private float waitToBall;

    [SerializeField]
    private Transform bombPoint;

    [SerializeField]
    private GameObject bomb;

    private bool canMove;

    private float ballCounter;

    private float dashRechargeCounter;

    private bool canDoubleJump;
    private float dashCounter;
    private float afterImageCounter;

    private Vector3 rightFacingScale = new Vector3(1f, 1f, 1f);
    private Vector3 leftFacingScale = new Vector3(-1f, 1f, 1f);

    private PlayerAbilityTracker playerAbilityTracker;

    void Start()
    {
        //Application.targetFrameRate = 60;
        playerAbilityTracker = GetComponent<PlayerAbilityTracker>();
        canMove = true;
    }

    void Update()
    {
        bool isTouchingGround = GetIsTouchingGround();

        if (canMove)
        {

            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Fire2") && standing.activeSelf && playerAbilityTracker.CanDash)
            {
                dashCounter = dashTime;
                dashRechargeCounter = waitAfterDashing;

                ShowAfterImage();
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                playerRigidbody.velocity = new Vector2(dashSpeed * transform.localScale.x, playerRigidbody.velocity.y);

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }

            }
            else
            {
                if (MoveHorizontally(out float horizontalSpeed, out bool shouldFaceRight))
                {
                    FacePlayer(shouldFaceRight);
                }
            }

            Jump(isTouchingGround);

            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    BulletController shot = Instantiate(shotToFire, shotPoint.position, shotPoint.rotation);
                    shot.SetMoveDirection(new Vector2(transform.localScale.x, 0f));

                    anim.SetTrigger("shotFired");
                }
                else if (ball.activeSelf && playerAbilityTracker.CanDropBomb)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                }

            }

            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -0.9f && playerAbilityTracker.CanBecomBall)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0.9f)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
        }
        else
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        if (standing.activeSelf)
        {
            UpdateAnimator(Mathf.Abs(playerRigidbody.velocity.x), isTouchingGround);
        }

        if (ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));
        }

    }

    public void SetAnimEnabled(bool isEnabled)
    {
        anim.enabled = isEnabled;
    }

    public void SetCanMove(bool playerCanMove)
    {
        canMove = playerCanMove;
    }

    private void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = playerSpriteRenderer.sprite;
        image.color = afterImageColour;
        image.transform.localScale = transform.localScale;
        Destroy(image.gameObject, afterImageLifetime);
        afterImageCounter = timeBetweenAfterImages;
    }

    private bool MoveHorizontally(out float horizontalSpeed, out bool isFacingRight)
    {
        playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, playerRigidbody.velocity.y);

        horizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x);
        isFacingRight = playerRigidbody.velocity.x > Mathf.Epsilon;
        return (horizontalSpeed > Mathf.Epsilon);
    }

    private void FacePlayer(bool faceRight)
    {
        transform.localScale = faceRight ? rightFacingScale : leftFacingScale;
    }

    private void Jump(bool isTouchingGround)
    {
        if (Input.GetButtonDown("Jump") && (isTouchingGround || (canDoubleJump && playerAbilityTracker.CanDoubleJump)))
        {
            if (isTouchingGround)
            {
                canDoubleJump = true;
            }
            else
            {
                anim.SetTrigger("doubleJump");
                canDoubleJump = false;
            }

            playerRigidbody.velocity = new Vector2(0, jumpForce);
        }
    }

    private void UpdateAnimator(float speed, bool isOnGround)
    {
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", speed);
    }

    private bool GetIsTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundLayer);
    }
}
