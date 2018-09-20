using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public string sceneChange;

    public void Interact(GameObject g)
    {
        SceneManager.LoadScene(sceneChange, LoadSceneMode.Single);
    }
}
