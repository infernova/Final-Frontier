using UnityEngine;
using System.Collections;

public class PausableButton : GameButton {
	protected override bool IsEnabled() {
		if (GameSession.Instance != null) {	
			return !PausableMonoBehaviour.isPaused;
		}
		return true;
	}
	
}
