using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEnd : MonoBehaviour
{
    public Animation end;
    public GameObject CreditsUI;
    private void Update() 
    {
        if(Input.anyKeyDown)
        {
            StopCredits();
        }
    }
    public void StopCredits()
    {
        end.Stop();
        CreditsUI.SetActive(false);
    }
}
