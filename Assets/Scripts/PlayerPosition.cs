using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPosition : MonoBehaviour
{
    public Vector3 initialPosition;
    public SwitchingMechanic Switcher;

    private void Start()
    {
        initialPosition = transform.position;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        initialPosition = transform.position;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            Singleton.Instance.AudioManager.Play("die");
            transform.position = initialPosition;
            Switcher.ResetObjectValues();
        }
    }
}