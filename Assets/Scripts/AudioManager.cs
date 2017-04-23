using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public AudioSource[] mxSource; //Contains the different layers of music.

	public AudioSource footstepSource; //Audiosource for footsteps
	public AudioSource shipRumble; //Audiosource that plays loop for when ship is rumbling

	public AudioClip[] footstepsWalk; //holds audioclips for footsteps when walking
	public AudioClip[] footstepsRun; //holds audioclips for footsteps when running
	public AudioClip[] alienVoices; //Holds audioclips for aliens

	public bool footstepIsPlaying = false;
//	bool mxTwoIsPlaying = false;
//	bool mxThreeIsPlaying = false;

	[SerializeField]
	float mx1Vol; //maximum volume of music
	[SerializeField]
	float mxBassVol; //maximum volume of music
	[SerializeField]
	float mx2Vol; //maximum volume of music
	[SerializeField]
	float mxGuitarVol; //maximum volume of music
	[SerializeField]
	float mx3Vol; //maximum volume of music

	[SerializeField]
	float rumbleVol; //maximum volume of ship rumbling


	[SerializeField]
	float fadeInTime; //length of music layer fade in
	[SerializeField]
	float fadeOutTime; //length of music layer fade out




	// Use this for initialization
	void Awake () {
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		mxSource[0].volume = mx1Vol;
		mxSource[1].volume = mxBassVol;

	}
		
	
	// Update is called once per frame
	void Update () {
	
		if (GameManager.instance.HappinessValue < 60 ) {

			StartCoroutine (FadeIn (mxSource[2], fadeInTime, mx2Vol));
			StartCoroutine (FadeIn (mxSource[3], fadeInTime, mxGuitarVol));
			StartCoroutine (FadeOut (mxSource[1], fadeOutTime));


		}

		if (GameManager.instance.HappinessValue > 60) {

			StartCoroutine (FadeOut (mxSource[2], fadeOutTime));
			StartCoroutine (FadeOut (mxSource[3], fadeOutTime));

			StartCoroutine (FadeIn (mxSource[1], fadeInTime, mxBassVol));
		
		}

		if (GameManager.instance.HappinessValue < 30 ) {
		
			StartCoroutine (FadeIn (mxSource[4], fadeInTime, mx3Vol));
			StartCoroutine (FadeOut (mxSource[3], fadeOutTime));
		
		}

		if (GameManager.instance.HappinessValue > 30 && GameManager.instance.HappinessValue < 60  ) {
		
			StartCoroutine (FadeOut (mxSource[4], fadeOutTime));
			StartCoroutine (FadeIn (mxSource[3], fadeInTime, mxGuitarVol));

		}
	
	}


	IEnumerator FadeOut (AudioSource audioSource, float fadeTime) {
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
		
			yield return null;
		}
	}


	IEnumerator FadeIn (AudioSource audioSource, float fadeTime, float vol) {

		while (audioSource.volume < vol) {
			audioSource.volume += 0.1f * Time.deltaTime / fadeTime;

			yield return null;
		}
	}

	//function is called from the footstep controller script on the player
	public void PlayFootstepWalk (){

		if (footstepIsPlaying == false) {	

			footstepIsPlaying = true;

			footstepSource.clip = footstepsWalk [Random.Range (0, footstepsWalk.Length)];
			footstepSource.pitch = Random.Range (0.75f, 1.5f);
			footstepSource.volume = Random.Range (0.75f, 0.85f);
			footstepSource.Play ();
		}

	}


	//function is called from the footstep controller script on the player
	public void PlayFootstepRun (){

		if (footstepIsPlaying == false){	

			footstepIsPlaying = true;

			footstepSource.clip = footstepsRun [Random.Range (0, footstepsRun.Length)];
			footstepSource.pitch = Random.Range (0.75f, 1.5f);
			footstepSource.volume = Random.Range (0.75f, 0.85f);
			footstepSource.Play ();
		}

		}
	}





