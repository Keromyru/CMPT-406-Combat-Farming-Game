using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] // 0 and 1 is important for the intensity graphs
    public float timeOfDay;
    public float fullDayLength;
    public float startTimeOfDay;
    public Vector3 noon;
    private float timeRate;
    private bool isNight = false;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightIntensityMultipler;
    public AnimationCurve reflectionsIntensityMultipler;

    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        timeOfDay = startTimeOfDay;
    }

    void Update()
    {
        //increment timeOfDay
        timeOfDay += timeRate * Time.deltaTime;

        if(timeOfDay >= 1.0f)
        {
            timeOfDay = 0.0f;
        }

        UpdateTime();
    }

    private void UpdateTime()
    {
        //light rotation
        sun.transform.eulerAngles = (timeOfDay - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (timeOfDay - 0.75f) * noon * 4.0f;
        
        //light intensity
        sun.intensity = sunIntensity.Evaluate(timeOfDay);
        moon.intensity = moonIntensity.Evaluate(timeOfDay);

        //change colours
        sun.color = sunColor.Evaluate(timeOfDay);
        moon.color = moonColor.Evaluate(timeOfDay);

        //lighting and reflections intensity
        RenderSettings.ambientIntensity = lightIntensityMultipler.Evaluate(timeOfDay);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultipler.Evaluate(timeOfDay);

        CheckNightDayTransition();
    }

    public void CheckNightDayTransition()
    {
        if(isNight)
        {
            //It is night time
            if(sun.intensity == 0 && isNight == true)
            {
                 StartNight();
            }
        }
        else
        {
            //It is day time
            if(moon.intensity == 0 && isNight == false)
            {
                StartDay();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Hard;
        moon.shadows = LightShadows.None;
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }
}