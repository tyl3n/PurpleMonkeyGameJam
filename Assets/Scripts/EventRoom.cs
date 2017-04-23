using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EventRoom : MonoBehaviour {

	public float timeUntilPeril = 10f; // in seconds
	public float perilDecrRate = 1f;
	public float perilIncrRate = 0.1f;


	private string PLAYER_TAG = "Player";
	private GameManager gm;

	private float perilCountDownTimer;

	private float perilVal;
	private const float MAX_PERIL = 1f;
	private const float MIN_PERIL = 0f;

	public bool inPeril = false; // public for debugging
	public bool plyrInRoom = false; // public for debugging

	// Use this for initialization
	void Start () {
		// Init GameManager
		gm = GameManager.instance;

		// Make sure object's trigger is on
		this.gameObject.GetComponent<Collider>().isTrigger = true;

		perilVal = MIN_PERIL;

		this.ResetPerilTrigger();
		StartCoroutine(ManagePerilTrigger());
	}

	void FixedUpdate(){

		if(inPeril) Debug.Log ("perilValue: " + perilVal);

		if(plyrInRoom) {
			/***DECREASE PERIL***/
			// When room is in perile and character doing "fixing" action
			if(inPeril && Input.GetKeyDown (KeyCode.Space)) {
				// Lower perile value by fix rate
				Invoke("decrPerilValue",1);
			}
		} else {

			/***INCREASE PERIL***/
			if(inPeril) {
				incrPerilValue();
				//				Invoke("incrPerilValue",1);
			}
		}
	}

	void OnTriggerEnter(Collider c){
		// Check that the player entered the room
		if(c.gameObject.CompareTag(PLAYER_TAG)){
			plyrInRoom = true;
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.CompareTag (PLAYER_TAG)){
			plyrInRoom = false;

			// Make sure peril does not increase immediately
			// after player has left the room
			this.ResetPerilTrigger();
		}
	}

	public float GetPerilValue(){ return perilVal; }

	/// Increase peril value
	private void incrPerilValue(){
		changePerilValue(1f, perilIncrRate);
	}

	/// Decrease peril value
	private void decrPerilValue(){
		changePerilValue(-1f, perilDecrRate);
	}

	/// Change peril value based dir (positive or negative)
	/// and rate.
	private void changePerilValue(float dir, float rate){
		perilVal += dir * rate;
		if(perilVal > MAX_PERIL){ perilVal = MAX_PERIL; }
		if(perilVal < MIN_PERIL){ perilVal = MIN_PERIL; }
	}

	/// Decide when a room is in peril
	private IEnumerator ManagePerilTrigger(){


		while(true) {
			yield return new WaitForSeconds (1f);
			// Only trigger when player is the room and room
			// is not in peril
			if(!plyrInRoom && !inPeril) {
				perilCountDownTimer -= 1;
				if(perilCountDownTimer <= 0) {
					Debug.Log("!!!!!TRIGGERED!!!!!");
					this.ResetPerilTrigger ();
					inPeril = true;
				}
			} else{ ResetPerilTrigger(); }

			Debug.Log("Peril Count Down Timer: " + perilCountDownTimer);				
		}



	}

	private void EscalalatePeril(){
	}

	private void ResetPerilTrigger(){
		perilCountDownTimer = timeUntilPeril;
	}


}
