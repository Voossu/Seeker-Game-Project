using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Construction turret management class
public class TurretBuildManager : MonoBehaviour {

	public TurretData laserTurretData; // Laser turret data
	public TurretData missileTurretData; // Turret turret data
	public TurretData standardTurretData; // Standard turret data
	private TurretData selectedTurretData; // The currently selected turret data will be built
	private TurretMapCube selectedMapCube; // The currently selected turret is located in the cube, and the turret will show or hide the upgrade UI
	public Text moneyText; // Show the text of money本
	public Animator moneyAnimator; // Money animated state机
	private int money = 1000; // money
	public GameObject upgradeCanvas; // Upgrade the canvas of the turret
	public Button upgradeButton; // Upgrade by press
	public Animator upgradeCanvasAnimator; // Turret upgrade canvas state machine
	
	// Money has changed
	void ChangeMoney(int change)
	{
		money += change;
		// Modify the money UI
		moneyText.text = "$ " + money;
	}
	
	void Update() 
	{
		// Press the left mouse button
		if (Input.GetMouseButtonDown(0))
		{
			// Whether the mouse clicks on the UI - if it is on the phone, you need to determine the touch
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				// Emitting rays
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				// Ray detection, parameters: ray, collision information, maximum distance, detection layer. Return whether to crash to
				bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
				if (isCollider)
				{
					// Get the click to the Cube
					TurretMapCube mapCube = hit.collider.gameObject.GetComponent<TurretMapCube>();
					// Has chosen a default turret type, and the click of the location of the turret has not yet been created
					if (selectedTurretData != null && mapCube.turretGo == null)
					{
						// If you click on the cube without a turret, you can create it
						if (money >= selectedTurretData.cost)
						{
							// Change in the number of money
							ChangeMoney(-selectedTurretData.cost);
							// Create turret
							mapCube.BuildTurret(selectedTurretData);
						}
						else 
						{
							// TODO money is not enough, give a hint
							moneyAnimator.SetTrigger("Flicker");
						}
					}
					else if (mapCube.turretGo != null)
					{
						if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
						{
							// Choose the same turret, and the turret has been shown on the upgrade UI
							StartCoroutine(HideUpgradeUI());
						}
						else
						{
							// There is already a turret, passing turret location and whether it has been upgraded
							ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
						}
						// Record the currently selected turret
						selectedMapCube = mapCube;
					}
				}
			}
		}
	}

	// Chose the laser turret
	public void OnLaserSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = laserTurretData;
		}
	}

	// Chose the turret turret
	public void OnMissileSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = missileTurretData;
		}
	}

	// Chose the standard turret
	public void OnStandardSelected(bool isOn)
	{
		if (isOn)
		{
			selectedTurretData = standardTurretData;
		}
	}

	// Show turret upgrade UI
	void ShowUpgradeUI(Vector3 position, bool isDisableUpgrade)
	{
		// Stop the last hidden animation - do not work用
		StopCoroutine(HideUpgradeUI());
		// Each activation is disabled first, so that the animation can be displayed properly
		upgradeCanvas.SetActive(false);

		// Activate turret upgrade UI
		upgradeCanvas.SetActive(true);
		// UpgradeCanvas audience only one object, each time to show him to set the location.
		upgradeCanvas.transform.position = position;
		// Whether the upgrade button is disabled or disabled if it is already upgraded or not enough money
		upgradeButton.interactable = !isDisableUpgrade;
	}

	// 隐藏炮塔升级UI
	IEnumerator HideUpgradeUI()
	{
		upgradeCanvasAnimator.SetTrigger("Hide");
		// Disable the UI after 0.5 seconds
		yield return new WaitForSeconds(0.5f);
		// Hide the turret upgrade UI
		upgradeCanvas.SetActive(false);
	}

	// Click the upgrade button
	public void OnUpgradeButtonDown()
	{
		// If you click on the cube without a turret, you can create it
		if (money >= selectedMapCube.turretData.costUpgraded)
		{
			// Change in the number of money
			ChangeMoney(-selectedTurretData.costUpgraded);
			// Upgrade the turret on the cube
			selectedMapCube.UpgradeTurret();
			// Hide UI
			StartCoroutine(HideUpgradeUI());
		}
		else 
		{
			// TODO money is not enough, give a hint
			moneyAnimator.SetTrigger("Flicker");
		}
	}
	
	// Click the Remove button
	public void OnDestroyButtonDown()
	{
		// Remove the turret on the cube
		selectedMapCube.DestroyTurret();
		// Hide UI
		StartCoroutine(HideUpgradeUI());
	}

}
