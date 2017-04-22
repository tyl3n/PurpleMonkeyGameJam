using UnityEngine;
using System.Collections;

public class HideWalls : MonoBehaviour {
    Ray ray;
    public GameObject character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    { 
        RaycastHit hit;

        if (Physics.Raycast(transform.position, character.transform.position, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            Renderer renderer = hitObj.GetComponent<Renderer>();
            Color transparentColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b,0.5f);
            renderer.material.color = transparentColor;
        }
            


    }
}
