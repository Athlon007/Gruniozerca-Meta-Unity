using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HUD {Health, Score}

public class UIManager : MonoBehaviour {

	[Header("Score")]
	public GameObject score;
	[Header("Grunio Art")]
	public GameObject grunioArt;

	public static UIManager instance;

 	void Awake() {
		instance = this;
	}

	void Start () {
		grunioArt.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
		StartCoroutine(GrunioArtAnim());
	}

	// Shows and then hides the Grunio artwork
	IEnumerator GrunioArtAnim () {
		SpriteRenderer grunioRenderer = grunioArt.GetComponent<SpriteRenderer>();

		while (true) {
			yield return new WaitForSeconds(10);

			do {
				grunioRenderer.color = new Color(grunioRenderer.color.r, grunioRenderer.color.g, grunioRenderer.color.b, grunioRenderer.color.a + 0.25f);
				yield return new WaitForSeconds(0.25f);
			}
			while (grunioRenderer.color.a < 1);

			yield return new WaitForSeconds (10);

			do {
				grunioRenderer.color = new Color(grunioRenderer.color.r, grunioRenderer.color.g, grunioRenderer.color.b, grunioRenderer.color.a - 0.25f);
				yield return new WaitForSeconds(0.25f);
			}
			while (grunioRenderer.color.a > 0);
		}
	}

	public void UpdateHUD (HUD hud) {
		switch (hud) {
			case HUD.Health:
				Destroy(GameObject.Find("life" + PlayerStatistics.instance.Health));
			break;
			case HUD.Score:
				score.GetComponent<Text>().text = PlayerStatistics.instance.Score.ToString();
			break;
		}
	}
}
