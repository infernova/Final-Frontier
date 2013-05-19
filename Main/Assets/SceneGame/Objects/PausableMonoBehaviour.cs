using UnityEngine;
using System.Collections;

public class PausableMonoBehaviour : MonoBehaviour {
	public static bool isPaused;
	
	public static void Pause() {
		ButtonPauseMenu.Instance.hide();
		isPaused = true;
		Time.timeScale = 0;
		GameSession.Instance.refreshSelected();
		ButtonPauseMenu.Instance.hide ();
		ButtonPauseMenu.Instance.menuDown();
	}
			
	
	public static void Resume(bool spec = true) {
		isPaused = false;
		Time.timeScale = 1;
		GameSession.Instance.refreshSelected();
		if (spec)
			ButtonPauseMenu.Instance.menuUp(false);
	}
	
	protected Coroutine _sync(){
        return StartCoroutine(PauseRoutine()); 
    }
       
    protected IEnumerator PauseRoutine(){
        while (PausableMonoBehaviour.isPaused) {
            yield return new WaitForFixedUpdate(); 
        }
        yield return new WaitForEndOfFrame();   
    }
}
