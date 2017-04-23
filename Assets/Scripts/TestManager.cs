using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestManager : MonoBehaviour {

	private EventRoom[] rooms;

	// Use this for initialization
	void Start () {
		rooms = FindObjectsOfType(typeof(EventRoom)) as EventRoom[];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(""+this.PerilScoreCard());
	}

	private string PerilScoreCard(){
		string scoreCard = "";
		for(int i = 0; i < rooms.Length; i++){
			EventRoom er = rooms[i];
			scoreCard += er.gameObject.name+": " + er.GetPerilValue() + "; ";
		}

		return scoreCard;
	}
}
