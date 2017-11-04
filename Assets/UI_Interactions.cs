using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interactions : MonoBehaviour {

	public float sensitivity = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void zoomIn(){
		Vector3 forward = this.transform.parent.transform.forward + new Vector3(0, 0, 2);
		this.transform.position += forward;
	}

	public void zoomOut(){
		Vector3 forward = this.transform.parent.transform.forward + new Vector3(0, 0, -2);
		this.transform.position += forward;
	}

	public void NavigateX(float x){
		Vector3 forward = this.transform.forward + new Vector3(x,0,0);
		this.transform.position += forward;
	}
	public void NavigateY(float y){
		Vector3 forward = this.transform.forward + new Vector3(0,y,0);
		this.transform.position += forward;
	}
}
