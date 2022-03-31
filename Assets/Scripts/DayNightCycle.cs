using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //Using text mesh for the clock display
using UnityEngine.Rendering;  //Used to access the volume component

public class DayNightCycle : MonoBehaviour
{
    // event delegate for others to subscribe too
    public delegate void IsDay();
    public delegate void IsNight();

    /* 
        Anything that needs to know what time of day it is, needs to subscribe to these events
        can be done by having a specific method that adds a function to these two below like:
        DayNightCycle.isNowDay += myDayEventInDifferentScript;
        DayNightCycle.isNowNight += myNightEventInDifferentScript;

        If someones does not want to forever be subscribed it can remove its reference with a 
        DayNightCycle.isNowDay -= myDayEventInDifferentScript;

        Typically this with onEnable and onDisable however i am sure that can be done at specific points
        where it makes sense
    */
    public static event IsDay isNowDay;
    public static event IsNight isNowNight;

    private bool daytime;


    public script_DayNightTracker clockTracker;

    // public TextMeshProUGUI timeDisplay;  //Display time
    // public TextMeshProUGUI dayDisplay;  //Display day
    private Volume ppv;  //Post processing volume

    [Header("Tick")]
    public float tick;  //Increasing the tick, increases second rate, Infinity is the fastest possible input
    public float oldTick;  //Keep track of original tick
    public float eclipseRate;  //What to times the tick by for the Eclipse
    public bool activeEclipse = false;  //Check if it is an Eclipse

    [Header("Start Time")]
    public float seconds = 0;
    public int minutes = 0;
    public int hours = 0;
    public int days = 1;
	
	[Header("Day-Night Switch Time")]
	public int dayStart = 0; // When the day light starts
	public int dayEnd = 0; // When the day light ends

    // [Header("Lights")]
    // public bool activateLights;  //Check if lights are on
    // public GameObject[] lights;  //All the lights turn on when dark
    //public SpriteRenderer[] lanternWithLights;  //lantern sprites (use if you want something to apear only at night or during the day)

    //Start is called before the first frame update
    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();

        //Make sure point light is off at the start
        // if(activateLights == true)  //If lights are on
        // {  
        //     for(int i = 0; i < lights.Length; i++)
        //     {
        //         lights[i].SetActive(false);  //Turn all the lights off
        //     }
        //     activateLights = false; 
        // }
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
            minutes += 1;
        }
        if(minutes >= 60)  //60min = 1 hour
        {
            minutes = 0;
            hours += 1;
        }
        // it is now day time
        if( hours >= dayStart && hours < dayEnd )  // Change when day ends and when day starts
        {
            clockTracker.swapDayNight( true );  // Tell tracker it is day
            if (daytime != true) {
                daytime = true;
                isNowDay();
            }
        }
        /// it is now night time
		if( hours < dayStart || hours >= dayEnd )
		{
			clockTracker.swapDayNight( false ); // Tell tracker it is night
            if (daytime == true) {
                daytime = false;
                isNowNight();
            }
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
        // changes this so the dynamic lighting happens depending on what we change
        // so the day end then the hour after will darken the screen
        if(hours >= dayEnd && hours < dayEnd + 1)  //Dusk at 21:00 (9:00pm) - untill 22:00 (10:00pm)
        {
            ppv.weight = (float)minutes / 60;  //Since dusk is 1hr, we just divide the min by 60 which will slowly increase from 0 - 1

            /*
            for(int i = 0; i < lanternWithLights.Length; i++)  
            {
                lanternWithLights[i].color = new Color(lanternWithLights[i].color.r, lanternWithLights[i].color.g, lanternWithLights[i].color.b, (float)mins / 60);  //Change the alpha value of the laterns so they become visible
            }
            */

            // if(activateLights == false)  //If lights havent been turned on
            // {
            //     if(minutes > 45)  //Wait untill pretty dark
            //     {
            //         for(int i = 0; i < lights.Length; i++)
            //         {
            //             lights[i].SetActive(true);  //Turn all the lights on
            //         }
            //         activateLights = true;
            //     }
            // }
        }

        //Night Time for 22:00 (10:00pm)
        if(hours == 22)
        {
            EndEclipse();
        }

        if(hours >= dayStart && hours < dayStart + 1)  //Dawn at 6:00 (6:00am) - until 7:00 (7:00am)
        {
            ppv.weight = 1 - (float)minutes / 60;  //We minus 1 because we want it to go from 1 - 0
            /*
            for(int i = 0; i < lanternWithLights.Length; i++)
            {
                lanternWithLights[i].color = new Color(lanternWithLights[i].color.r, lanternWithLights[i].color.g, lanternWithLights[i].color.b, 1 - (float)mins / 60);  //Make lanternWithLights invisible
            }
            */
            // if(activateLights == true)  //If lights are on
            // {
            //     if(minutes > 45)  //Wait untill pretty bright
            //     {
            //         for(int i = 0; i < lights.Length; i++)
            //         {
            //             lights[i].SetActive(false);  //Turn lights off
            //         }
            //         activateLights = false;
            //     }
            // }
        }

        //This will break the code, you get stuck at 7;00
        //Day Time 7:00 (7:00am)
        /*
        if(hours == 7)
        {
            StartDay();
        }
        */

        // Random Eclipse start between 8:00 (8:00am) - 18:00 untill (6:00pm)
        if(hours > 7 && hours <= 18 && activeEclipse == false)
        {
            //Get a random time
            int randomTime = Random.Range(8,18);
            if(hours == randomTime){
                //Get a random number
                int randomNumber = Random.Range(0,10000);  // Estimate 35% change
                if(randomNumber == 1)
                StartEclipse();
            }
        }

    }

    public void DisplayTime()  //Shows time and day in Unity Inspector
    {
        // Used for the daynight scene commented it out here to not through errors
        // timeDisplay.text = string.Format("{0:00}:{1:00}", hours, minutes);  //The formatting ensures that there will always be 0's in empty spaces
        // dayDisplay.text = "Day: " + days;  //Display day counter

        clockTracker.setTime(hours, minutes);
    }

    public void StartDay()  //Starting the day for 7:00 (7:00am)
    {
        seconds = 0;
        minutes = 0;
        hours = dayStart;
        // since this method circumvents the normal passage of time need to ensure that this is called
        isNowDay();
        daytime = true;
    }

    public void EndDay()  //Ending the day to progress to night time for 22:00 (10:00pm)
    {
        seconds = 0;
        minutes = 0;
        hours = dayEnd;

        //At night end Eclipse
        if(activeEclipse == true)
        {
            EndEclipse();
        }
        // this method circumvents the normal passage of time need to ensure that this is called
        isNowNight();
        daytime = false;
    }

    public void StartEclipse()  //Starts the Eclipses that causes days to be shortened drastically.
    {
        oldTick = tick;
        tick = tick * eclipseRate;
        activeEclipse = true;
    }

    public void EndEclipse()  //Sets the time speed back to normal.
    {
        if(activeEclipse == true)
        {
            tick = oldTick;
            activeEclipse = false;
        }
    }

    //Get the time to do somthing at the desired time
    //Returns a 24 hour time as an int
    public int GetTime()
    {
        return(hours*100)+(minutes);
    }

    /*
    //How to use
    //Example with do somthing at 7:00am
    int time = GetTime();
    if(time == 700)
    {
        //Do somthing
    }
    */
}
