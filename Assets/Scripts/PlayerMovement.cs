using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    //ThirdPersonCharacter thirdPersonCharacter = null;   
    CameraRaycaster cameraRayCaster = null;
    //Vector3 currentDestination;
	AICharacterControl aiCharacterControl = null;
	GameObject walkTarget = null;

	[SerializeField] const int walkableLayerNumber = 8;
	[SerializeField] const int enemyLayerNumber = 9;
	//[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float attackMoveStopRadius = 5f;

	private void Start()
    {
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		//thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		aiCharacterControl= GetComponent<AICharacterControl>();
		//currentDestination = transform.position;
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

	void OnDrawGizmos()
	{
		//Draw attack sphere
		Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);


	}

}

