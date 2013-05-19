using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
	AudioSource[] AudioSources;
	AudioSource MenuMusic;
	AudioSource GameMusic;
	// Use this for initialization
	void Start () {
		AudioSource[] AudioSources = GetComponents<AudioSource>();
		MenuMusic = AudioSources[1];
		GameMusic = AudioSources[0];
		DontDestroyOnLoad(gameObject);
	}
	
	void PlayMenuMusic () {
		if (GameMusic.isPlaying)
			GameMusic.Stop();
		if (!MenuMusic.isPlaying)
			MenuMusic.Play();
	}
	
	void PlayGameMusic() {
		if(MenuMusic.isPlaying)
			MenuMusic.Stop();
		if (!GameMusic.isPlaying)
			GameMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == Constants.sceneNo("game1") ||
			Application.loadedLevel == Constants.sceneNo("game2"))
			PlayGameMusic();
		else
			PlayMenuMusic();
	}
}
