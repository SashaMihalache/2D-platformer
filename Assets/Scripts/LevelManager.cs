﻿using System.Collections;
using System.Collections.Generic;
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

  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();

    coinText.text = "Coins: " + coinCount;

    this.healthCount = maxHealth;

    objectsToReset = FindObjectsOfType<ResetOnRespawn>();
  }

  void Update()
  {
    if (this.healthCount <= 0 && !this.isRespawning)
    {
      this.Respawn();
      this.isRespawning = true;
    }
  }

  public void Respawn()
  {
    StartCoroutine("RespawnCo");
  }

  public IEnumerator RespawnCo()
  {
    thePlayer.gameObject.SetActive(false);

    Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

    yield return new WaitForSeconds(waitToRespawn);

    healthCount = maxHealth;
    this.isRespawning = false;
    this.coinCount = 0;
    this.coinText.text = "Coins " + this.coinCount;
    this.UpdateHeartMeter();

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
    healthCount -= damageToTake;
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
}
