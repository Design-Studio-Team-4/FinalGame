using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject music;

    public void Die()
    {
        Destroy(music);
    }

    
}
