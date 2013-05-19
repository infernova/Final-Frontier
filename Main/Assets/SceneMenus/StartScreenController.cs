using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PrefabManager.LoadPrefabs();
		Constants.InitConstants();
	}
}
