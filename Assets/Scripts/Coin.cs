using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  private LevelManager theLevelManager;
  public int coinValue;

  void Start()
  {
    theLevelManager = FindObjectOfType<LevelManager>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      theLevelManager.AddCoins(coinValue);
      gameObject.SetActive(false);
    }
  }
}
