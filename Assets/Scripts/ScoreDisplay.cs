using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Text MyText = GetComponent<Text> ();
		MyText.text = ScoreKeeper.score.ToString ();
		ScoreKeeper.Reset ();
	}
	

}
