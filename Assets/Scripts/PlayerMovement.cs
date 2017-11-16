using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter;   
    CameraRaycaster cameraRayCaster;
    Vector3 currentClickTarget;
	[SerializeField] float walkMoveStopRadius = 0.2f;

	private void Start()
    {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {

			print("Cursor raycast hit " + cameraRayCaster.currentLayerHit);
			switch(cameraRayCaster.currentLayerHit)
			{
				case Layer.Walkable:
					currentClickTarget = cameraRayCaster.hit.point;
					break;

				case Layer.Enemy:
					print("Not moving to enemy");
					break;

				default:
					print("Shouldn't be here");
					return;
			}
			//print(currentClickTarget);				
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius)
		{
			thirdPersonCharacter.Move(playerToClickPoint, false, false);
			//print(playerToClickPoint);
		}
		else
		{
			thirdPersonCharacter.Move(Vector3.zero, false, false);
		}
		

	}
}

