using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PrefabManager
{
    public static bool initialized = false;
    public static Dictionary<string, GameObject> PrefabAliens = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> PrefabTowers = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject[]> PrefabTowerBullets = new Dictionary<string, GameObject[]>();
	public static Dictionary<string, AudioClip> TowerShotSounds = new Dictionary<string, AudioClip>();
	
	public static AudioClip SpawnSound;
	public static AudioClip ReachEndSound;
	
    public static GameObject PrefabHealthBar;
    public static GameObject PrefabFiringRadius;
    public static GameObject PrefabTowerSpawnPoint;
    public static GameObject PrefabUIRing;
    public static GameObject UICardMask;
	public static GameObject PrefabPoisonCloud;
	public static GameObject PrefabAoeShockwave;
	public static GameObject PrefabSlowEffectGlow;
    
    public static GameObject UIButtonCard;
    public static GameObject UIButtonPauseMenu;
    public static GameObject UIFrame;
    public static GameObject UIButtonSell;
    public static GameObject UIButtonUpgrade;
    
    public static Texture UISelected;
    public static Texture UIConfirm;
    public static Texture UIBlockLowerDefault;
    public static Texture UIBlockLowerSellUpgrade;
    
    public static Texture UICountDown1;
    public static Texture UICountDown2;
    public static Texture UICountDown3;
	public static GameObject UIScreenText;
	public static GameObject UIScreenTextController;
    
    public static Dictionary<string,Texture> PrefabCards = new Dictionary<string, Texture>();
    public static Dictionary<string,Texture> PrefabIcons = new Dictionary<string, Texture>();
    public static Dictionary<char,Texture> PrefabTopPanelText = new Dictionary<char, Texture>();
	public static Dictionary<char,Texture> PrefabScreenText = new Dictionary<char, Texture>();
    public static GameObject UIAlienIcon;
    public static Texture Invisible;
    public static GameObject Gamesession1;
	public static GameObject Gamesession2;
	public static GameObject UIAttackerMask;
	public static Dictionary<string,Texture> SelectText = new Dictionary<string, Texture>();
	public static Dictionary<string,Texture> CardDetails = new Dictionary<string, Texture>();
	public static Dictionary<string,Texture> UpgradeDetails = new Dictionary<string, Texture>();
	public static Dictionary<string,Texture> SellDetails = new Dictionary<string, Texture>();
	
    public static void LoadPrefabs(){
        if (initialized)
                return;
        
        initialized = true;
		
		UpgradeDetails.Add(typeof(TowerAoe).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerAoe1"));
		UpgradeDetails.Add(typeof(TowerAoe).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerAoe2"));
		UpgradeDetails.Add(typeof(TowerCannon).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerCannon1"));
		UpgradeDetails.Add(typeof(TowerCannon).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerCannon2"));
		UpgradeDetails.Add(typeof(TowerDot).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerDot1"));
		UpgradeDetails.Add(typeof(TowerDot).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerDot2"));
		UpgradeDetails.Add(typeof(TowerGoldGenerator).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerGoldGenerator1"));
		UpgradeDetails.Add(typeof(TowerGoldGenerator).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerGoldGenerator2"));
		UpgradeDetails.Add(typeof(TowerGun).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerGun1"));
		UpgradeDetails.Add(typeof(TowerGun).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerGun2"));
		UpgradeDetails.Add(typeof(TowerSlow).Name+"1",(Texture) Resources.Load("Materials/Details/UpgradeTowerSlow1"));
		UpgradeDetails.Add(typeof(TowerSlow).Name+"2",(Texture) Resources.Load("Materials/Details/UpgradeTowerSlow2"));
		UpgradeDetails.Add("UpgradeNone",(Texture) Resources.Load("Materials/Details/UpgradeNone"));
		
		SellDetails.Add (typeof(TowerAoe).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerAoe1"));
		SellDetails.Add (typeof(TowerAoe).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerAoe2"));
		SellDetails.Add (typeof(TowerAoe).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerAoe3"));
		SellDetails.Add (typeof(TowerCannon).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerCannon1"));
		SellDetails.Add (typeof(TowerCannon).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerCannon2"));
		SellDetails.Add (typeof(TowerCannon).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerCannon3"));
		SellDetails.Add (typeof(TowerDot).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerDot1"));
		SellDetails.Add (typeof(TowerDot).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerDot2"));
		SellDetails.Add (typeof(TowerDot).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerDot3"));
		SellDetails.Add (typeof(TowerGoldGenerator).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerGoldGenerator1"));
		SellDetails.Add (typeof(TowerGoldGenerator).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerGoldGenerator2"));
		SellDetails.Add (typeof(TowerGoldGenerator).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerGoldGenerator3"));
		SellDetails.Add (typeof(TowerGun).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerGun1"));
		SellDetails.Add (typeof(TowerGun).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerGun2"));
		SellDetails.Add (typeof(TowerGun).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerGun3"));
		SellDetails.Add (typeof(TowerSlow).Name+"1", (Texture) Resources.Load("Materials/Details/SellTowerSlow1"));
		SellDetails.Add (typeof(TowerSlow).Name+"2", (Texture) Resources.Load("Materials/Details/SellTowerSlow2"));
		SellDetails.Add (typeof(TowerSlow).Name+"3", (Texture) Resources.Load("Materials/Details/SellTowerSlow3"));
		
		Invisible = (Texture) Resources.Load("Materials/invisible");
		SelectText.Add(typeof(TowerAoe).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerAoe1"));
		SelectText.Add(typeof(TowerAoe).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerAoe2"));
		SelectText.Add(typeof(TowerAoe).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerAoe3"));
		SelectText.Add(typeof(TowerCannon).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerCannon1"));
		SelectText.Add(typeof(TowerCannon).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerCannon2"));
		SelectText.Add(typeof(TowerCannon).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerCannon3"));
		SelectText.Add(typeof(TowerDot).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerDot1"));
		SelectText.Add(typeof(TowerDot).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerDot2"));
		SelectText.Add(typeof(TowerDot).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerDot3"));
		SelectText.Add(typeof(TowerGoldGenerator).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerGoldGenerator1"));
		SelectText.Add(typeof(TowerGoldGenerator).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerGoldGenerator2"));
		SelectText.Add(typeof(TowerGoldGenerator).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerGoldGenerator3"));
		SelectText.Add(typeof(TowerGun).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerGun1"));
		SelectText.Add(typeof(TowerGun).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerGun2"));
		SelectText.Add(typeof(TowerGun).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerGun3"));
		SelectText.Add(typeof(TowerSlow).Name+"1", (Texture) Resources.Load ("Materials/Details/SelectTowerSlow1"));
		SelectText.Add(typeof(TowerSlow).Name+"2", (Texture) Resources.Load ("Materials/Details/SelectTowerSlow2"));
		SelectText.Add(typeof(TowerSlow).Name+"3", (Texture) Resources.Load ("Materials/Details/SelectTowerSlow3"));
		SelectText.Add("NoUpgrade", (Texture) Resources.Load ("Materials/Details/SelectNoUpgrade"));
		
		CardDetails.Add(typeof(AlienArmouredGrunt).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienArmouredGrunt"));
		CardDetails.Add(typeof(AlienBoss).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienBoss"));
		CardDetails.Add(typeof(AlienFast).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienFast"));
		CardDetails.Add(typeof(AlienGrunt).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienGrunt"));
		CardDetails.Add(typeof(AlienRebirthing).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienRebirthing"));
		CardDetails.Add(typeof(AlienRegenerating).Name, (Texture) Resources.Load ("Materials/Details/DetailsAlienRegenerating"));
		
		CardDetails.Add(typeof(TowerAoe).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerAoe"));
		CardDetails.Add(typeof(TowerCannon).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerCannon"));
		CardDetails.Add(typeof(TowerDot).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerDot"));
		CardDetails.Add(typeof(TowerGoldGenerator).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerGoldGenerator"));
		CardDetails.Add(typeof(TowerGun).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerGun"));
		CardDetails.Add(typeof(TowerSlow).Name, (Texture) Resources.Load ("Materials/Details/DetailsTowerSlow"));		
		
		
		UIScreenTextController = (GameObject) Resources.Load("Prefabs/UI/UIScreenTextController");
		UIScreenText = (GameObject) Resources.Load("Prefabs/UI/UIScreenText");
        Gamesession1 = (GameObject) Resources.Load("Prefabs/GameSession1");
		Gamesession2 = (GameObject) Resources.Load("Prefabs/GameSession2");
        UICardMask = (GameObject) Resources.Load("Prefabs/UI/UICardMask");
        UIAlienIcon = (GameObject) Resources.Load("Prefabs/UI/UIAlienIcon");
        UIAttackerMask = (GameObject) Resources.Load("Prefabs/UI/UIAttackerMask");
		
        UICountDown1 = (Texture) Resources.Load ("Materials/UICountDown1");
        UICountDown2 = (Texture) Resources.Load ("Materials/UICountDown2");
        UICountDown3 = (Texture) Resources.Load ("Materials/UICountDown3");
        
        UISelected = (Texture) Resources.Load ("Materials/UISelected");
        UIConfirm = (Texture) Resources.Load ("Materials/UIConfirm");
        UIBlockLowerDefault = (Texture) Resources.Load ("Materials/UIBlockLowerDefault");
        UIBlockLowerSellUpgrade = (Texture) Resources.Load ("Materials/UIBlockLowerSellUpgrade");

        PrefabTopPanelText.Add('0',(Texture) Resources.Load ("Materials/UIBarText0"));
        PrefabTopPanelText.Add('1',(Texture) Resources.Load ("Materials/UIBarText1"));
        PrefabTopPanelText.Add('2',(Texture) Resources.Load ("Materials/UIBarText2"));
        PrefabTopPanelText.Add('3',(Texture) Resources.Load ("Materials/UIBarText3"));
        PrefabTopPanelText.Add('4',(Texture) Resources.Load ("Materials/UIBarText4"));
        PrefabTopPanelText.Add('5',(Texture) Resources.Load ("Materials/UIBarText5"));
        PrefabTopPanelText.Add('6',(Texture) Resources.Load ("Materials/UIBarText6"));
        PrefabTopPanelText.Add('7',(Texture) Resources.Load ("Materials/UIBarText7"));
        PrefabTopPanelText.Add('8',(Texture) Resources.Load ("Materials/UIBarText8"));
        PrefabTopPanelText.Add('9',(Texture) Resources.Load ("Materials/UIBarText9"));
        PrefabTopPanelText.Add('/',(Texture) Resources.Load ("Materials/UIBarTextDiv"));
        PrefabTopPanelText.Add(' ',(Texture) Resources.Load ("Materials/UIBarTextBlank"));
		PrefabTopPanelText.Add('.',(Texture) Resources.Load ("Materials/UIBarTextDot"));
		PrefabTopPanelText.Add('_',(Texture) Resources.Load ("Materials/UIBarTextUnderScore"));
		
		PrefabScreenText.Add('0',(Texture) Resources.Load ("Materials/UISCreenText0"));
		PrefabScreenText.Add('1',(Texture) Resources.Load ("Materials/UISCreenText1"));
		PrefabScreenText.Add('2',(Texture) Resources.Load ("Materials/UISCreenText2"));
		PrefabScreenText.Add('3',(Texture) Resources.Load ("Materials/UISCreenText3"));
		PrefabScreenText.Add('4',(Texture) Resources.Load ("Materials/UISCreenText4"));
		PrefabScreenText.Add('5',(Texture) Resources.Load ("Materials/UISCreenText5"));
		PrefabScreenText.Add('6',(Texture) Resources.Load ("Materials/UISCreenText6"));
		PrefabScreenText.Add('7',(Texture) Resources.Load ("Materials/UISCreenText7"));
		PrefabScreenText.Add('8',(Texture) Resources.Load ("Materials/UISCreenText8"));
		PrefabScreenText.Add('9',(Texture) Resources.Load ("Materials/UISCreenText9"));
		PrefabScreenText.Add('$',(Texture) Resources.Load ("Materials/UISCreenTextDollar"));
		PrefabScreenText.Add('-',(Texture) Resources.Load ("Materials/UISCreenTextMinus"));
		PrefabScreenText.Add('+',(Texture) Resources.Load ("Materials/UISCreenTextPlus"));
		PrefabScreenText.Add('#',(Texture) Resources.Load("Materials/UIScreenTextPoison"));
        
        PrefabIcons.Add(typeof(AlienArmouredGrunt).Name, (Texture) Resources.Load("Materials/IconAlienArmouredGrunt"));
        PrefabIcons.Add(typeof(AlienBoss).Name, (Texture) Resources.Load("Materials/IconAlienBoss"));
        PrefabIcons.Add(typeof(AlienFast).Name, (Texture) Resources.Load("Materials/IconAlienFast"));
        PrefabIcons.Add(typeof(AlienGrunt).Name, (Texture) Resources.Load("Materials/IconAlienGrunt"));
        PrefabIcons.Add(typeof(AlienRebirthing).Name, (Texture) Resources.Load("Materials/IconAlienRebirthing"));
        PrefabIcons.Add(typeof(AlienRegenerating).Name, (Texture) Resources.Load("Materials/IconAlienRegenerating"));
        
        PrefabCards.Add("NoUpgrade", (Texture) Resources.Load("Materials/CardNoUpgrade"));
        
        PrefabCards.Add(typeof(AlienArmouredGrunt).Name, (Texture) Resources.Load("Materials/CardAlienArmouredGrunt"));
        PrefabCards.Add(typeof(AlienBoss).Name, (Texture) Resources.Load("Materials/CardAlienBoss"));
        PrefabCards.Add(typeof(AlienFast).Name, (Texture) Resources.Load("Materials/CardAlienFast"));
        PrefabCards.Add(typeof(AlienGrunt).Name, (Texture) Resources.Load("Materials/CardAlienGrunt"));
        PrefabCards.Add(typeof(AlienRebirthing).Name, (Texture) Resources.Load("Materials/CardAlienRebirthing"));
        PrefabCards.Add(typeof(AlienRegenerating).Name, (Texture) Resources.Load("Materials/CardAlienRegenerating"));
        
        PrefabCards.Add(typeof(AlienArmouredGrunt).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienArmouredGruntSelected"));
        PrefabCards.Add(typeof(AlienBoss).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienBossSelected"));
        PrefabCards.Add(typeof(AlienFast).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienFastSelected"));
        PrefabCards.Add(typeof(AlienGrunt).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienGruntSelected"));
        PrefabCards.Add(typeof(AlienRebirthing).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienRebirthingSelected"));
        PrefabCards.Add(typeof(AlienRegenerating).Name+"Selected", (Texture) Resources.Load("Materials/CardAlienRegeneratingSelected"));
        
        PrefabCards.Add(typeof(TowerAoe).Name+"1", (Texture) Resources.Load("Materials/CardTowerAoe1"));
        PrefabCards.Add(typeof(TowerCannon).Name+"1", (Texture) Resources.Load("Materials/CardTowerCannon1"));
        PrefabCards.Add(typeof(TowerGoldGenerator).Name+"1", (Texture) Resources.Load("Materials/CardTowerGoldGenerator1"));
        PrefabCards.Add(typeof(TowerDot).Name+"1", (Texture) Resources.Load("Materials/CardTowerDot1"));
        PrefabCards.Add(typeof(TowerGun).Name+"1", (Texture) Resources.Load("Materials/CardTowerGun1"));
        PrefabCards.Add(typeof(TowerSlow).Name+"1", (Texture) Resources.Load("Materials/CardTowerSlow1"));
        
        PrefabCards.Add(typeof(TowerAoe).Name+"2", (Texture) Resources.Load("Materials/CardTowerAoe2"));
        PrefabCards.Add(typeof(TowerCannon).Name+"2", (Texture) Resources.Load("Materials/CardTowerCannon2"));
        PrefabCards.Add(typeof(TowerGoldGenerator).Name+"2", (Texture) Resources.Load("Materials/CardTowerGoldGenerator2"));
        PrefabCards.Add(typeof(TowerDot).Name+"2", (Texture) Resources.Load("Materials/CardTowerDot2"));
        PrefabCards.Add(typeof(TowerGun).Name+"2", (Texture) Resources.Load("Materials/CardTowerGun2"));
        PrefabCards.Add(typeof(TowerSlow).Name+"2", (Texture) Resources.Load("Materials/CardTowerSlow2"));
        
        PrefabCards.Add(typeof(TowerAoe).Name+"3", (Texture) Resources.Load("Materials/CardTowerAoe3"));
        PrefabCards.Add(typeof(TowerCannon).Name+"3", (Texture) Resources.Load("Materials/CardTowerCannon3"));
        PrefabCards.Add(typeof(TowerGoldGenerator).Name+"3", (Texture) Resources.Load("Materials/CardTowerGoldGenerator3"));
        PrefabCards.Add(typeof(TowerDot).Name+"3", (Texture) Resources.Load("Materials/CardTowerDot3"));
        PrefabCards.Add(typeof(TowerGun).Name+"3", (Texture) Resources.Load("Materials/CardTowerGun3"));
        PrefabCards.Add(typeof(TowerSlow).Name+"3", (Texture) Resources.Load("Materials/CardTowerSlow3"));

        PrefabCards.Add(typeof(TowerAoe).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerAoeSelected"));
        PrefabCards.Add(typeof(TowerCannon).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerCannonSelected"));
        PrefabCards.Add(typeof(TowerGoldGenerator).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerGoldGeneratorSelected"));
        PrefabCards.Add(typeof(TowerDot).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerDotSelected"));
        PrefabCards.Add(typeof(TowerGun).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerGunSelected"));
        PrefabCards.Add(typeof(TowerSlow).Name+"Selected", (Texture) Resources.Load("Materials/CardTowerSlowSelected"));

        PrefabCards.Add(typeof(TowerAoe).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerAoeConfirm"));
        PrefabCards.Add(typeof(TowerCannon).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerCannonConfirm"));
        PrefabCards.Add(typeof(TowerGoldGenerator).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerGoldGeneratorConfirm"));
        PrefabCards.Add(typeof(TowerDot).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerDotConfirm"));
        PrefabCards.Add(typeof(TowerGun).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerGunConfirm"));
        PrefabCards.Add(typeof(TowerSlow).Name+"Confirm", (Texture) Resources.Load("Materials/CardTowerSlowConfirm"));
        
        
        PrefabHealthBar = (GameObject) Resources.Load ("Prefabs/PrefabHealthBar");
        PrefabFiringRadius = (GameObject) Resources.Load ("Prefabs/UI/PrefabFiringRadius");
        PrefabUIRing = (GameObject) Resources.Load("Prefabs/UI/PrefabUIRing");
        PrefabTowerSpawnPoint = (GameObject) Resources.Load("Prefabs/PrefabTowerSpawnPoint");
        UIButtonCard = (GameObject) Resources.Load("Prefabs/UI/UIButtonCard");
        UIButtonPauseMenu = (GameObject) Resources.Load("Prefabs/UI/UIButtonPauseMenu");
        UIFrame = (GameObject) Resources.Load("Prefabs/UI/UIFrame");
        UIButtonSell = (GameObject) Resources.Load("Prefabs/UI/UIButtonSell");
        UIButtonUpgrade = (GameObject) Resources.Load("Prefabs/UI/UIButtonUpgrade");
        
        PrefabAliens.Add(typeof(AlienArmouredGrunt).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienArmouredGrunt"));
        PrefabAliens.Add(typeof(AlienBoss).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienBoss"));
        PrefabAliens.Add(typeof(AlienFast).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienFast"));
        PrefabAliens.Add(typeof(AlienGrunt).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienGrunt"));
        PrefabAliens.Add(typeof(AlienRebirthing).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienRebirthing"));
        PrefabAliens.Add(typeof(AlienRegenerating).Name, (GameObject) Resources.Load("Prefabs/Aliens/PrefabAlienRegenerating"));
        
        PrefabTowers.Add(typeof(TowerAoe).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerAoe"));
        PrefabTowers.Add(typeof(TowerCannon).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerCannon"));
        PrefabTowers.Add(typeof(TowerGoldGenerator).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerGoldGenerator"));
        PrefabTowers.Add(typeof(TowerDot).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerDot"));
        PrefabTowers.Add(typeof(TowerGun).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerGun"));
        PrefabTowers.Add(typeof(TowerSlow).Name, (GameObject) Resources.Load("Prefabs/Towers/PrefabTowerSlow"));
        
        UnityEngine.Object[] objs = Resources.LoadAll("Prefabs/Bullets/AoeTowerBullets", typeof(GameObject));
        GameObject[] bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerAoe).Name, bullets);
        
        objs = Resources.LoadAll("Prefabs/Bullets/CannonTowerBullets", typeof(GameObject));
        bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerCannon).Name, bullets);
        
        objs = Resources.LoadAll("Prefabs/Bullets/DotTowerBullets", typeof(GameObject));
        bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerDot).Name, bullets);
        
        objs = Resources.LoadAll("Prefabs/Bullets/GoldGeneratorTowerBullets", typeof(GameObject));
        bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerGoldGenerator).Name, bullets);
        
        objs = Resources.LoadAll("Prefabs/Bullets/GunTowerBullets", typeof(GameObject));
        bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerGun).Name, bullets);
        
        objs = Resources.LoadAll("Prefabs/Bullets/SlowTowerBullets", typeof(GameObject));
        bullets = objs.Select(r => (r as GameObject)).Where(r => r != null).OrderBy(t => t.name).ToArray<GameObject>();
        PrefabTowerBullets.Add(typeof(TowerSlow).Name, bullets);
	
		AudioClip ac = (AudioClip) Resources.Load ("Sounds/SoundFX/AoE/AoETowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerAoe).Name, ac);
		
		ac = (AudioClip) Resources.Load ("Sounds/SoundFX/Cannon/CannonTowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerCannon).Name, ac);
		
		ac = (AudioClip) Resources.Load ("Sounds/SoundFX/DoT/DoTTowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerDot).Name, ac);
		
		ac = (AudioClip) Resources.Load ("Sounds/SoundFX/Gold/GoldTowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerGoldGenerator).Name, ac);
		
		ac = (AudioClip) Resources.Load ("Sounds/SoundFX/Gun/GunTowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerGun).Name, ac);
		
		ac = (AudioClip) Resources.Load ("Sounds/SoundFX/Slow/SlowTowerShot", typeof(AudioClip));
		TowerShotSounds.Add (typeof(TowerSlow).Name, ac);
		
		PrefabPoisonCloud = (GameObject) Resources.Load ("Prefabs/PoisonCloud");
		PrefabAoeShockwave = (GameObject) Resources.Load("Prefabs/AoeShockwave");
		PrefabSlowEffectGlow = (GameObject) Resources.Load("Prefabs/SlowEffectGlow");
		
		SpawnSound = (AudioClip) Resources.Load("Sounds/SoundFX/SpawnSound", typeof(AudioClip));
		ReachEndSound = (AudioClip) Resources.Load("Sounds/SoundFX/ReachEndSound", typeof(AudioClip));
    }

}
