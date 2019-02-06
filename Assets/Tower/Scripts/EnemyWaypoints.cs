using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy path point
public class EnemyWaypoints : MonoBehaviour {

	public static Transform[] positions; // all path points
	void Awake()
	{
		positions = new Transform[transform.childCount];
		for (int i = 0; i < positions.Length; i++) 
		{
			positions[i] = transform.GetChild(i);
		}
	}
}
