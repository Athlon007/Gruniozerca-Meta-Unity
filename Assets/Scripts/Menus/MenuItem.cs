using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class MenuItem : MonoBehaviour {

	public Items item;
	public enum Items {Start, Credits, Quit}

	public void Open () {
		switch (item) {
			case Items.Start:
				SceneManager.LoadScene("Game");
			break;
			case Items.Credits:
				MainMenu.instance.Credits();
			break;
			case Items.Quit:
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
				#endif
				Application.Quit();
			break;
		}
	}
}
