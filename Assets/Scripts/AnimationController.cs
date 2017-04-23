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
        bool horizontal = Input.GetAxis("Horizontal")>0.01 || Input.GetAxis("Horizontal")<-0.01;
        anim.SetBool("Horizontal",horizontal);
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
