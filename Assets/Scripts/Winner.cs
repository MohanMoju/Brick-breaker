using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
 public void Wins()
 {
    SceneManager.LoadScene(0);
 }
}
