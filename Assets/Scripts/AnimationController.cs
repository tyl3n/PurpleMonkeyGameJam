using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
    public Animator anim;
    public SpriteRenderer spriteRender;
	// Use this for initialization
	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        bool up = Input.GetAxis("Vertical") > 0.001 && (Input.GetAxis("Horizontal") < 0.2|| Input.GetAxis("Horizontal") > -0.2);
        anim.SetBool("Up", up);
        bool down = Input.GetAxis("Vertical") < -0.001 && (Input.GetAxis("Horizontal") < 0.2 || Input.GetAxis("Horizontal") > -0.2);
        anim.SetBool("Down", down);
        if (!up && !down)
        {
            bool horizontal = Input.GetAxis("Horizontal") > 0.01 || Input.GetAxis("Horizontal") < -0.01;
            anim.SetBool("Horizontal", horizontal);
        }
        else
        {
            anim.SetBool("Horizontal", false);
        }
        anim.SetBool("Repair", Input.GetButton("Interaction"));
        if (Input.GetAxis("Horizontal")<0)
        {
            spriteRender.flipX = true;
        }
        else
        {
            spriteRender.flipX = false;
        }
	}
}
