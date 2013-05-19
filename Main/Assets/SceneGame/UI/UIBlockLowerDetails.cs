using UnityEngine;
using System.Collections;

public class UIBlockLowerDetails : GUITextureHelper {

	public static UIBlockLowerDetails MyInstance;
	public static UIBlockLowerDetails Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIBlockLowerDetails)FindObjectOfType(typeof(UIBlockLowerDetails));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
}
