using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour {

	public GameObject score;
	public GameObject hiScore;

	public AudioClip normal;
	public AudioClip hi;

	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = normal;

		int lastHiScore = PlayerPrefs.GetInt("HiScore", 0);
		if (EndValues.score >= lastHiScore) {
			// If player's score is higher than high score
			// Enables "! NEW BEST SCORE !" text, sets audioclip
			hiScore.SetActive(true);
			audio.clip = hi;
			PlayerPrefs.SetInt("HiScore", EndValues.score);
			lastHiScore = EndValues.score;
		}
		audio.Play();
		
		score.GetComponent<Text>().text = $"YOUR SCORE:\t{EndValues.score}\nTOP SCORE:\t{lastHiScore}";
	}
	
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
			SceneManager.LoadScene("MainMenu");
	}
}
