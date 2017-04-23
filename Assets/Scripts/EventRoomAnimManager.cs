using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventRoomAnimManager : MonoBehaviour {
	public string fireTag = "Fire";
	public string sparkTag = "Sparks";

	private SpriteRenderer[] animations;
	private List<GameObject> sparks = new List<GameObject>();
	private List<GameObject> fire = new List<GameObject>();

	// Use this for initialization
	void Start () {
		animations = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
		this.ParseAnimations ();
		this.ToggleSparks (false);
		this.ToggleFire (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ParseAnimations(){
		foreach (SpriteRenderer sr in animations) {
			GameObject go = sr.gameObject;
			if (go.CompareTag (sparkTag)) {
				sparks.Add (go);
				Debug.Log ("Added: " + go.name);
			}
			if (go.CompareTag (fireTag)) {
				fire.Add (go);
				Debug.Log ("Added: " + go.name);
			}
		}
	}

	public void ToggleSparks(bool toggle){
		if(animations.Length <= 0 || sparks.Count <= 0){ return; }
		foreach (GameObject go in sparks) {
			go.SetActive (toggle);
		}
	}

	public void ToggleFire(bool toggle){
		if(animations.Length <= 0 || fire.Count <= 0){ return; }
		foreach (GameObject go in fire) {
			go.SetActive (toggle);
		}
	}
}
