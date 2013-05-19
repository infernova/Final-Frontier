using UnityEngine;
using System.Collections;

public class UICountDown : MonoBehaviour {
	public static UICountDown MyInstance;
	public static UICountDown Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UICountDown)FindObjectOfType(typeof(UICountDown));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
}
