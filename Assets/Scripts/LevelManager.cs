using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
  public float waitToRespawn;
  public PlayerController thePlayer;
  public GameObject deathSplosion;
  public int maxLifeCoinThreshold;
  public int coinCount;
  private int coinBonusLifeCount;
  public AudioSource coinSound;
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

  public AudioSource levelMusic;
  public AudioSource levelWonMusic;
  public AudioSource gameOverMusic;

  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();

    this.healthCount = maxHealth;

    objectsToReset = FindObjectsOfType<ResetOnRespawn>();

    InitPlayerData();

    coinText.text = "Coins: " + coinCount;
    livesText.text = "Lives x " + currentLives;
  }

  void Update()
  {
    if (this.healthCount <= 0 && !this.isRespawning)
    {
      this.Respawn();
    }

    if (coinBonusLifeCount >= maxLifeCoinThreshold)
    {
      currentLives += 1;
      livesText.text = "Lives x " + this.currentLives;
      coinBonusLifeCount -= maxLifeCoinThreshold;
    }
  }

  public void InitPlayerData()
  {
    if (PlayerPrefs.HasKey("CoinCount"))
    {
      coinCount = PlayerPrefs.GetInt("CoinCount");
    }

    if (PlayerPrefs.HasKey("PlayerLives"))
    {
      currentLives = PlayerPrefs.GetInt("PlayerLives");
    }
    else
    {
      currentLives = startingLives;
    }

    // PlayerPrefs.DeleteAll();
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
    Debug.Log("intra");

    if (currentLives == 0)
    {
      return;
    }

    gameOverScreen.SetActive(true);
    thePlayer.gameObject.SetActive(false);
    livesText.text = "Lives x 0";

    levelMusic.Stop();
    gameOverMusic.Play();
    currentLives = 0;
  }

  public IEnumerator HandleRespawn()
  {
    this.isRespawning = true;
    currentLives -= 1;
    livesText.text = "Lives x " + currentLives;
    thePlayer.gameObject.SetActive(false);

    Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

    yield return new WaitForSeconds(waitToRespawn);

    healthCount = maxHealth;
    this.UpdateHeartMeter();

    this.coinCount = 0;
    this.coinBonusLifeCount = 0;
    this.coinText.text = "Coins " + this.coinCount;

    this.isRespawning = false;
    thePlayer.transform.position = thePlayer.respawnPosition;
    thePlayer.gameObject.SetActive(true);

    this.ReactivateObjects();
  }

  public void AddCoins(int coinsToAdd)
  {
    coinCount += coinsToAdd;
    coinBonusLifeCount += coinsToAdd;
    coinSound.Play();

    coinText.text = "Coins: " + coinCount;
  }

  public void HurtPlayer(int damageToTake)
  {
    if (!invincible)
    {
      healthCount -= damageToTake;
      this.UpdateHeartMeter();
      thePlayer.Knockback();
      thePlayer.hurtSound.Play();
    }
  }

  public void GiveHealth(int healthToGive)
  {
    healthCount += healthToGive;

    if (healthCount > maxHealth)
    {
      healthCount = maxHealth;
    }

    coinSound.Play();
    this.UpdateHeartMeter();
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
    coinSound.Play();
    livesText.text = "Lives x " + currentLives;
  }
}
