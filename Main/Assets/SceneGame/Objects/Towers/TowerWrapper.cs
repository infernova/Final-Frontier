using UnityEngine;
using System.Collections;

public class TowerWrapper : MonoBehaviour {
	public NetworkViewID targetView = NetworkViewID.unassigned;
	public int Level = 1;
	
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting) {
			NetworkViewID idC = targetView;
			int levelC = Level;
			stream.Serialize(ref idC);
			stream.Serialize(ref levelC);
		} else {
			NetworkViewID idZ = NetworkViewID.unassigned;
			int levelZ = 1;
			stream.Serialize(ref idZ);
			stream.Serialize(ref levelZ);
				
			targetView = idZ;
			Level = levelZ;
		}
	}
	
	void FixedUpdate() {
		if (GameSession.Instance.isDefender && GameSession.Instance.isNetworkGame) {
			Tower tower = GetComponentInChildren<Tower>();
			if (tower != null) {
				Level = tower.Level;
			}
			
			if (tower != null && tower.target != null) {
				targetView = tower.target.networkView.viewID;	
			} else {
				targetView = NetworkViewID.unassigned;	
			}
		}
	}
	
	public void mirrorShootAction() {
		networkView.RPC("RPCShootAnimationSound",RPCMode.All);
	}
	
	[RPC]
	void RPCShootAnimationSound() {
		Tower tower = GetComponentInChildren<Tower>();
		if (tower != null) {
			tower.handleShootAnimationSound();
		}
	}
	
	
}
