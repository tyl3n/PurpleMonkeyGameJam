using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float MovementSpeed;
    Rigidbody rigidBody;
    public bool UseForceBasedMovement = true;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
    

    // Update is called once per frame
    void FixedUpdate () {
        GameManager.instance
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log(horizontal);
        Debug.Log(vertical);
        Vector3 movementVector = new Vector3(horizontal,0,vertical);
        Vector3 movementForce = movementVector.normalized * MovementSpeed;
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + movementForce);
        if (UseForceBasedMovement)
        {
            rigidBody.AddForce(movementForce);
        }
        else
        {
            rigidBody.velocity = movementForce;
        }
        //


    }
}
