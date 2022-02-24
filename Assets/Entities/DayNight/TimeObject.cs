using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

//Is a scriptable object (template to store data of the same kind ex:multiple baseball cards)
//Using delegates to subscript to diffrent funtions (diffrent ticks)
namespace Timer
{
    public delegate void OnTick(int ticksToday);    //Every hour
    public delegate void OnDayTick(int daysElapsed);    //Every day
    public delegate void OnNightTick(int daysElapsed);  //Every night

    [CreateAssetMenu(fileName = "TimeObject", menuName = "ScriptableObjects/Time Tracker")]

    public class TimeObject : ScriptableObject
    {
        public OnTick onTick = ticks => { };
        public OnDayTick onDayTick = days => { };
        public OnNightTick onNightTick = days => { };

        public int TicksToday {get; private set;}
        public int DaysElapsed {get; private set;}  //How many days have passed
        public int TotalTicks {get; private set;}   //How many hours so far
        public int TicksPerDay {get; private set;}  //How many hours in a day
        public float SecondsPerDay {get; private set;}  
        public float DayNightSplit {get; private set;}    //How many hours in day and how many hours in night   
        public bool IsNight {get; private set;}

        //Set up the day night cycle in unity
        [SerializeField] private FloatReference secondsPerTick;
        [SerializeField] private IntReference ticksPerDay;
        [SerializeField] private FloatReference dayNightSplit;

        //Set the time to a specific moment
        //Useful for saving the time in the game
        public void Setup (
            int totalTicks,
            int daysElapsed,
            int ticksToday,
            bool isNight
        )

        {
            TicksToday = ticksToday;
            DaysElapsed = daysElapsed;
            TotalTicks = totalTicks;
            IsNight = isNight;
            TicksPerDay = ticksPerDay;
            DayNightSplit = dayNightSplit;
            SecondsPerTick = secondsPerTick;
        }

        //Let time run (increase tick count)
        public void Tick(int ticks = 1)
        {
            TotalTicks += ticks;
            TicksToday += ticks;
            OnTick(TicksToday);

            //A day has passed
            if(TicksToday >= TicksPerDay)
            {
                //Handle the case where multiple days pass?
                DaysElapsed ++;
                TicksToday -= TicksPerDay;
                onDayTick(DaysElapsed);
                IsNight = false;
            }
            //It is night time
            else if(TicksToday / (float)TicksPerDay > DayNightSplit)
            {
                IsNight = true;
                onNightTick(DaysElapsed);
            }
        }
    }
}