using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_quit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
