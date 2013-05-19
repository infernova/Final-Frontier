using UnityEngine;
using System.Collections;

public class AlienRegenerating : Alien
{
	private float regenTick = 3f;
	private const int regenAmt = 50;
	void Awake() {
		iconHeightOffset = 18;
	}
	private IEnumerator RegenLife(){
		while(true){
			yield return _sync();
			
			if(HP < MaxHP){
				DamageOrHeal(regenAmt);
			}
			
			yield return new WaitForSeconds(regenTick);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (!GameSession.Instance.isNetworkGame || GameSession.Instance.isDefender)
			StartCoroutine(RegenLife());
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
	}
	
	void LateUpdate () {
		base.LateUpdate();	
	}
	
	void FixedUpdate() 
	{
		base.FixedUpdate();
	}
}