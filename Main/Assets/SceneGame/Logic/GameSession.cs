using UnityEngine;
using System.Collections;

public class GameSession : PausableMonoBehaviour {
	public string levelName = "game1";
	
	public static readonly int maxRound = 3;
	public static readonly int startTick = 60;
	public bool isNetworkGame = false;
	public bool isDefender = false;
	public bool isAttacker {
		get { return !isDefender; }
	}
	enum GameState {
		Running, AlienWin, HumanWin
	}
	GameState state = GameState.Running;
	
	public int credits{
		get;
		set;
	}
	
	public int tick = startTick;
	public int round = 1; // handled via RPC
	
	const int startLives = 10;
	
	private int _lives = startLives;
	public int lives {
		get {
			return _lives;
		}
		set {
			if (value < 0)
				_lives = 0;
			else {
				_lives = value;
			}
		}
	}
	public ButtonCard selectedCard = null;
	public SelectableObject selectedObject = null;
	
	
	public void refreshSelected() {
		if (selectedCard != null)
			selectedCard.Deselect();
		if (selectedObject != null && !(selectedObject is AlienSpawnPoint)) {
			selectedObject.Deselect();
		}
	}
	
	public void showConfirmation() {
		Transform firingRadius = ((TowerSpawnPoint)selectedObject).firingRadius;
		string ability = selectedCard.ability.ToString();
		if (firingRadius != null) {
			float range = (float)Constants.TowerRanges[ability][0];
			firingRadius.localScale = new Vector3(range*2,20,range*2);
			firingRadius.renderer.enabled = true;
		}
		TowerSpawnPoint.showAll = true;
		selectedCard.guiTexture.texture = PrefabManager.PrefabCards[ability+"Confirm"];
	}
	
