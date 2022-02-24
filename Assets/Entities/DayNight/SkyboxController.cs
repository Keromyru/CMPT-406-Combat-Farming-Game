using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timer;

namespace Environment
{
    public class SkyboxController : MonoBehaviour
    {
        [Header("Sky Elements")]
        [SerializeField] private Material daySkybox;
        [SerializeField] private Material nightSkybox;
        [SerializeField] private Light sun;
        [SerializeField] private Color SunLightColor;
        [SerializeField] private Color MoonLightColor;
        [SerializeField] private float maxIntensity;    //Noon high light
        [SerializeField] private float minIntensity;    //Midnight low light

        [Header("Sky Elements")]
        [SerializeField] private TimeObject timeObject;

        private float _sunLightIntensity;

        //Make it day
        public void TurnToDay()
        {
            RenderSettings.skybox = daySkybox;
            sun.color = sunLightColor;
        }

        //Make it night
        public void TurnToNight()
        {
            RenderSettings.skybox = nightSkybox;
            sun.color = moonLightColor;
        }

        public void UpdateLight()
        {
            //Move this to a startup method?
            //Figure out the math of time and light
            var dayTicks = timeObject.TicksPerDay * timeObject.DayNightSplit;
            var nightTicks = timeObject.TicksPerDay * (1 - timeObject.DayNightSplit);
            var maxLight = dayTicks / 2;    //Noon light
            var minLight = dayTicks + nightTicks / 2;   //Midnight light
            var lightVariation = (maxIntensity - minIntensity) / (timeObject.TicksPerDay / 2f); //Half the ticks of a day (half going up and half going down)

            //Find a better way to update the light (think sin/cos)
            //Increase the light
            if(timeObject.TicksToday < maxLight)
            {
                sun.intensity += lightVariation;
            }
            //Decrease the light
            else if(timeObject.TicksToday < minLight)
            {
                sun.intensity -= lightVariation;
            }
            else
            {
                sun.intensity += lightVariation;
            }

            sun.intensity = Mathf.Clamp(sun.intensity, minIntensity, maxIntensity);
        }
    }
}


