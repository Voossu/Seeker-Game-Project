using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// map cube, can be placed turret
public class TurretMapCube : MonoBehaviour {

	[HideInInspector] // [HideInInspector] can hide the public property displayed in the inspector panel
	public GameObject turretGo; // The turret under the current cube, if empty, indicates that there is no turret at the current location
	[HideInInspector] // [HideInInspector] can hide the public property displayed in the inspector panel
	public bool isUpgraded = false; // Whether the turret has been upgraded
	public GameObject buildEffect; // Build the turret's special effects preform
	private Renderer cubeRenderer; // Renderer
	public TurretData turretData; // the turret data under the current cube

	void Start()
	{
		cubeRenderer = GetComponent<Renderer>();
	}
	
	// Build turret
	public void BuildTurret(TurretData turretData) 
	{
		// let the current cube hold the turret data to facilitate the upgrade on the turret on the cube
		this.turretData = turretData;
		// Each build turret resets the upgrade logo
		isUpgraded = false;
		// instantiate the turret
		turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
		// Construction of turret dust effects
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}
	
	void OnMouseEnter()
	{
		// There is no turret in the current position, the mouse is not on the UI. Change the renderer color
		if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
		{
			cubeRenderer.material.color = Color.red;
		}
	}

	void OnMouseExit()
	{
		// Restore the color after the mouse is removed
		cubeRenderer.material.color = Color.white;
	}

	// Upgrade the turret under the current cube
	public void UpgradeTurret()
	{
		// Has been upgraded
		if (isUpgraded)
		{
			return;
		}

		Destroy(turretGo);
		// Modify the logo after upgrading the turret
		isUpgraded = true;
		// Instantiate the enhanced version of the turret
		turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
		// Upgrade the dust of the turret
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}

	// Remove the turret
	public void DestroyTurret()
	{
		Destroy(turretGo);
		isUpgraded = false;
		turretGo = null;
		turretData = null;
	}

}
