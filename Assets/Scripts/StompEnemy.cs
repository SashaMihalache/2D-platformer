using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
  public GameObject deathSplosion;
  public float bounceForce;
  private Rigidbody2D playerRigidBody;

  void Start()
  {
    playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
  }

  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      other.gameObject.SetActive(false);
      Instantiate(this.deathSplosion, other.transform.position, other.transform.rotation);
      this.playerRigidBody.velocity = new Vector3(this.playerRigidBody.velocity.x, this.bounceForce, 0f);
    }
  }
}
