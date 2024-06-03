using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad; // The name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
