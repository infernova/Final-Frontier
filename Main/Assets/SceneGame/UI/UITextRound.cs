using UnityEngine;
using System.Collections;

public class UITextRound : UITopPanelText {
	public override void PreUpdate() {
		text = GameSession.Instance.round + "/" + GameSession.maxRound;
	}
}