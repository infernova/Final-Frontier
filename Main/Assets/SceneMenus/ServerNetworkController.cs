using UnityEngine;
using System.Collections;

public class ServerNetworkController : MonoBehaviour {
	public string ip = "0.0.0.0";
	int ListenPort = 25000;
	public static ServerNetworkController MyInstance;
	public GUITexture[] characters = new GUITexture[15];
	float width = 40;
	float height = 48;
	public int selectedLevel = 1;
	
	public static ServerNetworkController Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (ServerNetworkController)FindObjectOfType(typeof(ServerNetworkController));
			}
			return MyInstance;
		}
	}
	
	public void CreateServer() {
		Network.Disconnect();
		NetworkConnectionError error = Network.InitializeServer(32, ListenPort, false);
		Debug.Log("new server instance");
	}
	
	void Start() {
		CreateServer();
		PrefabManager.LoadPrefabs();
		Color color = new Color(61/255f,127/255f,219/255f,0.5f);
		for (int i=0;i<15;i++) {
			GUITexture texture = ((GameObject)Instantiate(PrefabManager.UIScreenText)).GetComponent<GUITexture>();
			texture.texture = PrefabManager.PrefabTopPanelText[' '];
			texture.color = color;
			texture.pixelInset = Constants.getRectToScreen(-250 + i * 38 ,190,width,height);
			characters[i] = texture;
		}
	}
	
	void OnServerInitialized () {
		ip = Network.player.ipAddress;
	}
	// Update is called once per frame
	void Update () {
		for (int i=0;i<ip.Length;i++) {
			characters[i].texture = PrefabManager.PrefabTopPanelText[ip[i]];	
			characters[i].pixelInset = Constants.getRectToScreen((-  width  * ip.Length / 2f + i * width)-40, 190,width,height);
		}
		for (int i=ip.Length;i<15;i++) {
			characters[i].texture = PrefabManager.PrefabTopPanelText[' '];	
		}
		
	}
	void OnPlayerConnected () {
		Network.maxConnections = -1;  // Prevent more incoming connections
		if (selectedLevel == 1)
			Network.Instantiate(PrefabManager.Gamesession1,Vector3.zero,Quaternion.identity,0);
		if (selectedLevel == 2)
			Network.Instantiate(PrefabManager.Gamesession2,Vector3.zero,Quaternion.identity,0);
	}
}
