using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
  public string levelToLoad;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      SceneManager.LoadScene(levelToLoad);
    }
  }
}