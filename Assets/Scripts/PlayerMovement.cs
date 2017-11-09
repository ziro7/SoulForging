using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRayCaster;
    Vector3 currentClickTarget;
	[SerializeField] float walkMoveStopRadius = 0.2f;

	private void Start()
    {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {

			print("Cursor raycast hit " + cameraRayCaster.layerHit);
			switch(cameraRayCaster.layerHit)
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
			m_Character.Move(playerToClickPoint, false, false);
			//print(playerToClickPoint);
		}
		else
		{
			m_Character.Move(Vector3.zero, false, false);
		}
		

	}
}

