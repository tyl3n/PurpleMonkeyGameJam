using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    bool isGameOver = false;
    public bool isGameWon = false; // public for testing

	public float maxTime = 60f; // in seconds

    public bool isDebugging = true;
    public float HappinessValue = 0;
    public float MaxHappinessValue = 100.0f;
    public float RateOfDescent = 0.1f;
    public float KitchenMultiplier = 5f;
    public float EngineMultiplier = 2f;

	public GameObject player;
	public GameObject gameWonCard;
	public GameObject gameOverCard;
	public Text winTimerText;

    float multiplier = 1;

	private float winTimer;


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
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        HappinessValue = MaxHappinessValue;
		winTimer = maxTime;
		StartCoroutine(ManageWinConditionTimer());
        StartCoroutine(OnGameOver());
        winTimerText.enabled = true;

		// Make sure game over/won cards are disabled
		gameOverCard.SetActive(false);
		gameWonCard.SetActive(false);
	}

	public bool IsGameWon(){ return isGameWon; }
	public bool IsGameOver(){ return isGameOver; }

    void ProcessPassengerHappiness()
    {
        EventRoom[] allRooms = FindObjectsOfType(typeof(EventRoom)) as EventRoom[];
        multiplier = 1;
        foreach (EventRoom eventRoom in allRooms)
        {
            multiplier +=  eventRoom.GetPerilValue()*3f;
        }
        if (instance.HappinessValue >= 0)
            instance.HappinessValue -= instance.RateOfDescent * multiplier * Time.deltaTime;
        else
        {
            isGameOver = true;
        }
    }

    void ProcessDebugEvent()
    {
        if (Input.GetButton("KitchenDebug"))
        {
            multiplier += KitchenMultiplier;
        }
        if (Input.GetButton("EngineDebug"))
        {
            multiplier += EngineMultiplier;
        }
        if (Input.GetButton("PassengerDebug"))
        {
            instance.HappinessValue += 1;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {

		if(!isGameWon && !isGameOver){
			ProcessPassengerHappiness();
		}
		UpdateTimerText();

        if (isDebugging)
            ProcessDebugEvent();
       
    }

	void UpdateTimerText(){
		if (!isGameOver) {
			string tempText;

			if (isGameWon) {
				tempText = "" + maxTime;
			}
			tempText = "" + (maxTime - winTimer);
			if (tempText.IndexOf (".") >= 0) {
				tempText = tempText.Substring (0, (tempText.IndexOf (".") + 2));
			}
			winTimerText.text = tempText + "/" + maxTime + "s";
		}
	}
    IEnumerator OnGameOver()
    {
        if (isGameOver)
        {
            yield return new WaitForSeconds(2);
            ReloadGame();
        }
        
    }
    void ReloadGame()
    {
        StopAllCoroutines();
        HappinessValue = MaxHappinessValue;
        SceneManager.LoadScene(0);
    }
    void OnGUI()
    {    
        if (isGameWon)
        {
			gameWonCard.SetActive(true);
			TogglePlryMovement(false);
            //StopAllCoroutines();
            
//            GUI.Label(new Rect(Screen.height - 10, Screen.width - 40, 20, 80), "You WIN!");
//            if (GUI.Button(new Rect(Screen.height + 40, Screen.width - 80, 20, 20),"Play Again?"))
//            {
//                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//            }                
        }

		if (isGameOver)
		{
			gameOverCard.SetActive(true);
			TogglePlryMovement(false);
			//StopAllCoroutines();
            StartCoroutine(OnGameOver());//(ManageWinConditionTimer());
                                         //            GUI.Label(new Rect(Screen.height - 10, Screen.width - 40, 20, 80), "You Lose!");
                                         //            if (GUI.Button(new Rect(Screen.height + 40, Screen.width - 80, 20, 20), "Play Again?"))
                                         //                Application.LoadLevel(1);
        }
    }

	void TogglePlryMovement(bool setTo){
		Movement[] movements = player.GetComponents<Movement>();
		foreach(Movement m in movements){
			m.enabled = setTo;
		}
	}

	IEnumerator ManageWinConditionTimer(){
		while (true) {
			yield return new WaitForSeconds (0.1f);

			winTimer -= 0.1f;
			if (winTimer <= 0 && HappinessValue > 0) {
				winTimer = 0;
				isGameWon = true;
				StopAllCoroutines ();//(ManageWinConditionTimer());
			}
		}
	}

}
