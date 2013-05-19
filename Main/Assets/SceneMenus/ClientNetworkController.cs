using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ClientNetworkController : MonoBehaviour {
	public string ip = "";
	int ListenPort = 25000;
	public static ClientNetworkController MyInstance;
	public GUITexture[] characters = new GUITexture[15];
	Dictionary<string, IPButton> dictionary = new Dictionary<string, IPButton>();
	public static ClientNetworkController Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (ClientNetworkController)FindObjectOfType(typeof(ClientNetworkController));
			}
			return MyInstance;
		}
	}
	
	
	void Start() {
		PrefabManager.LoadPrefabs();
		Color color = new Color(61/255f,127/255f,219/255f,0.5f);
		for (int i=0;i<15;i++) {
			GUITexture texture = ((GameObject)Instantiate(PrefabManager.UIScreenText)).GetComponent<GUITexture>();
			texture.texture = PrefabManager.PrefabTopPanelText[' '];
			texture.color = color;
			texture.pixelInset = Constants.getRectToScreen(-290 + i * 38 ,225,40,48);
			characters[i] = texture;
		}
		UnityEngine.Object[] buttons = FindObjectsOfType(typeof(IPButton));
		foreach(UnityEngine.Object obj in buttons) {
			IPButton button = (IPButton)obj;
			dictionary.Add(button.key,button);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) {
			dictionary["0"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
			dictionary["1"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
			dictionary["2"].ButtonDown();	
		}else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
			dictionary["3"].ButtonDown();		
		}else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
			dictionary["4"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {
			dictionary["5"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) {
			dictionary["6"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) {
			dictionary["7"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) {
			dictionary["8"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) {
			dictionary["9"].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPeriod)) {
			dictionary["."].ButtonDown();
		}else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete) ) {
			dictionary["b"].ButtonDown();
		} else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			dictionary["e"].ButtonDown();
		}
		for (int i=0;i<ip.Length;i++) {
			characters[i].texture = PrefabManager.PrefabTopPanelText[ip[i]];	
		}
		if (ip.Length < 15) {
			characters[ip.Length].texture = PrefabManager.PrefabTopPanelText['_'];	
		}
		for (int i=ip.Length+1;i<15;i++) {
			characters[i].texture = PrefabManager.PrefabTopPanelText[' '];	
		}
	}
	
	public bool verifyKey(string key) {
		if ((key[0] >= '0' && key[0] <= '9') || key[0] == '.') {
			if (ip.Length != 0) {
				string [] words = ip.Split('.');
				if (key == ".") {
					if (ip[ip.Length-1] == '.') {
						return false;
					}
					if (words.Length == 4) {
						return false;
					}
					
					
				}else {
					if (Int32.Parse(words[words.Length-1]+key) > 255)
						return false;
					string lastWord = words[words.Length-1];
					if (lastWord.Length == 1 && lastWord[0] == '0') {
						return false;
					}
				}
			} else {
				if (key == ".")
					return false;
			}
			ip += key;
		}else {
			if (key == "b") {
				if (ip.Length != 0)
					ip = ip.Substring(0, ip.Length - 1);
			}else if (key == "e") {
				Debug.Log("Connecting");
				NetworkConnectionError error = Network.Connect(ip, ListenPort);
			}
		}
		return true;
	}
}
