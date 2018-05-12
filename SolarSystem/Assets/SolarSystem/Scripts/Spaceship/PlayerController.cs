using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int secondsPerBurning = 1;
	public float fuelPerBurn = 0.01f;
	public float turnspeed = 5.0f;
	public float speed = 5.0f;
	public float strafeSpeed = 5.0f;

	private bool isAccelerating = false;
	private float nextTime;
	private float trueSpeed = 0.0f;
	private Rigidbody rb;
	//private GUIController guiController;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		//guiController = GameObject.FindWithTag ("GameController").GetComponent<GUIController>();
	}

	void FixedUpdate () {

		float roll = Input.GetAxis("Roll");
		float pitch = Input.GetAxis("Pitch");
		float yaw = Input.GetAxis("Yaw");
		Vector3 strafe = new Vector3(Input.GetAxis("Horizontal")*strafeSpeed*Time.deltaTime, Input.GetAxis("Vertical")*strafeSpeed*Time.deltaTime, 0);

		float power = Input.GetAxis("Power");

		//Truespeed controls
		if (trueSpeed < 10 && trueSpeed > -3){
			trueSpeed += power;
		}
		if (trueSpeed > 10){
			trueSpeed = 9.99f;

		}
		if (trueSpeed < -3){
			trueSpeed = -2.99f;
		}
		if (Input.GetKey("backspace")){
			isAccelerating = false;
			trueSpeed = 0f;
		}

		//Input Events
		if(Input.GetButton("Roll")){
			Debug.Log ("Rotacionando no Z");
			burnFuel ();		
		}
		if(Input.GetButton("Yaw")){
			Debug.Log ("Rotacionando no Y");
			burnFuel ();		
		}
		if(Input.GetButton("Pitch")){
			Debug.Log ("Rotacionando no X");
			burnFuel ();		
		}
		if(Input.GetButton("Horizontal")){
			Debug.Log ("Transladando no X");
			burnFuel ();		
		}
		if(Input.GetButton("Vertical")){
			Debug.Log ("Transladando no Y");
			burnFuel ();		
		}
		if(Input.GetButton("Power")){
			isAccelerating = true;
			Debug.Log ("Força acionada");
		}
		if (Input.GetKeyDown("backspace")){
			Debug.Log ("Foguetes desligados");
		}
			
		if (isAccelerating) {
			burnFuel ();
		}

		/*if (power > 0) {
			burnFuel ();
			Debug.Log ("Acelerando para frente");
		} else if (power < 0) {
			burnFuel ();
			Debug.Log ("Acelerando para trás");
		}*/

		rb.AddRelativeTorque(pitch*turnspeed*Time.deltaTime, yaw*turnspeed*Time.deltaTime, roll*turnspeed*Time.deltaTime);
		rb.AddRelativeForce(0,0,trueSpeed*speed*Time.deltaTime);
		rb.AddRelativeForce(strafe);
	}
	private void burnFuel(){
		/*if (guiController != null) {
			if (Time.time >= nextTime) {
				guiController.decreaseFuel (fuelPerBurn);

				nextTime = Mathf.FloorToInt (Time.time) + secondsPerBurning;
			}
		} else {
			Debug.Log ("Add the gameController");
		}*/
	}
}