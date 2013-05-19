using UnityEngine;
using System.Collections;

public class HumanVictoryText : MonoBehaviour {
	// Use this for initialization
	void Start () {
		guiText.material.color = Color.black;
		if (GameSession.Instance != null && GameSession.Instance.isDefender) {
			guiText.text = "You Win!";	
		} else {
			guiText.text = "You Lose!";	
		}
	}
}
