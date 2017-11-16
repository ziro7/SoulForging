﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D attackCursor = null;
	[SerializeField] Texture2D unknownCursor = null;
	[SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);
	CameraRaycaster cameraRayCaster;
		
	// Use this for initialization
	void Start () {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		cameraRayCaster.layerChangeObservers += layerWasChanged; //register for changes on layer changes from raycasts.
	}

	void Update()
	{

	}
	
	void layerWasChanged(Layer newLayer)
	{
		switch (newLayer)
		{
			case Layer.Walkable:
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy:
				Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.RaycastEndStop:
				Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;

		}
	}
}