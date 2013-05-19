using UnityEngine;
using System.Collections;

public class SelectableObject : PausableMonoBehaviour {
	public static bool showAll = false;
	public bool showRing = false;
	public UIRing ring = null;
	void ButtonDown() {
		if (!isPaused) {
			handleButtonDown();
		}
	}
	protected virtual void handleButtonDown() { }
	protected virtual void handleDeselect() { }
	public void Deselect() {
		handleDeselect();
	}
}
