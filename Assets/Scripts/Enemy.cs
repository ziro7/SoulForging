using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 3f;
	[SerializeField] float chaseRadius = 5f;
	[SerializeField] float damagePerShot = 9f;
	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;

	private float currentHealthPoints = 100f;
	AICharacterControl aICharacterControl = null;
	GameObject player;

	void Start()
	{
		aICharacterControl = GetComponent<AICharacterControl>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius)
		{
			SpawnProjectile();
		}

		if (distanceToPlayer <= chaseRadius)
		{
			aICharacterControl.SetTarget(player.transform);
		}
		else
		{
			aICharacterControl.SetTarget(transform);
		}
	}
		

	public float healthAsPercentage
		{
			get
			{
			return (currentHealthPoints/maxHealthPoints);
			}	
		}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

	}

	void SpawnProjectile()
	{
		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity) as GameObject;
		newProjectile.name = "fireball";

		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
		projectileComponent.damageCaused = damagePerShot;

		Vector3 offsetPlayerCenter = new Vector3(0, 1, 0);
		Vector3 playerCenterPosition = player.transform.position + offsetPlayerCenter;

		Vector3 unitVectorToPlayer = (playerCenterPosition - projectileSocket.transform.position).normalized;
		float projectileSpeed = projectileComponent.projectileSpeed;
		newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
	}

	void OnDrawGizmos()
	{
		//Draw attack sphere
		Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, attackRadius);

		//Draw move sphere
		Gizmos.color = new Color(255f, 255f,255f,0.5f);
		Gizmos.DrawWireSphere(transform.position, chaseRadius);
	}

}
