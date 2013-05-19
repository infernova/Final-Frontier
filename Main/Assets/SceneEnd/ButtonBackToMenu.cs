using UnityEngine;
using System.Collections;

public class ButtonBackToMenu : GameButton {

	public override void ButtonDown() {
		Constants.GoToScene("menu");
	}
}
