using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet AI - instantiate out to attack the enemy
public class TurretBullet : MonoBehaviour {

	public int damage = 50; // Bullet damage
	public float speed = 40; // Bullet firing speed
	public GameObject explosionEffectPrefab; // The bullet hit the enemy's explosive effect of the preform
	private Transform target; // Attack target
	
	// Instructing bullets requires a given target
	public void SetTarget(Transform target)
	{
		this.target = target;
	}

	void Update() 
	{
		// When the target reaches the finish line, or the target is killed. The bullet is destroyed
		if (target == null)
		{
			// Destroy the bullet
			Die();
			return;
		}
		// The bullet points to the target
		transform.LookAt(target.position);
		// Launch to attack target
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

	// Bullet collision detection
	void OnTriggerEnter(Collider other)
	{
		// If the attack is the enemy
		if (other.tag == "Enemy")
		{
			// Let the enemy Diaoxie
			other.GetComponent<Enemy>().TakeDamage(damage);
			// Destroy the bullet
			Die();
		}
	}

	void Die()
	{
		// Explosive effect
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
		// Destroy effects
		Destroy(effect, 1);
		// Destroy the bullet
		Destroy(gameObject);
	}

}
