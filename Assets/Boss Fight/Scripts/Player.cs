using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float movement;
    [SerializeField] bool lookingRight = true;
    public float movementSpeed;

    [Header("Jumping")]
    public float jumpHeight;
    [SerializeField] bool isGround = true;

    // [Header("Combat Combo")]
    // [SerializeField] int comboCount;

    [Header("Player Components")]
    public Rigidbody2D playerRB;
    public Animator playerAnimator;



    void Update()
    {
        PlayerMovement(); // Horizontal Movement and Animations as well
        LookAroundFunction();  // left and right view
        CheckJump(); // Checks if player can jump and then jumps
        PlayerInputs();

    }

    void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * movementSpeed * Time.fixedDeltaTime;
    }

    void PlayerMovement()
    {
        movement = Input.GetAxis("Horizontal");

        if (Mathf.Abs(movement) > 0.1f)
        {
            playerAnimator.SetFloat("Run", 1f);
        }

        else if (movement < 0.1f)
        {
            playerAnimator.SetFloat("Run", 0f);
        }
    }

    void PlayerInputs()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton2))  // x button
        {
            playerAnimator.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.JoystickButton5)) // Right Bumper button
        {
            playerAnimator.SetTrigger("Parry");
        }
    }


    void LookAroundFunction()
    {
        if (movement < 0f && lookingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            lookingRight = false;
        }

        else if (movement > 0f && !lookingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            lookingRight = true;
        }
    }

    void CheckJump()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && isGround)  // Jump with A button on Controller
        {
            Jump();
            isGround = false;
        }
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        playerAnimator.SetBool("Jump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerAnimator.SetBool("Jump", false);
            isGround = true;
        }
    }
}