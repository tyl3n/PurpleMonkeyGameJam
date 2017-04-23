using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EventRoom : MonoBehaviour {

	public float timeUntilPeril = 10f; // in seconds
	public float roomHealAmount = -0.1f;
	public float roomHealSpeed = 0.5f; // in seconds
	public float roomBreakAmount = 0.1f;
	public float roomBreakSpeed = 0.5f; // in seconds


	private string PLAYER_TAG = "Player";
	private GameManager gm;

	private float perilCountDownTimer;

	private float perilValue;
	private const float MAX_PERIL = 1f;
	private const float MIN_PERIL = 0f;

	private float perilChangeRate = 1f;
	private float perilChangeAmount = 0;

	public bool inPeril = false; // public for debugging
	public bool plyrInRoom = false; // public for debugging

	// Use this for initialization
	void Start () {
		// Init GameManager
		gm = GameManager.instance;

		// Make sure object's trigger is on
		this.gameObject.GetComponent<Collider>().isTrigger = true;

		perilValue = MIN_PERIL;

		this.ResetPerilTrigger();
		StartCoroutine(ManagePerilTrigger());
		StartCoroutine(ChangePerilValue());
	}

	void FixedUpdate(){
		if(inPeril){ Debug.Log ("perilValue: " + perilValue); }

		if(plyrInRoom) {
			/***DECREASE PERIL***/
			// When room is in perile and character doing "fixing" action
			if(inPeril){
				this.DontChangePerilValue();
				if (Input.GetKeyUp(KeyCode.Space)) {
					// Lower perile value by fix rate
//					this.DecrPerilValue();
					this.ManuallyDecrPerilValue();

				} else if(Input.GetKey(KeyCode.Space)){
					this.DecrPerilValue();
				}
			}
		} else {

			/***INCREASE PERIL***/
			if(inPeril) {
				this.IncrPerilValue();
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

	public float GetPerilValue(){ return perilValue; }
	public float GetMaxPerilValue(){ return MAX_PERIL; }

	/// Increase peril value
	private void IncrPerilValue(){
//		ChangePerilValue(1f, perilIncrRate);
		perilChangeRate = roomBreakSpeed;
		perilChangeAmount = roomBreakAmount;
	}

	/// Decrease peril value
	private void DecrPerilValue(){
//		ChangePerilValue(-1f, perilDecrRate);
		perilChangeRate = roomHealSpeed;
		perilChangeAmount = roomHealAmount;
		if(perilValue <= MIN_PERIL){ inPeril = false; }
	}

	private void ManuallyDecrPerilValue(){
		perilValue += roomHealAmount;
		if(perilValue > MAX_PERIL){ perilValue = MAX_PERIL; }
		if(perilValue < MIN_PERIL){ perilValue = MIN_PERIL; }
		if(perilValue <= MIN_PERIL){ inPeril = false; }
	}

	private void DontChangePerilValue(){
		perilChangeRate = 1f;
		perilChangeAmount = 0;
	}

	/// Change peril value based dir (positive or negative)
	/// and rate.
	private IEnumerator ChangePerilValue(){
		while(true){
			yield return new WaitForSeconds(perilChangeRate);

			if(inPeril){
				perilValue += perilChangeAmount;
				if(perilValue > MAX_PERIL){ perilValue = MAX_PERIL; }
				if(perilValue < MIN_PERIL){ perilValue = MIN_PERIL; }

			} else{ 
				this.DontChangePerilValue();
			}
		}
	}

	/// Decide when a room is in peril
	private IEnumerator ManagePerilTrigger(){
		while(true) {
			yield return new WaitForSeconds (1f);

			// Only trigger when player is not in the room
			// and the room is not in peril
			if(!plyrInRoom && !inPeril) {

				// Count down (to 0)
				perilCountDownTimer -= 1;

				// Initiate increase in danger/chaos/peril
				if(perilCountDownTimer <= 0) {
					Debug.Log("!!!!!TRIGGERED!!!!!");
					this.ResetPerilTrigger();
					inPeril = true;
				}
				Debug.Log("Peril Count Down Timer: " + perilCountDownTimer);
			} else{ ResetPerilTrigger(); }
		}
	}

	private void EscalalatePeril(){
	}

	private void ResetPerilTrigger(){
		perilCountDownTimer = timeUntilPeril;
	}


}
