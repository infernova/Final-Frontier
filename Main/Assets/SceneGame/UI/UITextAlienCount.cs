using UnityEngine;
using System.Collections;

public class UITextAlienCount : UITopPanelText {
	public override void PreUpdate() {
		text = Alien.noOfAliens.ToString();
	}
}
