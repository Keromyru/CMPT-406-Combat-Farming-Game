using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //Using text mesh for the clock display
using UnityEngine.Rendering;  //Used to access the volume component

public class DayNightCycle : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;  //Display time
    public TextMeshProUGUI dayDisplay;  //Display day
    private Volume ppv;  //Post processing volume

    public float tick;  //Increasing the tick, increases second rate, Infinity is the fastest possible input
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;

    public bool activateLights;  //Check if lights are on
    public GameObject[] lights;  //All the lights turn on when dark
    //public SpriteRenderer[] lanternWithLights;  //lantern sprites (use if you want something to apear only at night or during the day)

    //Start is called before the first frame update
    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();

        //Make sure point light is off at the start
        if(activateLights == true)  //If lights are on
        {  
            for(int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);  //Turn all the lights off
            }
            activateLights = false; 
        }
    }

    //Update is called once per frame
    void FixedUpdate()  //Used fixed update, since update is frame dependant.
    {
        CalcTime();
        DisplayTime(); 
    }

    public void CalcTime()  //Used to calculate sec, min and hours
    {
        seconds += Time.fixedDeltaTime * tick;  //multiply time between fixed update by tick

        if(seconds >= 60)  //60sec = 1min
        {                                                                                   
            seconds = 0;
            mins += 1;
        }
        if(mins >= 60)  //60min = 1 hour
        {
            mins = 0;
            hours += 1;
        }
        if(hours >= 24)  //24hr = 1 day
        {
            hours = 0;
            days += 1;
        }
        ControlPPV();   //Changes post processing volume after calculation
    }

    public void ControlPPV()  //Used to adjust the post processing slider.
    {
        //ppv.weight = 0;
        if(hours >= 21 && hours < 22)  //Dusk at 21:00 (9:00pm) - untill 22:00 (10:00pm)
        {
            ppv.weight = (float)mins / 60;  //Since dusk is 1hr, we just divide the min by 60 which will slowly increase from 0 - 1

            /*
            for(int i = 0; i < lanternWithLights.Length; i++)  
            {
                lanternWithLights[i].color = new Color(lanternWithLights[i].color.r, lanternWithLights[i].color.g, lanternWithLights[i].color.b, (float)mins / 60);  //Change the alpha value of the laterns so they become visible
            }
            */

            if(activateLights == false)  //If lights havent been turned on
            {
                if(mins > 45)  //Wait untill pretty dark
                {
                    for(int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);  //Turn all the lights on
                    }
                    activateLights = true;
                }
            }
        }

        if(hours >= 6 && hours < 7)  //Dawn at 6:00 (6:00am) - until 7:00 (7:00am)
        {
            ppv.weight = 1 - (float)mins / 60;  //We minus 1 because we want it to go from 1 - 0
            /*
            for(int i = 0; i < lanternWithLights.Length; i++)
            {
                lanternWithLights[i].color = new Color(lanternWithLights[i].color.r, lanternWithLights[i].color.g, lanternWithLights[i].color.b, 1 - (float)mins / 60);  //Make lanternWithLights invisible
            }
            */
            if(activateLights == true)  //If lights are on
            {
                if(mins > 45)  //Wait untill pretty bright
                {
                    for(int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false);  //Turn lights off
                    }
                    activateLights = false;
                }
            }
        }
    }

    public void DisplayTime()  //Shows time and day in Unity Inspector
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);  //The formatting ensures that there will always be 0's in empty spaces
        dayDisplay.text = "Day: " + days;  //Display day counter
    }
}
