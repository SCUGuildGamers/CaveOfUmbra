using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public KeyCode resetkey = KeyCode.R;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(resetkey))
        {
            // Get the name of the currently active scene
            string currentscene = SceneManager.GetActiveScene().name;

            // Reload the current scene
            SceneManager.LoadScene(currentscene);
        }
    }
}

