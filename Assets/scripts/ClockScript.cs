using UnityEngine;
using System.Collections;
using Utilities;

public class ClockScript : MonoBehaviour
{
	Timer timer;

	int time;

	// Use this for initialization
	void Start () 
	{
		if(!PlayerPrefs.HasKey("timer"))
		{
			timer = new Timer(300000);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}