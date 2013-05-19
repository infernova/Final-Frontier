using UnityEngine;
using System.Collections;

public class SlowProjectile : Projectile
{
	
	public float SlowTime{
		get;
		set;
	}
	
	public float SlowRate{
		get;
		set;
	}
	
	protected override void ApplyHitEffects (Alien alien)
	{
		SlowEffect efx = alien.gameObject.GetComponent<SlowEffect>();
		
		if(efx != null){
			Object.Destroy (efx);
		}
		
		if (GameSession.Instance.isNetworkGame)
			alien.slowDown(SlowTime,SlowRate);
		SlowEffect fx = alien.gameObject.AddComponent<SlowEffect>();
		fx.Init(SlowTime, SlowRate);
	}
	
	public void Init(float time, float rate, int AOE, Alien tgt, int velocity, int dmg) {
		SlowRate = rate;
		SlowTime = time;
		base.Init(AOE, tgt, velocity, dmg);
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
}

