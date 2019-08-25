using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public Transform groundCheck;
  public float moveSpeed;
  private float activeMoveSpeed;
  public float jumpSpeed;
  public float groundCheckRadius;
  public bool isGrounded;
  public bool canMove;

  public LayerMask whatIsGround;
  public Vector3 respawnPosition;
  public LevelManager levelManager;
  public GameObject stompBox;
  public Rigidbody2D rb;
  private Animator anim;

  public float knockbackForce;
  public float knockbackLength;
  private float knockbackCounter;

  public float invicibilityLength;
  private float invicibilityCounter;

  private bool onPlatform;
  public float onPlatformSpeedModifier;

  public AudioSource jumpSound;
  public AudioSource hurtSound;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    respawnPosition = transform.position;

    levelManager = FindObjectOfType<LevelManager>();

    activeMoveSpeed = moveSpeed;

    canMove = true;
  }

  void Update()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    if (knockbackCounter <= 0 && canMove)
    {
      // if on platform
      activeMoveSpeed = onPlatform ? moveSpeed * onPlatformSpeedModifier : moveSpeed;

      // if right or left
      if (Input.GetAxisRaw("Horizontal") > 0f)
      {
        rb.velocity = new Vector3(activeMoveSpeed, rb.velocity.y, 0f);
        transform.localScale = new Vector3(1f, 1f, 1f);
      }
      else if (Input.GetAxisRaw("Horizontal") < 0f)
      {
        rb.velocity = new Vector3(-activeMoveSpeed, rb.velocity.y, 0f);
        transform.localScale = new Vector3(-1f, 1f, 1f);
      }
      else
      {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
      }

      // if jumping
      if (Input.GetButtonDown("Jump") && isGrounded)
      {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0f);
        jumpSound.Play();
      }
    }

    // if in knockback
    if (knockbackCounter > 0)
    {
      knockbackCounter -= Time.deltaTime;

      if (transform.localScale.x > 0)
      {
        rb.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
      }
      else
      {
        rb.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
      }
    }

    // invincibility
    if (invicibilityCounter > 0)
    {
      invicibilityCounter -= Time.deltaTime;
    }

    if (invicibilityCounter <= 0)
    {
      levelManager.invincible = false;
    }

    anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    anim.SetBool("Grounded", isGrounded);

    if (rb.velocity.y < 0)
    {
      stompBox.SetActive(true);
    }
    else
    {
      stompBox.SetActive(false);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "KillPlane")
    {
      // levelManager.HurtPlayer(levelManager.maxHealth);
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
      onPlatform = true;
    }
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "MovingPlatform")
    {
      transform.parent = null;
      onPlatform = false;
    }
  }

  public void Knockback()
  {
    knockbackCounter = knockbackLength;
    invicibilityCounter = invicibilityLength;
    levelManager.invincible = true;
  }
}
