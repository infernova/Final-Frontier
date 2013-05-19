using UnityEngine;
using System.Collections;

public class UITextTime : UITopPanelText {
	public override void PreUpdate() {
		int time = GameSession.Instance.tick;
		text = time.ToString();
		if (time == 0) {
			setColor(Color.red);
		}else {
			setColor(Constants.clear);	
		}
	}
}