using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{   

    [SerializeField] AudioControllerSO myController;
    void Start()
    {
        //This is in place of the game trigger
        gameObject.GetComponent<AudioHandler>().SetPlayList("DayCycle");
        gameObject.GetComponent<AudioHandler>().PlayNext();

        //Play Audio File From Selected Controller
        myController.Play("Menu Select");

    }


}
