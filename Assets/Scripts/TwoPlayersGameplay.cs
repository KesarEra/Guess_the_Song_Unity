using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//GetComponent<Image>().color = Color.red;

public class TwoPlayersGameplay : Play {

	static public int TotalRounds = 11;
    int round = 0;
	static public string[,] GameplaySongs = new string[TotalRounds, 2];
	string[] Options = new string[4]; // Array for options
	Image TimerBar;
	float maxTime = 10f;
	float timeLeft;
    int value;//Refers to corect option number
    int player1Score = 0, player2Score = 0;
    // Below are variables of type GameObject;
    GameObject ArtistText, SongText, P1Option1Button, P1Option2Button, P1Option3Button, P1Option4Button, P2Option1Button, P2Option2Button, P2Option3Button, P2Option4Button;
    TMPro.TextMeshProUGUI RoundText, P1Option1Text, P1Option2Text, P1Option3Text, P1Option4Text, P2Option1Text, P2Option2Text, P2Option3Text, P2Option4Text, P1score, P2score;
    public KeyCode P1Option1Key, P1Option2Key, P1Option3Key, P1Option4Key, P2Option1Key, P2Option2Key, P2Option3Key, P2Option4Key; // Shortcuts for Keys

    void Start() {
    	//CSVtoArray();
    	GamePlay();
    }

