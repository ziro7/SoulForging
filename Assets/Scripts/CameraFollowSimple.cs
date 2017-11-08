using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSimple : MonoBehaviour {

	public Transform cameraMain, player, cameraArm;
	private float mouseX = 0;
	private float mouseY = 0;
	private float mouseSensitivity = 10f;
	private float mouseYPosition = 1f;
	[SerializeField] private float zoom = -6;
	[SerializeField] private float zoomSpeed = 2;
	[SerializeField] private float zoomMin = -2f;
	[SerializeField] private float zoomMax = -12f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		if (zoom > zoomMin)
			zoom = zoomMin;

		if (zoom < zoomMax)
			zoom = zoomMax;

		cameraMain.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zoom);

		if (Input.GetMouseButton(1))
		{
			mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
			mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		}

		mouseY = Mathf.Clamp(mouseY, -40f, 60f);
		cameraMain.LookAt(cameraArm);
		cameraArm.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
		cameraArm.position = new Vector3(player.position.x, player.position.y + mouseYPosition, player.position.z);

	}
}
