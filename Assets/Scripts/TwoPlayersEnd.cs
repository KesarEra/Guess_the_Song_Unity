using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayersEnd : MonoBehaviour {

    string newText = "";
    string[,] GameplaySongsArray;
    int Winner;

    void Start() {
        Winner = TwoPlayersGameplay.ReturnWinner();
        if (Winner == 1) {
            GameObject.Find("WinnerPlayer2").SetActive(false);
            GameObject.Find("WinnerPlayer1").SetActive(true);
        } else {
            GameObject.Find("WinnerPlayer1").SetActive(false);
            GameObject.Find("WinnerPlayer2").SetActive(true);
        }
        GameplaySongsArray = TwoPlayersGameplay.getGameplaySongs();
        if (Play.CatSel == 3) {
            for (int i = 0; i < 11; i++) {
                newText += (i + 1) + ". " + GameplaySongsArray[i, 0] + "\n";
            }
        } else {
            for (int i = 0; i < 11; i++) {
                newText += (i + 1) + ". " + GameplaySongsArray[i, 0] + ", " + GameplaySongsArray[i, 1] + "\n";
            }
        }
        GameObject.Find("GamePlaySongs").GetComponent<TMPro.TextMeshProUGUI>().text = newText;
        GameObject.Find("P1score").GetComponent<TMPro.TextMeshProUGUI>().text = "Player 1 Score = " + PlayerPrefs.GetInt("Player1Score");
        GameObject.Find("P2score").GetComponent<TMPro.TextMeshProUGUI>().text = "Player 2 Score = " + PlayerPrefs.GetInt("Player2Score");
    }
    
}
