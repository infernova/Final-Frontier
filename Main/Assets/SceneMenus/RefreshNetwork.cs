using UnityEngine;
using System.Collections;

public class RefreshNetwork : GameButton {

	public override void ButtonDown ()
	{
		ServerNetworkController.Instance.CreateServer();
	}
}
