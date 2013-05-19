using UnityEngine;
using System.Collections;

public class BackButton : GameButton {
	public string sceneLoad = "menu";
	public override void ButtonDown() {
		Network.Disconnect();
		Constants.GoToScene(sceneLoad);
	}
}
