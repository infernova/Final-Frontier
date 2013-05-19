using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonProjectile : Projectile
{
	public float PoisonTime{
		get;
		set;
	}
	
	public int PoisonDamage{
		get;
		set;
	}
	
	public float PoisonTick{
		get;
		set;
	}
	
	protected override void ApplyHitEffects (Alien alien)
	{
		PoisonEffect efx = alien.gameObject.GetComponent<PoisonEffect>();
		
		if(efx != null){
			efx.PoisonDamage = Mathf.Max(PoisonDamage, efx.PoisonDamage);
			efx.EffectTime = Mathf.Max(PoisonTime,efx.EffectTime);
		}else {
			PoisonEffect fx = alien.gameObject.AddComponent<PoisonEffect>();
			fx.Init (PoisonDamage, PoisonTime, PoisonTick);
		}
	}
	
	public void Init (int damage, float time, float tick, int AOE, Alien tgt, int velocity, int dmg)
	{
		PoisonDamage = damage;
		PoisonTime = time;
		PoisonTick = tick;
		base.Init (AOE, tgt, velocity, dmg);
	}
	
	// Use this for initialization
	void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

