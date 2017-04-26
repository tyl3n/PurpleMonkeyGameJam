using UnityEngine;

using System.Collections;

public class HappinessBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       float scale = (GameManager.instance.HappinessValue / GameManager.instance.MaxHappinessValue<0)?0:GameManager.instance.HappinessValue / GameManager.instance.MaxHappinessValue;
        if (scale >1)
        {
            scale = 1;
        }
        GetComponent<RectTransform>().localScale = new Vector3( -scale,1);
        //GetComponent<Slider>();

    }
}
