using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_gallery : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Gallery");
    }
}
