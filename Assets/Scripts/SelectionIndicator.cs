using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {

	MouseManager mouseManager;

	void Start()
	{

		mouseManager = GameObject.FindObjectOfType<MouseManager>();
	}

	void Update()
	{

		if (mouseManager.selectedObject != null)
		{
			//TODO problem med terrain...hvordan løser jeg den?
			//tager kun første renderers størrelse - så kan være et problem med flerer renderer.
			Bounds bigBounds = mouseManager.selectedObject.GetComponentInChildren<Renderer>().bounds;

			float diameter = bigBounds.size.z;
			diameter *= 1.25f;

			this.transform.position = new Vector3(bigBounds.center.x, 0, bigBounds.center.z);
			this.transform.localScale = new Vector3(bigBounds.size.x, bigBounds.size.y, bigBounds.size.z);
		}
	}
}

