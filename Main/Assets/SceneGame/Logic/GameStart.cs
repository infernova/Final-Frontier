using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStart : PausableMonoBehaviour {
	public static GameStart MyInstance;
	public static GameStart Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (GameStart)FindObjectOfType(typeof(GameStart));
			}
			return MyInstance;
		}
	}
	
	protected IEnumerator GiveAttackerGold(){
		while(true){
			yield return _sync();
			
			yield return new WaitForSeconds(Constants.AttackerGoldGainTick);
			
			if(GameSession.Instance.isAttacker){
				GameSession.Instance.credits += (GameSession.Instance.round - 1) * Constants.AttackerGoldGainStep + Constants.AttackerGoldGain;
			}
		}
	}
	
	public AlienSpawnPoint[] spawnPoints;
	public ButtonCard [] cards = new ButtonCard[6];
	// Use this for initialization
	// Change this to awake in the final ver
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;
		MyInstance = this;
		GameSession.Instance.tick = GameSession.startTick;
		//Instantiate(PrefabManager.UIFrame);
		//Instantiate(PrefabManager.UIBlockLower);
		bool isDefender = GameSession.Instance.isDefender;
		
		if (isDefender) {
			Instantiate(PrefabManager.UIButtonSell);
			Instantiate(PrefabManager.UIButtonUpgrade);
		}
		int offSet = (isDefender)? 0 : 6;
		
		for (int i = 0; i < 6; i++) {	
			GameObject cardObj = (GameObject) Instantiate(PrefabManager.UIButtonCard);
			ButtonCard card = cardObj.GetComponent<ButtonCard>();
			if (card != null) {
				card.ability = (ButtonCard.ButtonAbility) i + offSet;
				card.Init(-502 + 104 * i, -380,85,125);
				if(isDefender){
					cardObj.guiTexture.texture = PrefabManager.PrefabCards[card.ability.ToString()+"1"];
				}
				else{
					cardObj.guiTexture.texture = PrefabManager.PrefabCards[card.ability.ToString()];
				}
				cards[i] = card;
			}
		}
		
		if(!isDefender){
			spawnPoints[0].Select();
		}
		
		GameSession session = GameSession.Instance;
		List<Vector3> towerpoints = new List<Vector3>();
		float ht = -80;
		if (session.levelName == "game1") {
			float leftOffset = 102;
			towerpoints.Add(new Vector3(4+leftOffset,ht,330));
			towerpoints.Add(new Vector3(-110+leftOffset,ht,-225));
			towerpoints.Add(new Vector3(150+leftOffset,ht,-245));
			towerpoints.Add(new Vector3(345+leftOffset,ht,330));
			towerpoints.Add(new Vector3(-510+leftOffset,ht,150));
			towerpoints.Add(new Vector3(-225+leftOffset,ht,-50));
			towerpoints.Add(new Vector3(-9+leftOffset,ht,-60));
			towerpoints.Add(new Vector3(-160+leftOffset,ht,150));
			towerpoints.Add(new Vector3(90+leftOffset,ht,150));
			towerpoints.Add(new Vector3(300+leftOffset,ht,150));
			towerpoints.Add(new Vector3(385+leftOffset,ht,-35));
			towerpoints.Add(new Vector3(-420+leftOffset,ht,-65));
			towerpoints.Add(new Vector3(200+leftOffset,ht,-50));			
		}else if (session.levelName == "game2") {
			towerpoints.Add(new Vector3(-427,ht,342));
			towerpoints.Add(new Vector3(-184,ht,358));
			towerpoints.Add(new Vector3(-134,ht,142));
			towerpoints.Add(new Vector3(-317,ht,123));
			towerpoints.Add(new Vector3(-500,ht,110));
			towerpoints.Add(new Vector3(-376,ht,-86));
			towerpoints.Add(new Vector3(-199,ht,-76));
			towerpoints.Add(new Vector3(-30,ht,-72));
			towerpoints.Add(new Vector3(102,ht,146));
			towerpoints.Add(new Vector3(95,ht,365));
		}
		
		if (session.isNetworkGame && session.isDefender) {
			foreach(Vector3 pos in towerpoints) {
				Network.Instantiate(PrefabManager.PrefabTowerSpawnPoint,pos,Quaternion.identity,0);	
			}
		} else if (!session.isNetworkGame) {
			foreach(Vector3 pos in towerpoints) {
				Instantiate(PrefabManager.PrefabTowerSpawnPoint,pos,Quaternion.identity);	
			}
		}
		
		
		if(GameSession.Instance.isAttacker){
			StartCoroutine(GiveAttackerGold());
			Instantiate(PrefabManager.UIAttackerMask);
		}
		//TODO remove
		//GameSession.Instance.credits = 1000;
		GameSession.Instance.credits += (isDefender)? Constants.DefenderStartGold(1) : Constants.AttackerStartGold(1);
	}
	bool initialized = false;
	void Update() {
		GameSession session = GameSession.Instance;
		if (!initialized) {
			if (!session.isNetworkGame)
				ButtonPauseMenu.Instance.Resume();
			else if (session.isAttacker) {
				session.networkView.RPC("RPCRequestStart",RPCMode.Server);
			}
			initialized = true;	
			if (!session.isNetworkGame || session.isDefender)
				session.StartClock();
		}
		if (session.isAttacker) {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				AlienSpawnPoint[] points = spawnPoints;
				int pos = 0;
				for (int i=0;i<points.Length;i++) {
					if (points[i] == session.selectedObject) {
						pos = i;
						break;
					}
				}
				pos = (pos+1)%(points.Length);
				session.selectedObject.Deselect();
				points[pos].Select();
			}
		}
	}
}
