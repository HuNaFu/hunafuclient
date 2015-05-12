using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Users : MonoBehaviour
{
	public Text ConnectedStatus;
	public Text FoundUsersStatus;
	public string ConnectURL;
	public string UsersURL;
	private Dictionary<string,string> ConnectionResponseHeader = null;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(CallURL(ConnectURL,Connected));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Connected(string text)
	{
		ConnectedStatus.text = text;
		StartCoroutine(CallURL(UsersURL,FoundUsers));
	}
	
	void FoundUsers(string text)
	{
		FoundUsersStatus.text = text;
	}

	private IEnumerator CallURL(string url, Action<string> callback)
	{
		var www = new WWW(url,null,ConnectionResponseHeader);
		yield return www;
		if (www.error != null)
		{
			Debug.LogError("Error " + www.error + "\nLoading URL " + www.url);
		}
		else
		{
			if (ConnectionResponseHeader == null && www.responseHeaders != null && www.responseHeaders["SET-COOKIE"] != null)
			{
				ConnectionResponseHeader = new Dictionary<string, string>();
				ConnectionResponseHeader["COOKIE"] = www.responseHeaders["SET-COOKIE"];
			}
			Debug.Log(" Text " + www.text);

			callback(www.text);
		}
	}

}
