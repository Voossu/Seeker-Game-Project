using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Save each wave of enemies to generate the desired attributes
[System.Serializable] // can be serialized, that is, it can be displayed on the Inspector panel
public class EnemyWave {
	public GameObject enemyPrefab; // enemy preforms
	public int count; // the number of enemies
	public float rate; // Every enemy generated interval
}
