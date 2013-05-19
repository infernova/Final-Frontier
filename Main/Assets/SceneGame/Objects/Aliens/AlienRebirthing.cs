using UnityEngine;
using System.Collections;

public class AlienRebirthing : Alien
{
	private bool hasDiedBefore = false;
	float deathTime = 3f;
	
	void Awake() {
		iconHeightOffset = 25;
		iconWidthOffset = 5;
	}
	protected override void handleDeath() {
		isDead = true;
		if (hpBar != null)
			Destroy(hpBar.gameObject);
		animation.Play("death");
		
	}
	
	public void ReStart() {
		base.Start();	
		animation.Stop();
		animation.Play("run");
	}
	
	void ZombieDeath(){
		if (hasDiedBefore) {
			animation.Stop();
			animation.Play("disappear");	
			return;
		}
		SlowEffect efx = GetComponent<SlowEffect>();
		if(efx != null){
			Destroy (efx);
		}
		PoisonEffect pfx = GetComponent<PoisonEffect>();
		if (pfx != null) {
			Destroy(pfx);	
		}
		
			hasDiedBefore = true;
			if(GameSession.Instance.isDefender){
				showGoldGain(Constants.AlienDeathGold[this.GetType().Name]);
			}
			else{
				float total = routeManager.distToEnd[0];
				showGoldGain(Mathf.FloorToInt(((total - distToEnd)/total) * (float) Cost));
			}
		StartCoroutine(Reanimate());			
	}
	
	private IEnumerator Reanimate(){
		yield return _sync();
		yield return new WaitForSeconds(deathTime);
		animation.Stop();
		animation.Play("reanimate");
	}
	
	// Use this for initialization
	void Start ()
	{
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