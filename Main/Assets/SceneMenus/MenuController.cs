using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	void Awake() {
		GameSession obj = (GameSession)FindObjectOfType(typeof(GameSession));	
		if (obj != null)
			Destroy(obj.gameObject);	
		
		Time.timeScale = 1;
		Invoke("DC",1f);
	}
	
	void DC () {
		Network.Disconnect();
	}
}
