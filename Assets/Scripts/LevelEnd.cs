using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
  public string levelToLoad;
  public string levelToUnlock;

  private PlayerController thePlayer;
  private CameraController theCamera;
  private LevelManager theLevelManager;
  public float waitToMove;
  public float waitToLoad;
  private bool movePlayer;

  public Sprite flagOpen;
  private SpriteRenderer spriteRenderer;

  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();
    theCamera = FindObjectOfType<CameraController>();
    theLevelManager = FindObjectOfType<LevelManager>();

    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (movePlayer == true)
    {
      thePlayer.rb.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.rb.velocity.y, 0f);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      spriteRenderer.sprite = flagOpen;
      StartCoroutine("LevelEndCo");
    }
  }

  public IEnumerator LevelEndCo()
  {
    thePlayer.canMove = false;
    theCamera.followTarget = false;
    theLevelManager.invincible = true;

    theLevelManager.levelMusic.Stop();
    theLevelManager.levelWonMusic.Play();

    thePlayer.rb.velocity = Vector3.zero;

    yield return new WaitForSeconds(waitToMove);

    PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
    PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);

    PlayerPrefs.SetInt(levelToUnlock, 1);

    movePlayer = true;

    yield return new WaitForSeconds(waitToLoad);
    SceneManager.LoadScene(levelToLoad);
  }
}