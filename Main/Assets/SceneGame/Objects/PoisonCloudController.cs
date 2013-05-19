using UnityEngine;
using System.Collections;

public class PoisonCloudController : PausableMonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		particleSystem.Play();
	}
	
	
	void LateUpdate(){
		if(!particleSystem.IsAlive()){
			if (GameSession.Instance.isNetworkGame)
				Network.Destroy(gameObject);
			else
				Object.Destroy(gameObject);
		}
	}
}

