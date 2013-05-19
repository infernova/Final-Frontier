using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ButtonCard : PausableButton {
	public enum ButtonAbility {
		TowerGun,TowerCannon, TowerAoe, TowerDot, TowerSlow, TowerGoldGenerator,
		AlienGrunt, AlienArmouredGrunt, AlienFast, AlienRegenerating, AlienRebirthing, AlienBoss
	}
	public ButtonAbility ability;
	private Rect onScreen;
	
	public void Init(float x, float y, float w, float h) {
		onScreen = Constants.getRectToScreen(x,y,w,h);
	}
	
	private GUITexture cooldownMask;
	
	private Hashtable enabledTable = new Hashtable();
	private Hashtable maskSize = new Hashtable();
	
	void Start() {
		if (!GameSession.Instance.isDefender) {
			cooldownMask = ((GameObject) Instantiate(PrefabManager.UICardMask)).GetComponent<GUITexture>();
			cooldownMask.pixelInset = new Rect(onScreen.x, onScreen.y,onScreen.width,0);
		}
	}
	
	
	public override void ButtonDown() {
		ScreenDebugger.addText(ability.ToString());
		GameSession session = GameSession.Instance;
		
		if (session.isDefender) {
			if (session.disableTouch) {
				return;
			}
			
			if (session.selectedObject is Tower) {
				// Cancel tower selection
				session.selectedObject.Deselect();	
			}
			
			if (session.selectedCard != null && 
				session.selectedCard != this) {
				session.selectedCard.Deselect();	
			}
			
			if (session.selectedObject == null) {
				if (session.selectedCard == this) {
					// Double-tapping is a deselect
					Deselect();
					
				} else {
					// Just select
					Select();
					SelectableObject.showAll = true;
					guiTexture.texture = PrefabManager.PrefabCards[ability.ToString()+"Selected"];
				}
			} else {
				if (session.selectedCard == this) {
					// Build tower
					
					string abilityS = ability.ToString();
					if (Constants.TowerBuildCosts[abilityS] <= session.credits) {
						Tower tower = Tower.Create(ability.ToString(),(TowerSpawnPoint) session.selectedObject);
						tower.Select();
					}else {
						// Do something here	
					}					
				} else {
					// Change selection
					Select();
					session.showConfirmation();
				}
			}
		}
		else{
			if(session.selectedObject is AlienSpawnPoint){
				guiTexture.texture = PrefabManager.PrefabCards[ability.ToString()+"Selected"];
				AlienSpawnPoint spawn = (AlienSpawnPoint)GameSession.Instance.selectedObject;
				if(session.selectedCard == this && (enabledTable[spawn] == null || (bool)enabledTable[spawn] == true)){
					if (session.tick <= 0) {
						// Cannot spawn when time is up
						return;	
					}
					string abilityString = ability.ToString();
					if (session.credits >= Constants.AlienCost[abilityString]) {
						session.credits -= Constants.AlienCost[abilityString];
						AlienSpawnPoint sp = (AlienSpawnPoint) session.selectedObject;
						int spawnPointPos = Array.IndexOf(GameStart.Instance.spawnPoints,sp);
						int routePos = UnityEngine.Random.Range(0,sp.routes.Length);
						session.SpawnUnit(abilityString,spawnPointPos, routePos);
						
						enabledTable[spawn] = false;
						
						StartCoroutine(Cooldown(spawn));
					} else {
						// TODO complain not enuff credits somehow!
					}
				}
				else{
					if (session.selectedCard != null && session.selectedCard != this)
						session.selectedCard.Deselect();
					Select();
				}
			}
		}
	}
	public bool breakFlag = false;
	
	private IEnumerator Cooldown(AlienSpawnPoint spawn){
		breakFlag = false;
		float cooldownTime = Constants.AlienCooldown[ability.ToString()], currentCooldownTime = 0f;
		
		while(currentCooldownTime <= cooldownTime){
			if (breakFlag) {
				breakFlag = false;
				break;	
			}
			
			yield return _sync();
			
			currentCooldownTime += Time.deltaTime;
			float percentage = (cooldownTime - currentCooldownTime) / cooldownTime;
			maskSize[spawn] = new Rect(onScreen.x-5, onScreen.y-5,onScreen.width+10,percentage * onScreen.height+10);
		}
		maskSize[spawn] = Constants.offScreenRect;
		enabledTable[spawn] = true;
		breakFlag = false;
	}
	
	public void Select() {
		GameSession session = GameSession.Instance;
		session.selectedCard = this;
		UIBlockLowerDetails.Instance.guiTexture.texture = PrefabManager.CardDetails[ability.ToString()];
	}
	
	public void Deselect() {		
		GameSession.Instance.selectedCard = null;
		if(GameSession.Instance.isDefender) {
			SelectableObject.showAll = false;
			guiTexture.texture = PrefabManager.PrefabCards[ability.ToString()+"1"];
		} else {
			guiTexture.texture = PrefabManager.PrefabCards[ability.ToString()];	
		}
		UIBlockLowerDetails.Instance.guiTexture.texture = PrefabManager.Invisible;
	}
	static readonly KeyCode [] keys = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6};
	protected override void UpdateAddOn() {
		GameSession session = GameSession.Instance;
		if (session.isDefender) {
			if (session.selectedObject is Tower) {
				guiTexture.pixelInset = Constants.offScreenRect;
			} else {
				guiTexture.pixelInset = onScreen;
			}
			if (Constants.TowerBuildCosts[ability.ToString()] <= session.credits) {
				guiTexture.color = Constants.clear;	
			} else {
				guiTexture.color = Constants.desaturate;	
			}
		} else {
			guiTexture.pixelInset = onScreen;
			AlienSpawnPoint spawn = (AlienSpawnPoint)session.selectedObject;
			if (maskSize[spawn] != null) {
				cooldownMask.pixelInset = (Rect)maskSize[spawn];	
			} else {
				cooldownMask.pixelInset = Constants.offScreenRect;	
			}
			if (session.credits >= Constants.AlienCost[ability.ToString()] && session.tick > 0) {
				guiTexture.color = Constants.clear;	
			}else {
				guiTexture.color = Constants.desaturate;	
			}
		}
		
		int abilityNum = (int) ability;
		if (abilityNum > 5) {
			abilityNum -= 6;
		}
		if (Input.GetKeyDown(keys[abilityNum])) {
			handleDown();
		}
	}
}
