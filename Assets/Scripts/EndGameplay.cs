using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameplay : MonoBehaviour {
    
    string newText = "";
    string[,] GameplaySongsArray;
    bool[] CorrectGuessesArray;

    void Start() {
        GameplaySongsArray = Gameplay.getGameplaySongs();
        CorrectGuessesArray = Gameplay.getCorrectGuesses();
        if (Play.CatSel == 3) {
            for (int i = 0; i < Gameplay.TotalRounds; i++) {
                if (CorrectGuessesArray[i] == true) {
                    newText += (i + 1) + ". Correct. " + GameplaySongsArray[i, 0] + "\n";
                } else {
                    newText += (i + 1) + ". Incorrect. Correct: " + GameplaySongsArray[i, 0] + "\n";
                }
            } 
        } else {
            for (int i = 0; i < Gameplay.TotalRounds; i++) {
                if (CorrectGuessesArray[i] == true) {
                    newText += (i + 1) + ". Correct. " + GameplaySongsArray[i, 0] + ", " + GameplaySongsArray[i, 1] + "\n";
                } else {
                    newText += (i + 1) + ". Incorrect. Correct: " + GameplaySongsArray[i, 0] + ", " + GameplaySongsArray[i, 1] + "\n";
                }
            }            
        }
        GameObject.Find("GamePlaySongs").GetComponent<TMPro.TextMeshProUGUI>().text = newText;
        GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Game Score: " + PlayerPrefs.GetInt("GameScore").ToString();
        if (Play.CatSel == 1) {
            GameObject.Find("CategoryScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Pop Score: " + PlayerPrefs.GetInt("PopScore").ToString();
        } else if (Play.CatSel == 2) {
            GameObject.Find("CategoryScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Kpop Score: " + PlayerPrefs.GetInt("KpopScore").ToString();
        } else {
            GameObject.Find("CategoryScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Theme Songs Score: " + PlayerPrefs.GetInt("ThemeSongsScore").ToString();
        }
    }

}
