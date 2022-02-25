using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    void Start()
    {
        //this litterally just starts the script
        gameObject.GetComponent<AudioHandler>().SetPlayList("DayCycle");
        gameObject.GetComponent<AudioHandler>().PlayNext();
    }


}
