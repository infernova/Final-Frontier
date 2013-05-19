using UnityEngine;
using System.Collections;

public class TowerGun : Tower {
	protected override void ProjectileInit(GameObject proj, Alien target) {
		Projectile data = proj.GetComponent<Projectile>();
		data.Init(AreaOfEffect, target, Constants.bulletVelocity, Damage);
	}
	
}