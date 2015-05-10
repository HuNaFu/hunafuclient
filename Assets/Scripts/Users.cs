using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Users : MonoBehaviour {

	public Text ShowUsers;
	public string UsersURL;

	// Use this for initialization
	void Start ()
	{



		StartCoroutine(CallURL(UsersURL,FoundUsers));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FoundUsers(string text)
	{
		ShowUsers.text = text;
	}

	private IEnumerator CallURL(string url, Action<string> callback)
	{
		var www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			Debug.LogError("Error " + www.error + "\nLoading URL " + www.url);
		}
		else
		{
			callback(www.text);
		}
	}

}
