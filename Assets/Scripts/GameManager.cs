using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    bool isGameOver = false;
    bool isGameWon = false;
    public bool isDebugging = true;
    public float HappinessValue = 0;
    public float MaxHappinessValue = 100.0f;
    public float RateOfDescent = 0.1f;
    public float KitchenMultiplier = 5f;
    public float EngineMultiplier = 2f;
    float multiplier = 1;
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
    void ProcessPassengerHappyness()
    {
        if (instance.HappinessValue > 0)
            instance.HappinessValue -= instance.RateOfDescent * multiplier * Time.deltaTime;
        else
        {
            isGameOver = true;
            isGameOver = false;
        }
    }
    void ProcessDebugEvent()
    {
        if (Input.GetButton("KitchenDebug"))
        {
            multiplier = KitchenMultiplier;
        }
        if (Input.GetButton("EngineDebug"))
        {
            multiplier = EngineMultiplier;
        }
        if (Input.GetButton("PassengerDebug"))
        {
            instance.HappinessValue += 1;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {


        ProcessPassengerHappyness();
        if (isDebugging)
            ProcessDebugEvent();
       
    }
    void OnGUI()
    {

        if (isGameOver)
        {
            if (isGameWon)
            {
                GUI.Label(new Rect(Screen.height - 10, Screen.width - 40, 20, 80), "You WIN!");
                if (GUI.Button(new Rect(Screen.height + 40, Screen.width - 80, 20, 20),"Play Again?"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                    
            }
            else
            {
                GUI.Label(new Rect(Screen.height - 10, Screen.width - 40, 20, 80), "You Lose!");
                if (GUI.Button(new Rect(Screen.height + 40, Screen.width - 80, 20, 20), "Play Again?"))
                    Application.LoadLevel(1);
            }

        }
    }

}
