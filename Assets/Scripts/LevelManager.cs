using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public float waitToRespawn;
  public PlayerController thePlayer;
  public GameObject deathSplosion;

  void Start()
  {
    thePlayer = FindObjectOfType<PlayerController>();
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

    thePlayer.transform.position = thePlayer.respawnPosition;
    thePlayer.gameObject.SetActive(true);
  }
}
