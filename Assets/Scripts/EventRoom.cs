using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EventRoom : MonoBehaviour {

	private GameManager gm;
	private float fixRate = 1f;
	private float fragRate = 0.1f;

	public float perileVal = 0;
	public bool inPerile = false;

	// Use this for initialization
	void Start () {
		gm = GameManager.instance;
		this.gameObject.GetComponent<Collider>().isTrigger = true;
	}

	public void OnTriggerEnter(Collider c){
		// if character enters room
			// if room inPerile
				// if character doing "fixing" action
					// lower perileVal by n (fixRate)
			// else
				// do nothing
	}
	

}
