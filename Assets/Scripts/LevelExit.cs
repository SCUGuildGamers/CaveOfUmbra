using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelName = "Tutorial";

    void OnTriggerEnter2D(Collider2D col) 
    {
        SceneManager.LoadScene(levelName);
    }
}
