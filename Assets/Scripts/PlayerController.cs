using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public Transform groundCheck;
  public float moveSpeed;
  public float jumpSpeed;
  public float groundCheckRadius;
  public bool isGrounded;

  public LayerMask whatIsGround;
  public Vector3 respawnPosition;
  public LevelManager levelManager;
  public GameObject stompBox;
  private Rigidbody2D rb;
  private Animator anim;

  public float knockbackForce;
  public float knockbackLength;
  private float knockbackCounter;

  public float invicibilityLength;
  private float invicibilityCounter;

  public AudioSource jumpSound;
  public AudioSource hurtSound;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    levelManager = FindObjectOfType<LevelManager>();

    respawnPosition = transform.position;
  }

  void Update()
  {
    if (knockbackCounter <= 0)
    {
      HandleHorizontalMovement();
      HandleJumpMovement();
    }
    else
    {
      knockbackCounter -= Time.deltaTime;

      float directionalKnockbackForce = this.IsPlayerFacingRight() ? knockbackForce : -knockbackForce;
      rb.velocity = new Vector3(-directionalKnockbackForce, knockbackForce, 0f);
    }

    HandleInvincibility();

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

  void HandleJumpMovement()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0f);
      jumpSound.Play();
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

  void HandleInvincibility()
  {
    if (invicibilityCounter > 0)
    {
      invicibilityCounter -= Time.deltaTime;
    }
    else
    {
      levelManager.invincible = false;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "KillPlane")
    {
      levelManager.HurtPlayer(levelManager.maxHealth);
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

  public void Knockback()
  {
    knockbackCounter = knockbackLength;
    invicibilityCounter = invicibilityLength;
    levelManager.invincible = true;
  }

  private bool IsPlayerFacingRight()
  {
    return transform.localScale.x > 0;
  }
}
