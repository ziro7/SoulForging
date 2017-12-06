using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] int enemyLayer = 9;
	[SerializeField] float damagePerHit = 12;
	[SerializeField] float minTimeBetweenHits = 1f;
	[SerializeField] float maxAttackDistance = 2f;
	private float currentHealthPoints;
	GameObject currentTarget = null;
	CameraRaycaster cameraRayCaster;
	float lastHitTime = 0f;

	public float healthAsPercentage {get{return (currentHealthPoints/maxHealthPoints);}	}

	
	void Start()
	{
		currentHealthPoints = maxHealthPoints;
		cameraRayCaster = FindObjectOfType<CameraRaycaster>();
		cameraRayCaster.notifyMouseClickObservers += OnMouseClick;   
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Debug.Log("Cast fireballl");
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
		if (currentHealthPoints <= 0)
		{
			DestroyObject(gameObject);
		}
	}


	void OnMouseClick(RaycastHit raycastHit, int layerHit)
	{
		if (layerHit == enemyLayer)
		{
			GameObject enemy = raycastHit.collider.gameObject;
			currentTarget = enemy;

			//Check eemy is in range
			if ((enemy.transform.position - transform.position).magnitude > maxAttackDistance)
			{
				return;
			}

			var enemyComponent = enemy.GetComponent<Enemy>();

			if (Time.time - lastHitTime > minTimeBetweenHits)
			{
				enemyComponent.TakeDamage(damagePerHit);
				lastHitTime = Time.time;
			}
		}
	}
}
