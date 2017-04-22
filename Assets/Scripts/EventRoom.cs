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

	private float perilVal = 0;
	private const float MAX_PERIL = 1f;
	private const float MIN_PERIL = 0f;

	public bool inPeril = false; // public for debugging
	public bool plyrInRoom = false; // public for debugging
	public bool pauseTimer = false;

	// Use this for initialization
	void Start () {
		// Init GameManager
		gm = GameManager.instance;

		// Make sure object's trigger is on
		this.gameObject.GetComponent<Collider>().isTrigger = true;

		this.ResetPerilTrigger();
		StartCoroutine(ManagePerilTrigger());
	}

	void FixedUpdate(){
		// Only escalate peril if player not in the room
		if (inPeril && !plyrInRoom) {
			this.incrPerilValue();
		}

		if(inPeril) Debug.Log ("perilValue: " + perilVal);

		/***DECREASE PERIL***/
		/***INCREASE PERIL***/
	}

	void OnTriggerEnter(Collider c){
		// Check that the player entered the room
		if(c.gameObject.CompareTag(PLAYER_TAG)){
			plyrInRoom = true;
			StopCoroutine(ManagePerilTrigger());
			// When room is in perile and character doing "fixing" action
			if(inPeril && Input.GetKeyDown(KeyCode.Space)){
				// Lower perile value by fix rate
				this.decrPerilValue();
			}
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.CompareTag (PLAYER_TAG)){
			plyrInRoom = false;
			StartCoroutine(ManagePerilTrigger());

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
		// Only trigger when player is the room and room
		// is not in peril
		if(!plyrInRoom && !inPeril) {
			while(true) {
				yield return new WaitForSeconds (1f);
				perilCountDownTimer -= 1;

				Debug.Log("Peril Count Down Timer: " + perilCountDownTimer);

				if(perilCountDownTimer <= 0) {
					inPeril = true;
					this.ResetPerilTrigger();
					StopCoroutine(ManagePerilTrigger());
				}
			}

		// Reset timer if player in the room or
		// the room is alread in peril
		} else{ ResetPerilTrigger(); }
	}

	private void EscalalatePeril(){
	}

	private void BeginPeril(){
		StopCoroutine(ManagePerilTrigger());
		inPeril = true;
	}

	private void ResetPerilTrigger(){
		StartCoroutine(ManagePerilTrigger());
		perilCountDownTimer = timeUntilPeril;
	}
	

}
