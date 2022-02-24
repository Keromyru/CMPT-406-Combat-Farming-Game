using UnityEngine;
using Tooling;

namespace Timer
{
    public class TimeAware : MonoBehaviour
    {
        [SerializeField] private TimeObject timeObject; //Reference to timeObject
        //Three time events that can happen
        [SerializeField] private UnityIntEvent onTick;
        [SerializeField] private UnityIntEvent onDayTick;
        [SerializeField] private UnityIntEvent onNightTick;

        private void Start()
        {
            //Subscribing the onTickHandlers to the timeObject callbacks
            timeObject.onTick += OnTickHandler;
            timeObject.onDayTick += OnDayTickHandler;
            timeObject.onNightTick += OnNightTickHandler;
        }

        private void OnDestory()
        {
            //Removing the callbacks
            //This part might cause a propblem with Unity - not always recommended to remove delegates
            timeObject.onTick -= OnTickHandler;
            timeObject.onDayTick -= OnDayTickHandler;
            timeObject.onNightTick-= OnNightTickHandler;
        }

        //Connecting the timeObjects to the TimeAware behavour
        private void OnTickHandler(int ticksToday)
        {
            onTick.Invoke(ticksToday);
        }

        private void OnDayTickHandler(int daysElapsed)
        {
            onDayTick.Invoke(daysElapsed);
        }

        private void OnNightTickHandler(int DaysElapsed)
        {
            onNightTick.Invoke(daysElapsed);
        }
    }
}
