using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
  public GameObject deathSplosion;

  void Start()
  {

  }

  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      Destroy(other.gameObject);
      Instantiate(deathSplosion, other.transform.position, other.transform.rotation);
    }
  }
}
