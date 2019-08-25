using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
  public float waitToRespawn;
  public PlayerController thePlayer;
  public GameObject deathSplosion;
  public int coinCount;
  public Text coinText;
  public Image heart1;
  public Image heart2;
  public Image heart3;
  public Sprite heartFull;
  public Sprite heartHalf;
  public Sprite heartEmpty;
  public int maxHealth;
  public int healthCount;
  private bool isRespawning;
  public ResetOnRespawn[] objectsToReset;
  public bool invincible;
  public int startingLives;
  public int currentLives;
  public Text livesText;
  public GameObject gameOverScreen;

  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();

    coinText.text = "Coins: " + coinCount;

    this.healthCount = maxHealth;

    objectsToReset = FindObjectsOfType<ResetOnRespawn>();

    this.currentLives = this.startingLives;
    livesText.text = "Lives x " + this.currentLives;
  }

  void Update()
  {
    if (this.healthCount <= 0 && !this.isRespawning)
    {
      this.Respawn();
    }
  }

  public void Respawn()
  {
    if (currentLives - 1 <= 0)
    {
      HandleGameOver();
    }
    else
    {
      StartCoroutine("HandleRespawn");
    }
  }

  public void HandleGameOver()
  {
    gameOverScreen.SetActive(true);
    thePlayer.gameObject.SetActive(false);
    livesText.text = "Lives x 0";
  }

  public IEnumerator HandleRespawn()
  {
    currentLives -= 1;
    livesText.text = "Lives x " + currentLives;
    this.isRespawning = true;
    thePlayer.gameObject.SetActive(false);

    Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

    yield return new WaitForSeconds(waitToRespawn);

    healthCount = maxHealth;
    this.UpdateHeartMeter();

    this.coinCount = 0;
    this.coinText.text = "Coins " + this.coinCount;

    this.isRespawning = false;
    thePlayer.transform.position = thePlayer.respawnPosition;
    thePlayer.gameObject.SetActive(true);

    this.ReactivateObjects();
  }

  public void AddCoins(int coinsToAdd)
  {
    coinCount += coinsToAdd;

    coinText.text = "Coins: " + coinCount;
  }

  public void HurtPlayer(int damageToTake)
  {
    if (!invincible)
    {
      healthCount -= damageToTake;
      this.UpdateHeartMeter();
      thePlayer.Knockback();
    }
  }

  public void UpdateHeartMeter()
  {
    switch (healthCount)
    {
      case 6:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartFull;
        return;
      case 5:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartHalf;
        return;
      case 4:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartEmpty;
        return;
      case 3:
        heart1.sprite = heartFull;
        heart2.sprite = heartHalf;
        heart3.sprite = heartEmpty;
        return;
      case 2:
        heart1.sprite = heartFull;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        return;
      case 1:
        heart1.sprite = heartHalf;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        return;
      case 0:
        heart1.sprite = heartEmpty;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        return;
      default:
        heart1.sprite = heartEmpty;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        return;
    }
  }

  public void ReactivateObjects()
  {
    for (int i = 0; i < objectsToReset.Length; i++)
    {
      objectsToReset[i].gameObject.SetActive(true);
      objectsToReset[i].ResetObject();
    }
  }

  public void AddLives(int livesToAdd)
  {
    currentLives += livesToAdd;
    livesText.text = "Lives x " + currentLives;
  }
}
