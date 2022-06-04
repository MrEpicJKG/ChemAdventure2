using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    //1 realtime sec = 1 in-game min
    //sunrise will be from 5am - 6:20am (300 - 380)
    //sunset will be from 8pm - 9:20pm (1200 - 1280)
    //Note: 1440 min = 1 day, noon = 720 min
    /* 12am = 0 min
     * 1am  = 60
     * 2am  = 120
     * 3am  = 180
     * 4am  = 240
     * 5am  = 300
     * 6am  = 360
     * 7am  = 420
     * 8am  = 480
     * 9am  = 540
     * 10am = 600
     * 11am = 660
     * 12pm = 720
     * 1pm  = 780
     * 2pm  = 840
     * 3pm  = 900
     * 4pm  = 960
     * 5pm  = 1020
     * 6pm  = 1080
     * 7pm  = 1140
     * 8pm  = 1200
     * 9pm  = 1260
     * 10pm = 1320
     * 11pm = 1380
     * 11:59pm = 1439*/
    [SerializeField] private bool shouldPrintToConsole = false;
    [Min(0.001f)] public float globalTimeMult = 1;
    public int currTimeInGameMin = 270; //4:30am
    public int flashlightOnTime = 1200; //8pm
    public int flashlightOffTime = 360; //6am
    public int sunriseStartTime = 300;
    public int sunsetStartTime = 1200;
    public int riseSetDuration = 80;
    public float lightMinIntensity = 0.1f;
    public Light2D globalLight;
    public Light2D outsideLight;
    public Light2D flashLight;

    private float lightIntensityChangePerMin = 0.01125f;
    // Start is called before the first frame update
    void Start()
    {
        lightIntensityChangePerMin = (1 - lightMinIntensity) / riseSetDuration;
        if(currTimeInGameMin > sunriseStartTime + riseSetDuration && currTimeInGameMin < sunsetStartTime)
        {
            globalLight.intensity = 1;
		}
        else if(currTimeInGameMin < sunriseStartTime && currTimeInGameMin > sunsetStartTime + riseSetDuration)
        {
            globalLight.intensity = 0.1f;
		}
        else if (currTimeInGameMin >= sunriseStartTime && currTimeInGameMin < sunriseStartTime + riseSetDuration)
        {
            globalLight.intensity = 0.1f + (lightIntensityChangePerMin * (currTimeInGameMin - sunriseStartTime));
        }
        else if (currTimeInGameMin >= sunsetStartTime && currTimeInGameMin < sunsetStartTime + riseSetDuration)
        {
            globalLight.intensity = 1f - (lightIntensityChangePerMin * (currTimeInGameMin - sunsetStartTime));
        }

        if (currTimeInGameMin > flashlightOffTime && currTimeInGameMin < flashlightOnTime) //if it is daytime
        {
            if (flashLight.enabled == true)
            {
                flashLight.enabled = false;
            }
        }
        else  //it is nighttime
        {
            if (flashLight.enabled == false)
            {
                flashLight.enabled = true;
            }
        }

        Invoke("TimeUpdate", 0);
    }

    private void TimeUpdate() //called once per sec
    {
        //advance time by 1 min
        if (currTimeInGameMin < 1440)
        {
            currTimeInGameMin++;
        }
        else if (currTimeInGameMin >= 1440) //should never be greater than, just a precaution 
        {
            currTimeInGameMin = 1;
		}

        //toggle flashlight based on time
        if (currTimeInGameMin > flashlightOffTime && currTimeInGameMin < flashlightOnTime) //if it is daytime
        {
            if (flashLight.enabled == true)
            {
                flashLight.enabled = false;
            }
        }
        else  //it is nighttime
        {
            if (flashLight.enabled == false)
            {
                flashLight.enabled = true;
            }
        }

        if(currTimeInGameMin >= sunriseStartTime && currTimeInGameMin < sunriseStartTime + riseSetDuration)
        {
            globalLight.intensity = Mathf.Clamp(globalLight.intensity + lightIntensityChangePerMin, 0.1f, 1);
		}
        else if(currTimeInGameMin >= sunsetStartTime && currTimeInGameMin < sunsetStartTime + riseSetDuration)
        {
            globalLight.intensity = Mathf.Clamp(globalLight.intensity - lightIntensityChangePerMin, 0.1f, 1);
        }

        Vector3 timeVect = ConvertMinsToClock();
        int currHours = (int)timeVect.x;
        int currMins = (int)timeVect.y;
        int currPeriod = (int)timeVect.z;

        string periodString = "";

        if(currPeriod == -1)
        {
            periodString = " AM";
		}
        else
        {
            periodString = " PM";
		}

        if(currHours > 12)
        {
            currHours -= 12;
		}

        if (shouldPrintToConsole == true)
        {
            string timeString = currHours.ToString() + ":" + currMins.ToString("00") + periodString;
            print("Current Time: " + timeString + "       Minutes: " + currTimeInGameMin);
        }

        Invoke("TimeUpdate", 1 / globalTimeMult);
	}

    private Vector3Int ConvertMinsToClock()
    {
        //Vector3Int is simply a way of returning 3 values in this use-case
        //X = hours, Y = Minutes, Z = AM/PM (-1 = AM, 1 = PM)
        int currHours = Mathf.FloorToInt(currTimeInGameMin / 60);
        int currMinutes = Mathf.RoundToInt(currTimeInGameMin % 60);
        int currPeriod = 0; //am or pm

        if(currTimeInGameMin < 720)
        {
            currPeriod = -1;
        }
        else
        {
            currPeriod = 1;
		}

        return new Vector3Int(currHours, currMinutes, currPeriod);
	}
}
