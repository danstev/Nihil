using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public void NewGame()
    {
        Debug.Log("New Game pressed.");
    }

    public void JoinGame()
    {
        Debug.Log("Join Game pressed.");
    }

    public void Options()
    {
        Debug.Log("Options pressed.");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game pressed.");
        Application.Quit();
    }


}
