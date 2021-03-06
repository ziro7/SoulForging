﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileSpeed = 10f;
	private float damageCaused = 10f;

	public void SetDamage(float damage)
	{
		damageCaused = damage;
	}

	void OnTriggerEnter(Collider collider)
	{
		Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damageCaused);
		}
	}
}
