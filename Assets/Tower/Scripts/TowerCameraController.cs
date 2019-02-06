using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制摄像机
public class TowerCameraController : MonoBehaviour {
	
	public float translateSpeed = 25; // angle of view movement speed
	public float scaleSpeed = 500; // angle zoom speed

	void Update () 
	{
		// Direction buttons to control the viewing angle before and after moving
		float h = Input.GetAxis("Horizontal") * translateSpeed;
		float v = Input.GetAxis("Vertical") * translateSpeed;

		// mouse pulley controls the perspective of the distance
		float mouse = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;

		// perspective in accordance with the world coordinate system, so that it is not affected by its own rotation
		transform.Translate(new Vector3(h, mouse, v) * Time.deltaTime, Space.World);
	}
}
