using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy incubator
public class EnemySpawner : MonoBehaviour {

	public EnemyWave[] waves; // Each element holds the desired attributes for each wave of enemies, and how many elements are generated
	public Transform START; // Generate the enemy's position
	public float waveRate = 0.2f; // the last wave of enemies dead and then rebuild the waves of the enemy's time interval
	public static int CountEnemyAlive = 0; // the number of surviving enemies
	private Coroutine coroutine; // Associations

	void Start()
	{
		coroutine = StartCoroutine(SpawnEnemy());
	}

	// generate enemies
	IEnumerator SpawnEnemy() 
	{
		foreach (EnemyWave wave in waves)
		{
			for (int i = 0; i < wave.count; i++) 
			{
				// generate enemies
				GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
				// Every time enemies make enemies survive the counter +1, and when the enemy is dead -1
				CountEnemyAlive++;
				// Every wave after the last enemy does not need to be paused
				if (i != wave.count - 1) 
				{
					// Each enemy generates an interval
					yield return new WaitForSeconds(wave.rate);
				}
			}
			while (CountEnemyAlive > 0)
			{
				// If there is no enemy dead, it has been waiting
				yield return 0;
			}
			// the last wave of enemies dead and then rebuild the waves of the enemy's time interval
			yield return new WaitForSeconds(waveRate);
		}

		while (CountEnemyAlive > 0)
		{
			yield return 0;
		}
		// no enemies survive, game victory
		TowerGameManager.instance.Win();
	}

	// stop generating enemies
	public void Stop()
	{
		StopCoroutine(coroutine);
	}

}