    void Update() {
    	if (round < TotalRounds) {
    		if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				TimerBar.fillAmount = timeLeft/maxTime;
                InvokeButton(P1Option1Key, P1Option1Button);
                InvokeButton(P1Option2Key, P1Option2Button);
                InvokeButton(P1Option3Key, P1Option3Button);
                InvokeButton(P1Option4Key, P1Option4Button);
                InvokeButton(P2Option1Key, P2Option1Button);
                InvokeButton(P2Option2Key, P2Option2Button);
                InvokeButton(P2Option3Key, P2Option3Button);
                InvokeButton(P2Option4Key, P2Option4Button);
			} else {
				GamePlay();
			}
    	} else if (round == TotalRounds) {
    		if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				TimerBar.fillAmount = timeLeft/maxTime;
                InvokeButton(P1Option1Key, P1Option1Button);
                InvokeButton(P1Option2Key, P1Option2Button);
                InvokeButton(P1Option3Key, P1Option3Button);
                InvokeButton(P1Option4Key, P1Option4Button);
                InvokeButton(P2Option1Key, P2Option1Button);
                InvokeButton(P2Option2Key, P2Option2Button);
                InvokeButton(P2Option3Key, P2Option3Button);
                InvokeButton(P2Option4Key, P2Option4Button);
			} else {
				round += 1;
			}
    	} else {
    		Debug.Log("Done!");
            PlayerPrefs.SetInt("Player1Score", player1Score);
            PlayerPrefs.SetInt("Player2Score", player2Score);
            GameObject.Find("ChangeSceneScriptObject").GetComponent<ChangeScene>().Scene_Change("2PlayersEnd");
    	}
    }

    void Awake() {
        
        ArtistText = GameObject.Find("ArtistName");
    	SongText = GameObject.Find("SongName");
    	TimerBar = GameObject.Find("Timer").GetComponent<Image>();
        RoundText = GameObject.Find("RoundUpdate").GetComponent<TMPro.TextMeshProUGUI>();

        P1Option1Button = GameObject.Find("P1Option1");
        P1Option2Button = GameObject.Find("P1Option2");
        P1Option3Button = GameObject.Find("P1Option3");
        P1Option4Button = GameObject.Find("P1Option4");
        P2Option1Button = GameObject.Find("P2Option1");
        P2Option2Button = GameObject.Find("P2Option2");
        P2Option3Button = GameObject.Find("P2Option3");
        P2Option4Button = GameObject.Find("P2Option4");


        P1Option1Text = GameObject.Find("P1Option1").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P1Option2Text = GameObject.Find("P1Option2").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P1Option3Text = GameObject.Find("P1Option3").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P1Option4Text = GameObject.Find("P1Option4").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P2Option1Text = GameObject.Find("P2Option1").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P2Option2Text = GameObject.Find("P2Option2").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P2Option3Text = GameObject.Find("P2Option3").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        P2Option4Text = GameObject.Find("P2Option4").GetComponentInChildren<TMPro.TextMeshProUGUI>();

        P1score = GameObject.Find("P1score").GetComponent<TMPro.TextMeshProUGUI>();
        P2score = GameObject.Find("P2score").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void GamePlay() {
    	TimerBar.fillAmount = 1;
    	timeLeft = maxTime;
        Debug.Log($"Round {round + 1}");
        int name = NameSelection();
        do { 
            value = SetOptions(round, name);
        } while (CheckPreviousGameplaySongs(round) == true);
        PrintingOptions();
        FindObjectOfType<AudioManager>().Play(GameplaySongs[round, 0]);
        Debug.Log("GameplaySongs[" + round + ", 0] = " + GameplaySongs[round, 0]);
        Debug.Log("GameplaySongs[" + round + ", 1] = " + GameplaySongs[round, 1]);
        round += 1;
        RoundText.text = "Round: " + round.ToString() + "/11";
    }

    /*public void CSVtoArray() {
        try {
            string[] lines = File.ReadAllLines(@filePath);
            for(int i = 0; i < lines.Length; i++) {
                string[] fields = lines[i].Split(',');
                Songs[i, 0] = fields[0];
                Songs[i, 1] = fields[1];
            }
        } catch(Exception ex) {
            Debug.Log("The file could not be read.");
        	Debug.Log(ex.Message);
        }
    }*/

    public int NameSelection() {
    	int Name = UnityEngine.Random.Range(0, 2);
    	if(Name == 0) {
    		SongText.SetActive(true);
    		ArtistText.SetActive(false);
    	} else {
    		ArtistText.SetActive(true);
    		SongText.SetActive(false);
    	}
    	return Name;
    }

    public String TheRndSongGenerator(int Category, int Level, int Name, int turn) {
        int LB = (Category - 1) * 60;
        int UB = (CatSel * 60) - 1;
        int RndSongNum = UnityEngine.Random.Range(LB, (UB + 1));
        String RndSong = Songs[RndSongNum, Name];
        GameplaySongs[turn, 0] = Songs[RndSongNum, 0];
        GameplaySongs[turn, 1] = Songs[RndSongNum, 1];
        return RndSong;
    }

    public String RndSongGenerator(int Category, int Level, int Name) {
        int LB = (Category - 1) * 60;
        int UB = (CatSel * 60) - 1;
        int RndSongNum = UnityEngine.Random.Range(LB, (UB + 1));
        String RndSong = Songs[RndSongNum, Name];
        return RndSong;
    }

    public bool CheckPreviousOptions(int CheckOptionNum) {
        for (int i = 0; i < CheckOptionNum; i++) {
            if (Options[i] == Options[CheckOptionNum]) {
                return true;
            }
        }
        return false;
    }

    public int SetOptions(int turn, int name) {
        String TheRndSong = TheRndSongGenerator(CatSel, LevSel, name, turn);
        Debug.Log($"TheRndSong = {TheRndSong}");
        int CorrectRndOption = UnityEngine.Random.Range(0, 4);
        Debug.Log(CorrectRndOption + 1);// Prints Correct Choice - For Checking
        for (int i = 0; i < 4; i++) {
            if (i == CorrectRndOption) {
                Options[i] = TheRndSong;
            } else {
                String SomeRndSong;
                do {
                    do {
                        SomeRndSong = RndSongGenerator(CatSel, LevSel, name);
                    } while (SomeRndSong == TheRndSong);
                    Options[i] = SomeRndSong;
                } while (CheckPreviousOptions(i) == true);
            }
        }
        return (CorrectRndOption + 1);
    }

    public void PrintingOptions() {
        P1Option1Text.text = "1. " + Options[0];
        P1Option2Text.text = "2. " + Options[1];
        P1Option3Text.text = "3. " + Options[2];
        P1Option4Text.text = "4. " + Options[3];
        P2Option1Text.text = "←. " + Options[0];
        P2Option2Text.text = "↑. " + Options[1];
        P2Option3Text.text = "↓. " + Options[2];
        P2Option4Text.text = "→. " + Options[3];
    }

    public bool CheckPreviousGameplaySongs(int CheckSongNum) {
        for (int i = 0; i < CheckSongNum; i++) {
            if (GameplaySongs[i, 0] == GameplaySongs[CheckSongNum, 0]) {
                return true;
            }
        }
        return false;
    }

    static public string[,] getGameplaySongs() {
        return GameplaySongs;
    }

    public void InvokeButton(KeyCode key, GameObject button) {
        if (Input.GetKeyDown(key)) {
            button.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void OptionClicked(GameObject button) {
        int Num;
        if (button.name == "P1Option1" || button.name == "P2Option1") {
            Num = 1;
        } else if (button.name == "P1Option2" || button.name == "P2Option2") {
            Num = 2;
        } else if (button.name == "P1Option3" || button.name == "P2Option3") {
            Num = 3;
        } else {
            Num = 4;
        }
        if (value == Num) {
            ScoringSystem(button, true);
            SetColors(Num);
        } else {
            ScoringSystem(button, false);
            SetCorrectColors();
        }
        timeLeft = 0;
        ResetColors();
        FindObjectOfType<AudioManager>().Stop(GameplaySongs[round - 1, 0]);
    }

    public void ResetColors() {
        P1Option1Button.GetComponent<Image>().color = Color.black;
        P1Option2Button.GetComponent<Image>().color = Color.black;
        P1Option3Button.GetComponent<Image>().color = Color.black;
        P1Option4Button.GetComponent<Image>().color = Color.black;
        P2Option1Button.GetComponent<Image>().color = Color.black;
        P2Option2Button.GetComponent<Image>().color = Color.black;
        P2Option3Button.GetComponent<Image>().color = Color.black;
        P2Option4Button.GetComponent<Image>().color = Color.black;
    }

    public void SetColors(int OptionNum) {
        if (OptionNum == 1) {
            P1Option1Button.GetComponent<Image>().color = Color.green;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.green;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else if (OptionNum == 2) {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.green;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.green;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else if (OptionNum == 3) {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.green;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.green;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.green;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.green;
        }
    }

    public void SetCorrectColors() {
        if (value == 1) {
            P1Option1Button.GetComponent<Image>().color = Color.green;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.green;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else if (value == 2) {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.green;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.green;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else if (value == 3) {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.green;
            P1Option4Button.GetComponent<Image>().color = Color.red;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.green;
            P2Option4Button.GetComponent<Image>().color = Color.red;
        } else {
            P1Option1Button.GetComponent<Image>().color = Color.red;
            P1Option2Button.GetComponent<Image>().color = Color.red;
            P1Option3Button.GetComponent<Image>().color = Color.red;
            P1Option4Button.GetComponent<Image>().color = Color.green;
            P2Option1Button.GetComponent<Image>().color = Color.red;
            P2Option2Button.GetComponent<Image>().color = Color.red;
            P2Option3Button.GetComponent<Image>().color = Color.red;
            P2Option4Button.GetComponent<Image>().color = Color.green;
        }
    }

    public void ScoringSystem(GameObject someButton, bool Boolean) {
        if (Boolean == true) {
            if (someButton.name == "P1Option1" || someButton.name == "P1Option2" || someButton.name == "P1Option3" || someButton.name == "P1Option4") {
                player1Score += 1;
                P1score.text = "Player 1 Score = " + player1Score.ToString();
            } else {
                player2Score += 1;
                P2score.text = "Player 2 Score = " + player2Score.ToString();
            }
        } else {
            if (someButton.name == "P1Option1" || someButton.name == "P1Option2" || someButton.name == "P1Option3" || someButton.name == "P1Option4") {
                player2Score += 1;
                P2score.text = "Player 2 Score = " + player2Score.ToString();
            } else {
                player1Score += 1;
                P1score.text = "Player 1 Score = " + player1Score.ToString();
            }
        }
    }

    static public int ReturnWinner() {
        if (PlayerPrefs.GetInt("Player1Score") > PlayerPrefs.GetInt("Player2Score")) {
            return 1;
        } else {
            return 2;
        }
    }

}
