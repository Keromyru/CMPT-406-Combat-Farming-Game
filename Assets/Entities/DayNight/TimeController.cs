using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Setting and Moving time
namespace Timer
{
    public class TimeController : MonoBehaviour
    {   
        [SerializeField] private TimeObject timeObject;

        private float _secondsPerTick;  //seconds per hour
        private float _nextTick;

        //Setting up the timeObject and starting at the begining 
        private void Start()
        {
            //Get Start values from the save file?
            timeObject.Setup(
                totalTicks:0,
                daysElapsed: 0,
                ticksToday: 0,
                isNight: false
            );

            _secondsPerTick = timeObject.SecondsPerTick;
        }

        private void Update()
        {
            //If the current time is less than the next tick time - do nothing
            if(Time.time < _nextTick) 
            {
                return;
            }

            //Set the next time to the current time + secondsPerTick
            _nextTick = Time.time + _secondsPerTick;    //Time in the game (Time.time) and real life time (_secondsPerTick) 
            timeObject.Ticks();    //Let time run (increase tick count)
        }
    }
}