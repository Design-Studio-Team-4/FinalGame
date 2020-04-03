using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void Die()
    {
        Destroy(GameObject.Find("Music Player"));
    }

    
}
