using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{   
    [SerializeField] AudioControllerSO myController;
    void Awake()
    {
            //SoundPlayer.ClearAudio();
            //This is in place of the game trigger
            //gameObject.GetComponent<AudioHandler>().StartPlayList("DayCycle");
            // gameObject.GetComponent<AudioHandler>().PlayNext();
            //gameObject.GetComponent<AudioHandler>().PlaySong("Day_1");
            //Play Audio File From Selected Controller
    }
}
