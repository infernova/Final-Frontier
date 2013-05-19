using UnityEngine;
using System.Collections;

public class TowerDot : Tower
{
	private int PoisonDamage{
		get{
			switch(Level){
			case 1:
				return 10;
			case 2:
				return 14;
			case 3:
				return 20;
			default:
				return 0;
			}
		}
	}
	
	private float PoisonTick{
		get{ return 1f; }
	}
	
	private float PoisonTime{
		get{
			switch(Level){
			case 1:
				return 3f;
			case 2:
				return 5f;
			case 3:
				return 7f;
			default:
				return 0f;
			}
		}			
	}
	
	protected override void ProjectileInit(GameObject proj, Alien target) {
		PoisonProjectile data = proj.GetComponent<PoisonProjectile>();
		data.Init(PoisonDamage, PoisonTime, PoisonTick, AreaOfEffect, target, Constants.bulletVelocity, Damage);
	}

}