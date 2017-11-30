﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 4f;

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
		if (distanceToPlayer<= attackRadius)
		{
			aICharacterControl.SetTarget(player.transform);
		} else
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

}
