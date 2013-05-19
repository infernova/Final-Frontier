using UnityEngine;
using System.Collections;

public class UITextCredits : UITopPanelText {
	public override void PreUpdate() {
		text = GameSession.Instance.credits.ToString();
	}
}