using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public Transform groundCheck;
  public float moveSpeed;
  public float jumpSpeed;
  public float groundCheckRadius;
  public bool isGrounded;
  public LayerMask whatIsGround;
  private Rigidbody2D rb;
  private Animator anim;
  public Vector3 respawnPosition;
  public LevelManager levelManager;
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    levelManager = FindObjectOfType<LevelManager>();

    respawnPosition = transform.position;
  }

  void Update()
  {
    HandleHorizontalMovement();
    HandleJumpMovement();

    anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    anim.SetBool("Grounded", isGrounded);
  }

  void HandleJumpMovement()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0f);
    }
  }

  void HandleHorizontalMovement()
  {
    if (Input.GetAxisRaw("Horizontal") > 0f)
    {
      rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);
      transform.localScale = new Vector3(1f, 1f, 1f);
    }
    else if (Input.GetAxisRaw("Horizontal") < 0f)
    {
      rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
      transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    else
    {
      rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "KillPlane")
    {
      levelManager.Respawn();
    }

    if (other.tag == "Checkpoint")
    {
      respawnPosition = other.transform.position;
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "MovingPlatform")
    {
      transform.parent = other.transform;
    }
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "MovingPlatform")
    {
      transform.parent = null;
    }
  }
}
