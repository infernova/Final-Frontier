using UnityEngine;
using System.Collections;

public class AoeShockwaveController : PausableMonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		particleSystem.Play();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(!particleSystem.IsAlive()){
			Object.Destroy(gameObject);
		}
	}
}

