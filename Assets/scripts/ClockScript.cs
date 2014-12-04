using UnityEngine;
using System;
using System.Collections;
using Utilities;

public class ClockScript : MonoBehaviour
{
     Timer timer;

     int time;

     // Use this for initialization
     void Start()
     {
          //PlayerPrefs.DeleteAll();
          if (PlayerPrefs.HasKey("timer")) { Debug.Log("has timer"); timer = new Timer(PlayerPrefs.GetFloat("timer")); }
          else timer = new Timer(300);
          timer.start();
     }

     void OnGUI()
     {
          GUI.Label(new Rect(5, 5, 300, 30), "Time: " + formatTime((int)timer.getRemaining())); // formatTime(timer.getRemaining())
     }

     void OnDestroy()
     {
          PlayerPrefs.SetFloat("timer", timer.getRemaining());
          PlayerPrefs.Save();
     }

     private string formatTime(int time)
     {
          int s = (int)time,
               h = s / 3600,
               m = s / 60;

          //Debug.Log(s);

          TimeSpan ts = new TimeSpan(h, 0, s);

          return "" + ts.Duration();
     }
}