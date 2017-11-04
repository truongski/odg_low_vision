using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interactions : MonoBehaviour {

	public Transform viewCameraTransform;
	public float sensitivity = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void zoomIn(){
		Vector3 forward = viewCameraTransform.transform.forward * 1;
		this.transform.position += forward;
	}

	public void zoomOut(){
		Vector3 forward = viewCameraTransform.transform.forward * -1;
		this.transform.position += forward;
	}

	public void NavigateX(float x){
		Vector3 right = viewCameraTransform.right * x;
		this.transform.position += right;
	}
	public void NavigateY(float y){
		Vector3 up = viewCameraTransform.up * y;
		this.transform.position += up;
	}
}
