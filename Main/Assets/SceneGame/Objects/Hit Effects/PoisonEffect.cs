using UnityEngine;
using System.Collections;

public class PoisonEffect : Effect
{
	
	public int PoisonDamage{
		get;
		set;
	}
	
	public float PoisonTick{
		get;
		set;
	}
	
	private IEnumerator DamageTarget(){
		float prevFixedTime = Time.fixedTime;
		
		while(EffectTime >= 0){
			yield return _sync();
			yield return new WaitForSeconds(PoisonTick);
			EffectTime -= (Time.fixedTime - prevFixedTime);
			
			gameObject.GetComponent<Alien>().DamageOrHeal(-PoisonDamage, true);
			
			
			prevFixedTime = Time.fixedTime;
		}
		
		EffectTimeOut();
	}
	
	public void Init(int damage, float time, float tick){
		PoisonDamage = damage;
		PoisonTick = tick;
		base.Init(time);
	}
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine (DamageTarget ());
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
	}
}

