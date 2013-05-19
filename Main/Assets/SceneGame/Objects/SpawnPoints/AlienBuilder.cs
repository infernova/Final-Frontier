using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AlienBuilder : PausableMonoBehaviour
{
	/*public class ObjLocPair{
		public Object obj;
		public AlienSpawnPoint loc;
		
		public ObjLocPair(Object o, AlienSpawnPoint sp){
			obj = o;
			loc = sp;
		}
	}*/
	
	//private static Queue<ObjLocPair> overallAlienQueue = new Queue<ObjLocPair>();
	const float spawnWaitTime = 3f;
	
	private static AlienBuilder _DispatcherInstance;
	public static AlienBuilder Instance {
		get {
			if (_DispatcherInstance == null) {
				_DispatcherInstance = (AlienBuilder) FindObjectOfType(typeof(AlienBuilder));
			}
			return _DispatcherInstance;
		}
	}
	/*
	private IEnumerator Dispatch(){
		while(true){
			yield return _sync();
			
			if(overallAlienQueue.Count != 0){
				Debug.Log("noticed entry");
				yield return new WaitForSeconds(spawnWaitTime);
				Debug.Log("Spawning");
				ObjLocPair dispatch = overallAlienQueue.Dequeue();
				
				dispatch.loc.Spawn(dispatch.obj);
			}
		}
	}*/
	
	public IEnumerator BuildAlien(string alienType, int spawnPointID, int routeID){
		Alien.noOfAliens++;
		float timePassed = 0f, buildTime = spawnWaitTime;
		GUITexture icon = ((GameObject)Instantiate(PrefabManager.UIAlienIcon)).GetComponent<GUITexture>();
		icon.texture = PrefabManager.PrefabIcons[alienType];
		while(timePassed <= buildTime){
			yield return _sync();
			timePassed += Time.deltaTime;
			float percentage = timePassed / spawnWaitTime;
			icon.pixelInset = Constants.getRectToScreen(440, 230 - (530 * percentage), 60, 60);
		}
		Destroy(icon.gameObject);
		if (!GameSession.Instance.isNetworkGame || GameSession.Instance.isDefender) {
			AlienSpawnPoint sp = GameStart.Instance.spawnPoints[spawnPointID];
			sp.Spawn(PrefabManager.PrefabAliens[alienType],spawnPointID, routeID);
		}
	}
	/*
	public static void QueueAlien(Object alien, AlienSpawnPoint spawnPt){
		overallAlienQueue.Enqueue(new ObjLocPair(alien, spawnPt));
	}*/

}

