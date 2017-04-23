using UnityEngine;
using System.Collections;

public class FootStepController : MonoBehaviour {
    SpriteRenderer spriteRender;
    // Use this for initialization
    void Start () {
		spriteRender = GetComponent<SpriteRenderer> ();


	}

	void Update(){

		StartCoroutine (Footsteps());
	
	}
		
	IEnumerator Footsteps () {

		if (spriteRender.sprite.name == "firstStep"){
			
			AudioManager.instance.PlayFootstepRun ();

			yield return new WaitForSeconds (AudioManager.instance.footstepSource.clip.length);
			AudioManager.instance.footstepIsPlaying = false;
		}
		else 
			yield return null;
	}
}
