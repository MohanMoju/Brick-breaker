
using UnityEngine;
using UnityEngine.SceneManagement;

public class REstart : MonoBehaviour
{
 public void ChangeScene()
 {
    SceneManager.LoadScene(0);
    Destroy(GameManager.Instance.gameObject);
 }
}
