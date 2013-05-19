using UnityEngine;
using System.Collections;

public class UIBlockLower : GUITextureHelper {
	public static UIBlockLower MyInstance;
	public static UIBlockLower Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIBlockLower)FindObjectOfType(typeof(UIBlockLower));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
	
}
