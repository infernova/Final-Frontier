using UnityEngine;
using System.Collections;

public class ScreenDebugger : MonoBehaviour {
	private static ScreenDebugger MyInstance = null;
	public static ScreenDebugger Instance {
		get {
			if (MyInstance == null) {
				//Final hope of setting MyInstance to something
				MyInstance = (ScreenDebugger)FindObjectOfType(typeof(ScreenDebugger));
			}
			return MyInstance;
		}
	}
	public static void addText(string line) {
		ScreenDebugger screen = ScreenDebugger.Instance;
		if (screen) {
			screen.guiText.text = line+"\n"+screen.guiText.text;
		}
	}
	
	public static void clearText() {
		ScreenDebugger screen = ScreenDebugger.Instance;
		if (screen) {
			screen.guiText.text = "";
		}
	}
}
