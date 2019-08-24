using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
  public float moveSpeed;
  private bool canMove;
  private Rigidbody2D myRigidBody;
  void Start()
  {
    this.myRigidBody = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (canMove)
    {
      this.myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0);
    }
  }

  void OnBecameVisible()
  {
    this.canMove = true;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "KillPlane")
    {
      Destroy(this.gameObject);
    }
  }
}
