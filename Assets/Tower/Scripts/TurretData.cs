using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fort data category
[System.Serializable] // Serializable
public class TurretData {
	public GameObject turretPrefab; // Basic version of the preform
	public int cost; // Basic Edition price
	public GameObject turretUpgradedPrefab; // Reinforced preforms
	public int costUpgraded; // Upgrade the price
}

// Turret type enumeration
public enum TurretType {
	LaserTurret, // Laser turret
	MissileTurret, // Turret turret
	StandardTurret // Turret turret
}
