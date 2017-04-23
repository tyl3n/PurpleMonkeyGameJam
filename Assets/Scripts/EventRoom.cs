using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EventRoom : MonoBehaviour {
	
	public string playerTag = "Player";
	public int countDownTimerMin = 3; // inclusive
	public int countDownTimerMax = 10; // exclusive
	public float roomHealAmount = 0.05f;
	public float roomHealRate = 0.5f; // in seconds
	public float roomBreakAmount = 0.01f;
	public float roomBreakRate = 0.1f; // in seconds

	private GameManager gm;

	private float countDownTimer;
	public int countDownTimerReset = 3; // global and public for debugging

	public float perilValue; // public for debugging
	private const float MAX_PERIL = 1f;
	private const float MIN_PERIL = 0f;

	private float perilChangeRate = 1f;
	private float perilChangeAmount = 0;

	public bool roomIsDead = false; // public for debugging
	public bool inPeril = false; // public for debugging
	public bool plyrInRoom = false; // public for debugging

	// Use this for initialization
	void Start () {
		// Init GameManager
		gm = GameManager.instance;

		// Make sure object's trigger is on
		this.gameObject.GetComponent<Collider>().isTrigger = true;

		// Init peril/danger and timer values
		perilValue = MIN_PERIL;
		this.ResetCountDownTimer();

		// Begin count down timer and peril/danger tracker
		StartCoroutine(this.ManageCountDownTimer());
		StartCoroutine(this.ManagePerilValue());
	}

	void FixedUpdate(){
		if (gm.IsGameWon () || gm.IsGameOver ()) {
			StopAllCoroutines();
		}

		// Check if room is dead from too much peril
		if (perilValue >= MAX_PERIL) {
			roomIsDead = true;
		} else{ roomIsDead = false; }


		/***DECREASE PERIL***/
		if(plyrInRoom) {
			// Heal if room is in peril and character
			// is doing "fixing" action
			if(inPeril){
				// Prevent peril value from decreasing without user input
				this.DontChangePerilValue();

				// Lower peril value by fix rate by mashing the "heal" key
				if (Input.GetKeyUp(KeyCode.Space)) {
					this.ManuallyDecrPerilValue();

				// Lower peril value by fixed rate
				} else if(Input.GetKey(KeyCode.Space)){
					this.DecrPerilValue();
				}
			}

		/***INCREASE PERIL***/
		} else {
			// Room breaks when the player is out of the room
			if(inPeril) {
				this.IncrPerilValue();
			}
		}
	}

	void OnTriggerEnter(Collider c){
		// Check that the player entered the room
		if(c.gameObject.CompareTag(playerTag)){
			plyrInRoom = true;
		}
	}

	void OnTriggerExit(Collider c){
		// Check that the player left the room
		if(c.gameObject.CompareTag (playerTag)){
			plyrInRoom = false;

			// Make sure peril does not increase immediately
			// after player has left the room
//			this.ResetCountDownTimer();
		}
	}

	/*****Accessors for GameManager*****/
	public float GetPerilValue(){ return perilValue; }
	public float GetMaxPerilValue(){ return MAX_PERIL; }
	/**********************************/

	/// Tell manager to increase peril value
	private void IncrPerilValue(){
		perilChangeRate = roomBreakRate;
		perilChangeAmount = roomBreakAmount;
	}

	/// Tell manager to decrease peril value
	private void DecrPerilValue(){
		perilChangeRate = roomHealRate;
		perilChangeAmount = -roomHealAmount;
		if(perilValue <= MIN_PERIL){ inPeril = false; }
	}

	/// Decrease peril value without using the manager
	private void ManuallyDecrPerilValue(){
		perilValue -= roomHealAmount;
		if(perilValue > MAX_PERIL){ perilValue = MAX_PERIL; }
		if(perilValue < MIN_PERIL){ perilValue = MIN_PERIL; }
		if(perilValue <= MIN_PERIL){ inPeril = false; }
	}

	private void DontChangePerilValue(){
		perilChangeRate = 1f;
		perilChangeAmount = 0;
	}

	/// Change peril value by a specific amount at a
	/// certain frequency
	private IEnumerator ManagePerilValue(){
		while(true){
			yield return new WaitForSeconds(perilChangeRate);

			// Change the peril/danger value
			if(inPeril){
				perilValue += perilChangeAmount;
				if(perilValue > MAX_PERIL){ perilValue = MAX_PERIL; }
				if(perilValue < MIN_PERIL){ perilValue = MIN_PERIL; }

			} else{ 
				this.DontChangePerilValue();
			}
		}
	}


	private void ResetCountDownTimer(){
		countDownTimerReset = Random.Range(countDownTimerMin, countDownTimerMax);
		countDownTimer = countDownTimerReset;
	}

	/// Decide when a room is in peril
	private IEnumerator ManageCountDownTimer(){
		while(true) {
			yield return new WaitForSeconds (1f);

			// Only trigger when player is not in the room
			// and the room is not in peril
			if(!plyrInRoom && !inPeril) {

				// Count down (to 0)
				countDownTimer -= 1;

				// Initiate increase in danger/chaos/peril
				if(countDownTimer <= 0) {
					this.ResetCountDownTimer();
					inPeril = true;
				}
				Debug.Log("Peril Count Down Timer: " + countDownTimer);
			}
		}
	}

}
