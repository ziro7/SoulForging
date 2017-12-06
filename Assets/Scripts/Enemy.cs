using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 3f;
	[SerializeField] float chaseRadius = 5f;
	[SerializeField] float damagePerShot = 9f;
	[SerializeField] float secondsBetweenShots = 1f;
	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;
	[SerializeField] Vector3 aimOffset = new Vector3 (0, 1f, 0);

	bool isAttacking = false;
	private float currentHealthPoints;
	AICharacterControl aICharacterControl = null;
	GameObject player;

	void Start()
	{
		currentHealthPoints = maxHealthPoints;
		aICharacterControl = GetComponent<AICharacterControl>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius && !isAttacking)
		{
			isAttacking = true;
			InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots); //TODO switch to Coroutines

		}

		if (distanceToPlayer > attackRadius)
		{
			isAttacking = false;
			CancelInvoke();
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
		if (currentHealthPoints <= 0)
		{
			DestroyObject(gameObject);
		}

	}

	void SpawnProjectile()
	{
		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity) as GameObject;
		newProjectile.name = "fireball";

		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
		projectileComponent.SetDamage(damagePerShot);

		Vector3 playerCenterPosition = player.transform.position + aimOffset;

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
		Gizmos.color = new Color(255f, 255f, 255f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, chaseRadius);
	}

}
