using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 一个Enemy就是一个敌人
public class Enemy : MonoBehaviour {

	private float totalHp; // Save the total amount of blood to calculate the blood stripe progress. Because hp will be diminished
	public float hp = 150; // enemy blood
	public Slider hpSlider; // blood strips
	public float speed = 10; // Moving speed
	public GameObject explosionEffectPrefab; // enemy death explosion effects
	private Transform[] positions; // all path points
	private int index = 0; // Which point is moving now?

	void Start() 
	{
		// Save the blood
		totalHp = hp;
		// Get an array of path points
		positions = EnemyWaypoints.positions;
	}
	
	void Update() 
	{
		Move();
	}

	// Monster mobile processing
	void Move() 
	{
		// To prevent cross-border
		if (index > positions.Length - 1) return;
		// Target point - own point = direction of its own point to the target point
		transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
		// If the position of the target point and its own point is less than 0.2 m, it starts to move like the next target point
		if (Vector3.Distance(positions[index].position, transform.position) < 0.2) 
		{
			index++;
		}
		// The last execution, index will be greater than the array maximum angle
		if (index > positions.Length - 1) 
		{
			// The current enemy has arrived at the destination
			ReachDestination();
		}
	}

	// The enemy arrives at the destination
	void ReachDestination() 
	{
		// After the enemy arrives at the destination, destroy the current game object
		GameObject.Destroy(gameObject);
		// The game failed
		TowerGameManager.instance.Failed();
	}

	// The enemy is destroyed
	void OnDestroy()
	{
		// When the enemy is destroyed, it will survive the counter -1
		EnemySpawner.CountEnemyAlive--;
	}

	// Hurt
	public void TakeDamage(float damage)
	{
		if (hp <= 0)
		{
			return;
		}
		// Reduce blood volume and update UI
		hp -= damage;
		hpSlider.value = hp / totalHp;
		if (hp <= 0)
		{
			Die();
		}
	}

	// The enemy is dead
	void Die()
	{
		// enemy death explosion effects
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
		// delay 1.5 seconds to destroy the explosion effects
		Destroy(effect, 1.5f);
		// destroy the enemy
		Destroy(gameObject);
	}

}
