using UnityEngine;
using System.Collections;

public class PassengerInteraction : MonoBehaviour {
    bool CanInteract = false;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	    if(CanInteract && Input.GetButton("Interaction"))
        {
            if (GameManager.instance.HappinessValue< GameManager.instance.MaxHappinessValue)
                GameManager.instance.HappinessValue += 10;

			audioSource.clip = AudioManager.instance.alienVoices [Random.Range (0, AudioManager.instance.alienVoices.Length)];
			audioSource.Play ();


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
