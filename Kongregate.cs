using UnityEngine;
using System.Collections;
using System;

public class Kongregate : MonoBehaviour {
	public static Kongregate i;
	
	public static bool isKongregateReady = false;
	public static int userId;
	public static string username;
	public static string gameAuthToken;
	
	// Use this for initialization
	void Start () {
		
		if (!i)
		{
			// Try to connect to Kongregate.
		   	// The gameObject.name parameter is used so SendMessage
		   	// will look for the OnKongregateAPILoaded method
		   	// on this same MonoBehaviour
		   	Application.ExternalEval(
			"if(typeof(kongregateUnitySupport) != 'undefined'){" +
			  		" kongregateUnitySupport.initAPI('" + gameObject.name + "','OnKongregateAPILoaded');" +
			   "};"
			);
			i = this;
			DontDestroyOnLoad(this);
		}
	}
	
	void OnKongregateAPILoaded(string userInfoString)
	{
		// Here I set a static variable which I can
		// check to know if Kongregate connection is ready
		isKongregateReady = true;
		// Kongregate returns a char delimited string
		// composed of userId|username|gameAuthToken
		// Here I just store them for easier access
		string[] parms = userInfoString.Split("|"[0]);
		userId = Convert.ToInt32(parms[0]); // int
		username = parms[1]; // string
		gameAuthToken = parms[2]; // string	
	}
	
	public static void UpdateScore(int score)
	{
		Application.ExternalCall("kongregate.stats.submit", "Nice Thingies", score);
		
	}

}
