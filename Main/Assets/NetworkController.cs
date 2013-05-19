using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class NetworkController : MonoBehaviour {
	int ListenPort = 25000;
	string text = "No network stuff";
	string text2 = "";
	public string connectIP = "192.168.1.";
	
	// Use this for initialization
	void Start () {
		Network.Disconnect();
		GameSession[] sessions = FindObjectsOfType(typeof(GameSession)) as GameSession[];
        foreach (GameSession session in sessions) {
           Destroy(session.gameObject);
        }
		// Reset key static variables
		GameSession.MyInstance = null;
		GameStart.MyInstance = null;
		PrefabManager.LoadPrefabs();
		Constants.InitConstants();
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10,10,200,200), "Loader Menu");
		GUI.Label(new Rect(15,30,90,20),"Port");
		string newPort = GUI.TextField(new Rect(50,30,90,20), ListenPort.ToString(), 5);
		newPort = Regex.Replace(newPort,@"[^0-9]","");
		if (newPort != "") {
			ListenPort = int.Parse(newPort); 	
		}
		
		connectIP = GUI.TextField(new Rect(50,180,90,20), connectIP, 30);
		connectIP = Regex.Replace(connectIP,@"[^0-9 .]","");
		
		
		if(GUI.Button(new Rect(20,70,80,20), "Server")) {
			Network.Disconnect();
			NetworkConnectionError error = Network.InitializeServer(32, ListenPort, false);
			if (error != NetworkConnectionError.NoError) {
				text = error.ToString();
			}
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,100,80,20), "Client")) {
			Network.Disconnect();
			text = "Connecting to "+connectIP;
			NetworkConnectionError error = Network.Connect(connectIP, ListenPort);
			if (error != NetworkConnectionError.NoError) {
				text = error.ToString();
			}
		}
		GUI.Label(new Rect(15,120,500,20),text);
		GUI.Label(new Rect(15,140,500,20),text2);
	}
	void OnServerInitialized () {
		text = "Server IP: "+Network.player.ipAddress;
	}
	
	void OnFailedToConnect (NetworkConnectionError error) {
		text = error.ToString();
	}
	
	void OnConnectedToServer () {
		text = "Client, Connected";
	}
	
	void OnPlayerConnected () {
		text2 = "connection!";
		Network.maxConnections = -1;  // Prevent more incoming connections
		Network.Instantiate(PrefabManager.Gamesession1,Vector3.zero,Quaternion.identity,0);
	}
}
