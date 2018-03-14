using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0; 
	private Text MyText;
	// Use this for initialization
	void Start () {
		MyText = GetComponent<Text> ();
		Reset ();
	}
	
	public void AddScore(int points)
	{
		score += points;
		MyText.text = score.ToString ();
	}

	public static void Reset()
	{
		score = 0;

	}
}
