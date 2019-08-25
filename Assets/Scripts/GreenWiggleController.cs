using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour
{
  public Rigidbody2D myRigidBody;
  public Transform leftPoint;
  public Transform rightPoint;
  public float moveSpeed;
  public bool movingRight;

  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (movingRight && transform.position.x > rightPoint.position.x)
    {
      movingRight = false;
    }
    if (!movingRight && transform.position.x < leftPoint.position.x)
    {
      movingRight = true;
    }
    if (movingRight)
    {
      myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
    }
    else
    {
      myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
    }
  }
}
