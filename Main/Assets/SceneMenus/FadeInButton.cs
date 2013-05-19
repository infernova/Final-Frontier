using UnityEngine;
using System.Collections;

public class FadeInButton : FadeInImage {
	public string goalScene = "";
	
	public override void ButtonDown ()
	{
		if (goalScene != "") {
			Constants.GoToScene(goalScene);	
		}
	}
}
