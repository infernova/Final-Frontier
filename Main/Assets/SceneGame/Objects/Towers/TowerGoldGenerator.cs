using UnityEngine;
using System.Collections;

public class TowerGoldGenerator : Tower
{
	private int GoldGenerated{
		get {
			switch(Level){
			case 1:
				return 2;
			case 2:
				return 5;
			case 3:
				return 10;
			default:
				return -1;
			}
		}
	}
	
	protected override void ProjectileInit(GameObject proj, Alien target) {
		Projectile data = proj.GetComponent<Projectile>();
		data.Init(AreaOfEffect, target, Constants.bulletVelocity, Damage);
	}
	
	protected override IEnumerator Shoot(){
		while(true){
			yield return _sync();
			
			if(GameSession.Instance.isDefender){
				GameSession.Instance.credits += GoldGenerated;
				gameObject.GetComponent<AudioSource>().PlayOneShot(PrefabManager.TowerShotSounds[this.GetType().Name], 0.3f);
			}
			
			yield return new WaitForSeconds((float) RateOfFire);
		}
	}

}