	public static GameSession MyInstance;
	public static GameSession Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (GameSession)FindObjectOfType(typeof(GameSession));
			}
			return MyInstance;
		}
	}
	
	void Awake() {
		if (MyInstance != null) {
			Destroy(this.gameObject);
			return;
		}
		MyInstance = this;
		state = GameState.Running;
		PrefabManager.LoadPrefabs();
		Constants.InitConstants();
		
		PausableMonoBehaviour.isPaused = true;
		_lives = startLives;
		Alien.noOfAliens = 0;
		DontDestroyOnLoad(this.gameObject);
		
		// For network game, otherwise handled by init in selection scene
		switch (Network.peerType) {
		case NetworkPeerType.Server:
			isNetworkGame = true;
			isDefender = true;
			Constants.GoToScene(levelName);
			break;
			
		case NetworkPeerType.Client:
			isNetworkGame = true;
			isDefender = false;
			Constants.GoToScene(levelName);
			break;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {		
		if (GameStart.Instance != null) {
			// Swap towers using tab for attacker :D			
			if (isNetworkGame && isDefender) {
				if (!gameStarted && clientReady) {
					gameStarted = true;
					networkView.RPC ("RPCGameStart",RPCMode.All);	
				}
			}
		}
	}
	
	[RPC]
	void RPCGameStart() {
		gameStarted = true;
		ButtonPauseMenu.Instance.Resume();
	}
	
	void LateUpdate() {
		if (!GameOver && state == GameState.AlienWin) {
			GameOver = true;
			Constants.GoToScene("alienvictory");
		}
		if (!GameOver && state == GameState.HumanWin) {
			GameOver = true;
			Constants.GoToScene("humanvictory");
		}
		
		//check if has reached end state	
		if (GameStart.Instance != null && 
			(!isNetworkGame || isDefender)) {
			if (lives <= 0) {
				state = GameState.AlienWin;
				Constants.GoToScene("alienvictory");
			}else if (Alien.noOfAliens == 0 && tick == 0) {
				nextRound();	
			}
			
		}
	}
	
	public void Init(bool isDef, bool isNetwork) {
		isDefender = isDef;
		isNetworkGame = isNetwork;
	}
	
	public bool GameOver = false;
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		if (!GameOver)
			Constants.GoToScene("disconnected");
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (!GameOver)
			Constants.GoToScene("disconnected");
    }
	
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting) {
			int tickC = tick;
			int livesC = lives;
			int stateC = (int)state;
			stream.Serialize(ref tickC);
			stream.Serialize(ref livesC);
			stream.Serialize(ref stateC);
			if (state != GameState.Running) {
				GameOver = true;	
			}
		} else {
			int tickZ = 0;
			int livesZ = 0;
			int stateZ = 0;
			stream.Serialize(ref tickZ);
			stream.Serialize(ref livesZ);
			stream.Serialize(ref stateZ);
			tick = tickZ;
			lives = livesZ;
			state = (GameState) stateZ;
		}
	}
	
	
	void nextRound() {
		if (round >= maxRound) {
			state = GameState.HumanWin;
			Constants.GoToScene("humanvictory");
			return;
		}
		
		
		if (isNetworkGame) {
			networkView.RPC("RPCnextRound",RPCMode.All);
		} else {
			handleNextRound();	
		}
		// Starting the clock while paused 
		// ensure that it starts the moment the pause ends
		tick = startTick;
		StartClock();
	}
	
	[RPC]
	void RPCnextRound() {
		handleNextRound();
	}
	
	void handleNextRound() {
		isPaused = true;
		round++;
		credits += (isDefender)? Constants.DefenderStartGold(round) : Constants.AttackerStartGold(round);
		ButtonPauseMenu.Instance.Resume();
		if (isAttacker) {
			// Reset cards cooldowns
			foreach (ButtonCard card in GameStart.Instance.cards) {
				card.breakFlag = true;	
			}
		}
	}
	
	public void SpawnUnit(string unitName, int spawnPointID, int routeInPoint) {
		if (isNetworkGame) {
			networkView.RPC("RPCspawnUnit",RPCMode.All,unitName,spawnPointID, routeInPoint);
		} else {
			StartCoroutine(AlienBuilder.Instance.BuildAlien(unitName, spawnPointID, routeInPoint));
		}
	}
	
	[RPC]
	void RPCspawnUnit (string unitName, int spawnPointID, int routeInPoint) {
		StartCoroutine(AlienBuilder.Instance.BuildAlien(unitName, spawnPointID, routeInPoint));
	}
	
	public void RequestResume() {
		if (Network.isServer) {
			ResumeGame();
		} else {
			networkView.RPC("RPCRequestResume",RPCMode.Server);
		}
	}
	
	[RPC]
	void RPCRequestResume() {
		if (PausableMonoBehaviour.isPaused) {
			ResumeGame();	
		}
	}
	
	public void ResumeGame() {
		if (!ButtonPauseMenu.Instance.processing)
			networkView.RPC("RPCResumeGame",RPCMode.All);
	}
	
	[RPC]
	void RPCResumeGame() {
		handleResumeGame();
	}
	
	void handleResumeGame() {
		// need an instance to reference the animation ><
		ButtonPauseMenu.Instance.Resume();
	}
	
	public void RequestPause() {
		if (Network.isServer) {
			PauseGame();
		} else {
			networkView.RPC("RPCRequestPause",RPCMode.Server);
		}
	}
	
	[RPC] 
	void RPCRequestPause() {
		if (!PausableMonoBehaviour.isPaused) {
			PauseGame();	
		}
	}
	
	void PauseGame() {
		networkView.RPC("RPCPauseGame",RPCMode.All);
	}
	
	[RPC]
	void RPCPauseGame() {
		handlePauseGame();
	}
	
	void handlePauseGame() {
		PausableMonoBehaviour.Pause();
	}
	
	public bool disableTouch = false;
	protected virtual IEnumerator Tick(){
		while(tick > 0){
			yield return _sync();
			yield return new WaitForSeconds(1f);
			tick--;	
			if (tick == 0) {
				disableTouch = true;
				refreshSelected();
			}
		}
	}
	
	public void StartClock() {
		StartCoroutine("Tick");
		
	}
	bool clientReady = false;
	bool gameStarted = false;
	[RPC]
	void RPCRequestStart() {
		clientReady = true;
	}
	
	void OnApplicationPause(bool paused) {
		Network.Disconnect();	
	}
	
}
