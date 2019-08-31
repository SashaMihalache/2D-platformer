using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelDoor : MonoBehaviour
{

  public string levelToLoad;
  public bool unlocked;

  public Sprite doorTopOpen;
  public Sprite doorBottomOpen;

  public Sprite doorTopClosed;
  public Sprite doorBottomClosed;

  public SpriteRenderer doorTop;
  public SpriteRenderer doorBottom;

  void Start()
  {
    PlayerPrefs.SetInt("Level1", 1);

    if (PlayerPrefs.GetInt(levelToLoad) == 1)
    {
      unlocked = true;
    }
    else
    {
      unlocked = false;
    }

    if (unlocked)
    {
      doorTop.sprite = doorTopOpen;
      doorBottom.sprite = doorBottomOpen;
    }
    else
    {
      doorTop.sprite = doorTopClosed;
      doorBottom.sprite = doorBottomClosed;
    }
  }

  void Update()
  {

  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      if (Input.GetButtonDown("Jump") && unlocked)
      {
        SceneManager.LoadScene(levelToLoad);
      }
    }
  }
}
