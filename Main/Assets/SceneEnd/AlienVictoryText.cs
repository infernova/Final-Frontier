using UnityEngine;
using System.Collections;

public class AlienVictoryText : MonoBehaviour {
	// Use this for initialization
	void Start () {
		guiText.material.color = Color.black;
		if (GameSession.Instance != null && GameSession.Instance.isAttacker) {
			guiText.text = "You Win!";	
		} else {
			guiText.text = "You Lose!";	
		}
	}
}
