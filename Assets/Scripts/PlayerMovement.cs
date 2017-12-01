using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter = null;   
    CameraRaycaster cameraRayCaster = null;
    Vector3 currentDestination;
	//Vector3 clickPoint;
	AICharacterControl aiCharacterControl = null;
	GameObject walkTarget = null;

	[SerializeField] const int walkableLayerNumber = 8;
	[SerializeField] const int enemyLayerNumber = 9;
	[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float attackMoveStopRadius = 5f;

	private void Start()
    {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		aiCharacterControl= GetComponent<AICharacterControl>();
		currentDestination = transform.position;
		walkTarget = new GameObject("WalkTarget");

		cameraRayCaster.notifyMouseClickObservers += ProcessMouseClick;
    }


	void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
	{
		switch (layerHit)
		{
			case enemyLayerNumber:
				GameObject enemy = raycastHit.collider.gameObject;
				aiCharacterControl.SetTarget(enemy.transform);
				break;
			case walkableLayerNumber:
				walkTarget.transform.position = raycastHit.point;
				aiCharacterControl.SetTarget(walkTarget.transform);
				break;
			default:
				Debug.LogWarning("Don't know how to handle movement");
				return;
		}

	}


	////private void WalkToDestination()
	////{
	////	var playerToClickPoint = currentDestination - transform.position;
	////	if (playerToClickPoint.magnitude >= walkMoveStopRadius)
	////	{
	////		thirdPersonCharacter.Move(playerToClickPoint, false, false);
	////		print(playerToClickPoint);
	////	}
	////	else
	////	{
	////		thirdPersonCharacter.Move(Vector3.zero, false, false);
	////	}
	////}

	////Vector3 ShortDestination(Vector3 destination, float shortening)
	////{
	////	Vector3 reductionVector = (currentDestination - transform.position).normalized * shortening;
	////	return currentDestination - reductionVector;
	////}

	void OnDrawGizmos()
	{
		//Draw movement gizmos
		////Gizmos.color = Color.black;
		////Gizmos.DrawLine(currentDestination, transform.position);
		////Gizmos.DrawSphere(currentDestination, 0.15f);
		//Gizmos.DrawSphere(clickPoint, 0.10f);

		//Draw attack sphere
		Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);


	}

}

