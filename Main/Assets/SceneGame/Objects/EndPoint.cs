using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour
{
	private void OnTriggerEnter(Collider other){
		if (!GameSession.Instance.isNetworkGame || GameSession.Instance.isDefender) {
			Alien alien = other.gameObject.GetComponent<Alien>();
			alien.ReachEnd();
		}
		GetComponent<AudioSource>().PlayOneShot(PrefabManager.ReachEndSound);
	}
}

