using UnityEngine;
using System.Collections;

public class TerrainHit : MonoBehaviour {
	void ButtonDown() {
		if (!PausableMonoBehaviour.isPaused && GameSession.Instance.isDefender) {
			ScreenDebugger.addText("teh-rain");
			GameSession.Instance.refreshSelected();
		}
	}
}
