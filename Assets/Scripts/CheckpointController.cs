using UnityEngine;

public class CheckpointController : MonoBehaviour
{
  public Sprite flagClosed;
  public Sprite flagOpen;
  public bool checkpointActive;
  private SpriteRenderer spriteRenderer;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      spriteRenderer.sprite = flagOpen;
      checkpointActive = true;
    }
  }
}
