using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public void StopBgm()
    {
        GetComponent<AudioSource>().Stop();
    }

    
}
