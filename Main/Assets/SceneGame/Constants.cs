using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Constants {
	private static string [] scenes = new string[]{"startscreen", "menu", "game", "game1", "alienvictory","humanvictory","disconnected","encyclopedia","networkplay","beastiary","armoury","clientattacker","serverdefender","networkexample","game2"};
	public static readonly Rect offScreenRect = new Rect(0,0,0,0);
	public static readonly Color clear = new Color(0.5f,0.5f,0.5f,0.5f);
	public static readonly Color desaturate = new Color(0.2f,0.2f,0.2f,0.5f);
	public static bool initialized = false;
	public static LayerMask AliensLayer;
	
	public static int AttackerGoldGain = 5;
	public static int AttackerGoldGainStep = 2;
	public static float AttackerGoldGainTick = 6f;
	public static int AttackerStartGold(int round) {
		int i=0;
		switch (round) {
			case 1: i= 10; break;
			case 2: i= 30; break;
			case 3: i= 50; break;
			default: break;
		}
		return i;
	}
	public static int DefenderStartGold(int round) {
		int i=0;
		switch (round) {
			case 1: i = 30; break;
			case 2: i = 30; break;
			case 3: i = 30; break;
			default: break;
		}
		return i;
	}
	
	public static float ratio {
		get { 
			// Somehow, precalculating this doesn't work!!
			return Screen.height / 768f;
		}
	}
	public static int bulletVelocity = 50;
	
	public static Rect getRectToScreen(Rect rect) {
		/*#if UNITY_IPHONE
		return rect;
		#else*/
		//return rect;
		return new Rect(ratio * rect.x, ratio * rect.y, ratio * rect.width, ratio * rect.height);		
		//#endif	
	}
	
	public static Rect getRectToScreen(float x, float y, float width, float height) {
		/*#if UNITY_IPHONE
		return new Rect(x,y,width,height);
		#else*/
		//return new Rect(x,y,width,height);
		return new Rect(ratio*x,ratio*y,ratio*width, ratio*height);
		//#endif
	}

	
	
	public enum ButtonAbility {
		Aoe, Slow, Cannon, Pea, Dot, Gold,
		Cheapo, Fast, Tank, Zombie, Armour, Boss, Regen
	} 
	
	public static void GoToScene(string scene) {
		int num = sceneNo(scene);
		if (num == -1)
			return;
		else
			Application.LoadLevel(num);
	}
	
	public static int sceneNo(string scene) {
		for(int i=0;i<scenes.Length;i++) {
			if (scenes[i] == scene)
				return i;
		}
		return -1;
	}
	
	public static Dictionary<string, int[]> TowerRanges = new Dictionary<string, int[]>();
	public static Dictionary<string, int[]> TowerAreasOfEffect = new Dictionary<string, int[]>();
	public static Dictionary<string, double[]> TowerRatesOfFire = new Dictionary<string, double[]>();
	public static Dictionary<string, int[]> TowerUpgradeCosts = new Dictionary<string, int[]>();
	public static Dictionary<string, int[]> TowerDamages = new Dictionary<string, int[]>();
	public static Dictionary<string, int> TowerBuildCosts = new Dictionary<string, int>();
	
	
	public static Dictionary<string, int> AlienDeathGold = new Dictionary<string, int>();
	public static Dictionary<string, int> AlienCost = new Dictionary<string, int>();
	public static Dictionary<string, int> AlienMaxHP = new Dictionary<string, int>();
	public static Dictionary<string, float> AlienMovementSpeed = new Dictionary<string, float>();
	public static Dictionary<string, float> AlienCooldown = new Dictionary<string, float>();
	public static Dictionary<string, int> AlienReachEndLifeDeduction = new Dictionary<string, int>();
	public static Dictionary<string, float> AlienBuildTime = new Dictionary<string, float>();
	
	
	public static Rect wholeScreen;
	
	
	public static void InitConstants(){

		if (initialized)
			return;
		initialized = true;
		wholeScreen = getRectToScreen(-512,-384,1024,768);


		AliensLayer = 1 << LayerMask.NameToLayer("Aliens");		

		TowerRanges.Add(typeof(TowerAoe).Name, new int[] {125, 150, 175});
		TowerRanges.Add(typeof(TowerCannon).Name, new int[] {165, 185, 225});
		TowerRanges.Add(typeof(TowerDot).Name, new int[] {135, 165, 180});
		TowerRanges.Add(typeof(TowerGoldGenerator).Name, new int[] {0, 0, 0});
		TowerRanges.Add(typeof(TowerGun).Name, new int[] {115, 130, 150});
		TowerRanges.Add(typeof(TowerSlow).Name, new int[] {135, 165, 195});
		
		TowerAreasOfEffect.Add(typeof(TowerAoe).Name, new int[] {15, 25, 40});
		TowerAreasOfEffect.Add(typeof(TowerCannon).Name, new int[] {0, 5, 10});
		TowerAreasOfEffect.Add(typeof(TowerDot).Name, new int[] {0, 0, 15});
		TowerAreasOfEffect.Add(typeof(TowerGoldGenerator).Name, new int[] {0, 0, 0});
		TowerAreasOfEffect.Add(typeof(TowerGun).Name, new int[] {0, 0, 0});
		TowerAreasOfEffect.Add(typeof(TowerSlow).Name, new int[] {0, 5, 10});
		
		TowerRatesOfFire.Add(typeof(TowerAoe).Name, new double[] {2.0, 2.0, 2.0});
		TowerRatesOfFire.Add(typeof(TowerCannon).Name, new double[] {3.0, 2.75, 2.5});
		TowerRatesOfFire.Add(typeof(TowerDot).Name, new double[] {2.0, 1.5, 1.25});
		TowerRatesOfFire.Add(typeof(TowerGoldGenerator).Name, new double[] {4.0, 4.0, 4.0});
		TowerRatesOfFire.Add(typeof(TowerGun).Name, new double[] {1.3, 1.0, 0.5});
		TowerRatesOfFire.Add(typeof(TowerSlow).Name, new double[] {2.0, 1.5, 1.0});
		
		TowerUpgradeCosts.Add(typeof(TowerAoe).Name, new int[] {19, 30});
		TowerUpgradeCosts.Add(typeof(TowerCannon).Name, new int[] {15, 25});
		TowerUpgradeCosts.Add(typeof(TowerDot).Name, new int[] {17, 27});
		TowerUpgradeCosts.Add(typeof(TowerGoldGenerator).Name, new int[] {22, 35});
		TowerUpgradeCosts.Add(typeof(TowerGun).Name, new int[] {12, 22});
		TowerUpgradeCosts.Add(typeof(TowerSlow).Name, new int[] {17, 27});
		
		TowerDamages.Add(typeof(TowerAoe).Name, new int[] {60, 100, 140});
		TowerDamages.Add(typeof(TowerCannon).Name, new int[] {100, 150, 250});
		TowerDamages.Add(typeof(TowerDot).Name, new int[] {60, 70, 80});
		TowerDamages.Add(typeof(TowerGoldGenerator).Name, new int[] {0, 0, 0});
		TowerDamages.Add(typeof(TowerGun).Name, new int[] {40, 50, 40});
		TowerDamages.Add(typeof(TowerSlow).Name, new int[] {10, 10, 10});
		
		TowerBuildCosts.Add(typeof(TowerAoe).Name, 17);
		TowerBuildCosts.Add(typeof(TowerCannon).Name, 13);
		TowerBuildCosts.Add(typeof(TowerDot).Name, 15);
		TowerBuildCosts.Add(typeof(TowerGoldGenerator).Name, 20);
		TowerBuildCosts.Add(typeof(TowerGun).Name, 10);
		TowerBuildCosts.Add(typeof(TowerSlow).Name, 15);
		
		AlienDeathGold.Add(typeof(AlienArmouredGrunt).Name, 3);
		AlienDeathGold.Add(typeof(AlienBoss).Name, 14);
		AlienDeathGold.Add(typeof(AlienFast).Name, 3);
		AlienDeathGold.Add(typeof(AlienGrunt).Name, 2);
		AlienDeathGold.Add(typeof(AlienRebirthing).Name, 5);
		AlienDeathGold.Add(typeof(AlienRegenerating).Name, 7);
		
		AlienCost.Add(typeof(AlienArmouredGrunt).Name, 8);
		AlienCost.Add(typeof(AlienBoss).Name, 50);
		AlienCost.Add(typeof(AlienFast).Name, 8);
		AlienCost.Add(typeof(AlienGrunt).Name, 4);
		AlienCost.Add(typeof(AlienRebirthing).Name, 12);
		AlienCost.Add(typeof(AlienRegenerating).Name, 15);
		
		AlienMaxHP.Add(typeof(AlienArmouredGrunt).Name, 300);
		AlienMaxHP.Add(typeof(AlienBoss).Name, 1000);
		AlienMaxHP.Add(typeof(AlienFast).Name, 100);
		AlienMaxHP.Add(typeof(AlienGrunt).Name, 120);
		AlienMaxHP.Add(typeof(AlienRebirthing).Name, 200);
		AlienMaxHP.Add(typeof(AlienRegenerating).Name, 200);
		
		AlienMovementSpeed.Add(typeof(AlienArmouredGrunt).Name, 40);
		AlienMovementSpeed.Add(typeof(AlienBoss).Name, 25);
		AlienMovementSpeed.Add(typeof(AlienFast).Name, 70);
		AlienMovementSpeed.Add(typeof(AlienGrunt).Name, 50);
		AlienMovementSpeed.Add(typeof(AlienRebirthing).Name, 40);
		AlienMovementSpeed.Add(typeof(AlienRegenerating).Name, 40);
		
		AlienCooldown.Add(typeof(AlienArmouredGrunt).Name, 3);
		AlienCooldown.Add(typeof(AlienBoss).Name, 20);
		AlienCooldown.Add(typeof(AlienFast).Name, 5);
		AlienCooldown.Add(typeof(AlienGrunt).Name, 0.75f);
		AlienCooldown.Add(typeof(AlienRebirthing).Name, 5);
		AlienCooldown.Add(typeof(AlienRegenerating).Name, 7);
		
		AlienReachEndLifeDeduction.Add(typeof(AlienArmouredGrunt).Name, 1);
		AlienReachEndLifeDeduction.Add(typeof(AlienBoss).Name, 3);
		AlienReachEndLifeDeduction.Add(typeof(AlienFast).Name, 1);
		AlienReachEndLifeDeduction.Add(typeof(AlienGrunt).Name, 1);
		AlienReachEndLifeDeduction.Add(typeof(AlienRebirthing).Name, 1);
		AlienReachEndLifeDeduction.Add(typeof(AlienRegenerating).Name, 1);
		
		AlienBuildTime.Add(typeof(AlienArmouredGrunt).Name, 4f);
		AlienBuildTime.Add(typeof(AlienBoss).Name, 4f);
		AlienBuildTime.Add(typeof(AlienFast).Name, 4f);
		AlienBuildTime.Add(typeof(AlienGrunt).Name, 4f);
		AlienBuildTime.Add(typeof(AlienRebirthing).Name, 4f);
		AlienBuildTime.Add(typeof(AlienRegenerating).Name, 4f);
	}
}
