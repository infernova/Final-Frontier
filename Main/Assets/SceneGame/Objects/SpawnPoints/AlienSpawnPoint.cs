using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienSpawnPoint : SelectableObject
{
	private Vector3 spawnLocation;
	public PathManager[] routes;
	public static readonly Vector3 startPos = new Vector3(0.5f,0.5f,-100f);
	public void Spawn(Object toSpawn,int spawnPointID, int routeID){
		Vector3 pos = routes[routeID].path[0].position;
		GameObject alien;
		if (GameSession.Instance.isNetworkGame) {
			alien = (GameObject) Network.Instantiate(toSpawn,new Vector3(pos.x, spawnLocation.y-10, pos.z),Quaternion.identity,0);
		}else {
			alien = (GameObject) Instantiate(toSpawn, new Vector3(pos.x, spawnLocation.y-10, pos.z), Quaternion.identity);
		}
		Alien alienData = alien.GetComponent<Alien>();
		alienData.Init(spawnPointID, routeID);
		GetComponent<AudioSource>().PlayOneShot(PrefabManager.SpawnSound);
	}
	/*
	public void AddAlien(string alien){
		Object alienPrefab = PrefabManager.PrefabAliens[alien];
		
		if(!GameSession.Instance.isDefender){
			GameSession.Instance.credits -= ((GameObject) alienPrefab).GetComponent<Alien>().Cost;
		}
		
		AlienBuilder.QueueAlien(alienPrefab, this);
	}*/
	
	
	// Use this for initialization
	void Start ()
	{
		spawnLocation = transform.position;
		//Spawn a selection ring
		GameObject obj = (GameObject)Instantiate(PrefabManager.PrefabUIRing,transform.position,Quaternion.identity);
		UIRing uiRing = obj.GetComponent<UIRing>();
		uiRing.obj = this;
		ring = uiRing;
		uiRing.toInit = true;
	}
	
	protected override void handleButtonDown() {
		ScreenDebugger.addText("ey-lee-an");
		GameSession session = GameSession.Instance;
		if (!session.isDefender) {
			if (session.selectedObject != null && 
				session.selectedObject != this) {
				session.selectedObject.Deselect();	
			}
			
			Select ();
		} 
		else {
			ScreenDebugger.addText("Deselecting");
			session.refreshSelected();
		}
		
	}
	
	public void Select() {
		GameSession session = GameSession.Instance;
		if (session.selectedObject != null)
			session.selectedObject.Deselect();
		
		session.selectedObject = this;
		showRing = true;
	}
	
	protected override void handleDeselect() { 
		GameSession.Instance.selectedObject = null;
		showRing = false;
	}
	
}

