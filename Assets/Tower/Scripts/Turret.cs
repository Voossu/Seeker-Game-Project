using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turret script, control turret attack
public class Turret : MonoBehaviour {

	// the enemy in the attack range
	private List<GameObject> enemies = new List<GameObject>();
	public float attackRateTime = 1.0f; // attack interval
	private float timer = 0; // attack interval
	public GameObject bulletPrefab; // bullet preform
	public Transform firePosition; // turret launch port position
	public Transform head; // turret head
	public bool isUseLaser = false; // whether to use a laser turret
	public float damageRate = 70; // Laser turret attack damage 1 sec 70 damager
	public LineRenderer laserRenderer; // laser rendere
	public GameObject laserEffect; // laser attack effects
	
	void Start()
	{
		// Turret just instantiation will be able to start attack, so timer> = attackRateTime set up
		timer = attackRateTime;
	}

	void Update()
	{
		// The turret is aimed at the enemy
		if (enemies.Count > 0 && enemies[0] != null)
		{
			Vector3 targetPosition = enemies[0].transform.position;
			// Let the turret height remain the same
			targetPosition.y = head.position.y;
			head.LookAt(targetPosition);
		}

		// Whether it is a laser turret
		if (isUseLaser)
		{
			if (enemies.Count > 0)
			{
				// If the target has been killed or has reached the end, the collection is removed
				if (enemies[0] == null)
				{
					UpdateEnemys();
				}
				// Clean up the empty element, once again to determine whether there are enemies can be attacked
				if (enemies.Count > 0)
				{
					if (laserRenderer.enabled == false)
					{
						laserRenderer.enabled = true;
						laserEffect.SetActive(true);
					}
					// Laser attack target
					laserRenderer.SetPositions(new Vector3[]{firePosition.position, enemies[0].transform.position});
					laserEffect.transform.position = enemies[0].transform.position;
					laserEffect.transform.LookAt(new Vector3(transform.position.x, enemies[0].transform.position.y, transform.position.z));
					// Causing sustained damage
					enemies[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
				}
			}
			else
			{
				// Can attack state
				laserRenderer.enabled = false;
				laserEffect.SetActive(false);
			}
			
		}
		else 
		{
			// Ordinary turret, timer increments
			timer += Time.deltaTime;
			// There is a place, and the timer is reset over the attack interval, and the attack method is called
			if (enemies.Count > 0 && timer >= attackRateTime)
			{
				// The timer is cleared
				timer = 0;
				Attack();
			}
		}

	}

	// Into the attack range
	void OnTriggerEnter(Collider other)
	{
		// The enemy enters the attack range and joins the collection
		if (other.tag == "Enemy")
		{
			enemies.Add(other.gameObject);
		}
	}

	// Leave the range of attacks - if the turret range envelops the end, will not remove the enemy
	void OnTriggerExit(Collider other)
	{
		// The enemy leaves the attack range and removes the collection
		if (other.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
		}
	}

	// Attack the enemy
	void Attack()
	{
		// If the target has been killed or has reached the end, the collection is removed
		if (enemies[0] == null)
		{
			UpdateEnemys();
		}
		// Clean up the empty element, once again to determine whether there are enemies can be attacked
		if (enemies.Count > 0)
		{
			// The instantaneous bullets, bullet positions and directions coincide at the gun muzzle
			GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
			// Set the target to the bullet
			bullet.GetComponent<TurretBullet>().SetTarget(enemies[0].transform);
		}
		else
		{
			// Can attack state
			timer = attackRateTime;
		}
		
	}

	// Update the enemy collection - remove enemies that have been killed or reached the finish line
	void UpdateEnemys()
	{
		// Store all empty elements
		List<int> emptyIndexList = new List<int>();
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemies[i] == null)
			{
				emptyIndexList.Add(i);
			}
		}
		// Remove empty elements
		for (int i = 0; i < emptyIndexList.Count; i++)
		{
			enemies.RemoveAt(emptyIndexList[i] - i);
		}
	}

}
