using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour {

	public float speed; // Starting speed
	public float sprintSpeed; // Sprinting speed
	
	private int health, score;

	public bool isDida;

	public static PlayerStatistics instance;

	void Awake() {
		instance = this;
		health = 3;
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UIManager.instance.UpdateHUD(HUD.Score);
		}
	}

	public int Health {
		get {
			return health;
		}
		set {
			health = value;

			if (PlayerStatistics.instance.health == 0) {
				Globals.instance.GameOver();
				return;
			}

			UIManager.instance.UpdateHUD(HUD.Health);
		}
	}
}
