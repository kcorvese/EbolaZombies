using UnityEngine;
using System.Collections;
using Utilities;

public class LevelSwitch : MonoBehaviour 
{
	public int level;
	public Transform player;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Use"))
		{
			Debug.Log("Door");

			RaycastHit hit = RayCast.Raycast (player, 5);
			
			if (hit.transform.gameObject.tag == "Door")
			{
				Application.LoadLevel(level);
			}
		}
	}
}
