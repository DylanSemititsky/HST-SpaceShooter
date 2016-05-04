using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {

	public Text[] highScores;
	int[] highScoreValues;
	string[] highScoreNames;

	int j;

	void Start(){
		highScoreValues = new int[highScores.Length];
		highScoreNames = new string[highScores.Length];
		for (int i = 0; i < highScores.Length; i++) {
			highScoreValues [i] = PlayerPrefs.GetInt ("highScoreValues" + i);
			highScoreNames [i] = PlayerPrefs.GetString ("highScoreNames" + i);
		}
		DrawScores ();
	}


	void SaveScores(){
		for (int i = 0; i < highScores.Length; i++) {
			PlayerPrefs.SetInt ("highScoreValues" + i, highScoreValues [i]);
			PlayerPrefs.SetString ("highScoreNames" + i, highScoreNames [i]);
		}
	}

	public void CheckForHighScore(int _value, string _userName){
		for (int i = 0; i < highScores.Length; i++) {
			if (_value > highScoreValues [i]) {
				for (j = highScores.Length - 1; j > i; j--) {
					highScoreValues [j] = highScoreValues [j - 1];
					highScoreNames [j] = highScoreNames [j - 1];
				}
				highScoreValues [j] = _value;
				highScoreNames [j] = _userName;
				DrawScores ();
				SaveScores ();
				break;
			}
		}
	}

	void DrawScores(){
		for (int i = 0; i < highScores.Length; i++) {
			highScores [i].text = (i+1) + ". " + highScoreValues [i].ToString () + " : " + highScoreNames[i];
		}
	}
}