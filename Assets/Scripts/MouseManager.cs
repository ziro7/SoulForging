using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject hoveredObject = null;
	public GameObject selectedObject = null;

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.gameObject;
			HoveredObject(hitObject);
		} else
		{ 
			ClearSelection();
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (hoveredObject != null)
			{
				selectedObject = hoveredObject;
			}
			else
			{
				return;
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			ClearSelection();
		}
				
	}

	void HoveredObject(GameObject obj)
	{
		if (hoveredObject != null)
		{
			if (obj == hoveredObject)
			{
				return;
			}
			ClearHovered();
		}
		hoveredObject = obj;
		
	}

	void ClearSelection()
	{
		selectedObject = null;
	}

	void ClearHovered()
	{
		hoveredObject = null;
	}
}

