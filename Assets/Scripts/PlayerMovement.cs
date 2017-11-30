using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter;   
    CameraRaycaster cameraRayCaster;
    Vector3 currentDestination;
	Vector3 clickPoint;

	[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float attackMoveStopRadius = 5f;

	private void Start()
    {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

	
	private void WalkToDestination()
	{
		var playerToClickPoint = currentDestination - transform.position;
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
		
	Vector3 ShortDestination(Vector3 destination, float shortening)
	{
		Vector3 reductionVector = (currentDestination - transform.position).normalized * shortening;
		return currentDestination - reductionVector;
	}

	void OnDrawGizmos()
	{
		//Draw movement gizmos
		Gizmos.color = Color.black;
		Gizmos.DrawLine(transform.position, currentDestination);
		Gizmos.DrawSphere(currentDestination, 0.15f);
		Gizmos.DrawSphere(clickPoint, 0.10f);

		//Draw attack sphere
		Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);


	}

}

