using UnityEngine;
using System.Collections;

public class PassengerInteraction : MonoBehaviour {
    bool CanInteract = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	    if(CanInteract && Input.GetButton("Interaction"))
        {
            GameManager.instance.HappinessValue += 2;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
        }
    }
}
