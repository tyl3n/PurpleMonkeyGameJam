using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    float HappinessValue;
    public float MaxHappinessValue;
    public float RateOfDescent = 10;
    public float KitchenMultiplier = 2.0f;
    public float EngineMultiplier = 1.5f;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        HappinessValue = MaxHappinessValue;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float multiplier =  1;
        HappinessValue -= RateOfDescent* multiplier;
    }
    
}
