using UnityEngine;
using System.Collections;

public class TowerSlow : Tower
{
	private float SlowTime{
		get{
			switch(Level){
			case 1:
				return 3f;
			case 2:
				return 5f;
			case 3:
				return 7f;
			default:
				return -1f;
			}
		}
	}
	
	private float SlowRate{
		get{
			switch(Level){
			case 1:
				return 0.6f;
			case 2:
				return 0.5f;
			case 3:
				return 0.4f;
			default:
				return -1f;
			}
		}
	}
	
	protected override void ProjectileInit(GameObject proj, Alien target) {
		SlowProjectile data = proj.GetComponent<SlowProjectile>();
		data.Init(SlowTime, SlowRate, AreaOfEffect, target, Constants.bulletVelocity, Damage);
	}

}