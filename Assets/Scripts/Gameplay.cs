using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//GetComponent<Image>().color = Color.red;

public class Gameplay : Play {

	int RoundsInTotal = Levels[LevSel - 1, 0];
    static public int TotalRounds;
	static public string[,] GameplaySongs;
	string[] Options = new string[4]; // Array for options
	GameObject ArtistText, SongText, Option1Button, Option2Button, Option3Button, Option4Button; // These are variables of type GameObject;
	Image TimerBar;
    TMPro.TextMeshProUGUI RoundText, Option1Text, Option2Text, Option3Text, Option4Text, ScoreText;
	float maxTime = 10f;
	float timeLeft;
    int round = 0;
    int GameScore;
    int value;//Refers to corect option number
    public KeyCode Option1Key, Option2Key, Option3Key, Option4Key; // Shortcuts for Keys
    static public bool[] CorrectGuesses; // Array for the correct guesses made by player to print 
    //gameplay songs at the end of gameplay. I created this array because I wad unable to change colour of options for long enough.

    void Start() {
        PlayerPrefs.SetInt("RoundsInTotal", RoundsInTotal);
        TotalRounds = PlayerPrefs.GetInt("RoundsInTotal");
        GameplaySongs = new string[TotalRounds, 2];
        CorrectGuesses = new bool[TotalRounds];
    	//CSVtoArray();
    	GamePlay();
    }

    void Update() {
    	if (round < TotalRounds) {
    		if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				TimerBar.fillAmount = timeLeft/maxTime;
                InvokeButton(Option1Key, Option1Button);
                InvokeButton(Option2Key, Option2Button);
                InvokeButton(Option3Key, Option3Button);
                InvokeButton(Option4Key, Option4Button);
			} else {
				GamePlay();
			}
    	} else if (round == TotalRounds) {
    		if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				TimerBar.fillAmount = timeLeft/maxTime;
                InvokeButton(Option1Key, Option1Button);
                InvokeButton(Option2Key, Option2Button);
                InvokeButton(Option3Key, Option3Button);
                InvokeButton(Option4Key, Option4Button);
			} else {
				round += 1;
			}
    	} else {
    		Debug.Log("Done!");
            GameObject.Find("ChangeSceneScriptObject").GetComponent<ChangeScene>().Scene_Change("EndOfGamePlay");
    	}
    }

    void Awake() {
    	SongText = GameObject.Find("SongName");
    	ArtistText = GameObject.Find("ArtistName");
    	TimerBar = GameObject.Find("Timer").GetComponent<Image>();
        RoundText = GameObject.Find("RoundUpdate").GetComponent<TMPro.TextMeshProUGUI>();
        Option1Text = GameObject.Find("Option1").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Option2Text = GameObject.Find("Option2").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Option3Text = GameObject.Find("Option3").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Option4Text = GameObject.Find("Option4").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Option1Button = GameObject.Find("Option1");
        Option2Button = GameObject.Find("Option2");
        Option3Button = GameObject.Find("Option3");
        Option4Button = GameObject.Find("Option4");
        ScoreText = GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>();
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
        RoundText.text = "Round: " + round.ToString() + "/" + TotalRounds.ToString();
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
        int UB = LB - 1 + Levels[Level - 1, 1];
        int RndSongNum = UnityEngine.Random.Range(LB, (UB + 1));
        String RndSong = Songs[RndSongNum, Name];
        GameplaySongs[turn, 0] = Songs[RndSongNum, 0];
        GameplaySongs[turn, 1] = Songs[RndSongNum, 1];
        return RndSong;
    }

    public String RndSongGenerator(int Category, int Level, int Name) {
        int LB = (Category - 1) * 60;
        int UB = LB - 1 + Levels[Level - 1, 1];
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
        Option1Text.text = "1. " + Options[0];
        Option2Text.text = "2. " + Options[1];
        Option3Text.text = "3. " + Options[2];
        Option4Text.text = "4. " + Options[3];
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

    static public bool[] getCorrectGuesses() {
        return CorrectGuesses;
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(2);
        Debug.Log("Wait is over!");
    }

    public void InvokeButton(KeyCode key, GameObject button) {
        if (Input.GetKeyDown(key)) {
            button.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void OptionClicked(int Num) {
        if (value == Num) {
            ScoringSystem();
            SetColors(Num);
            CorrectGuesses[round - 1] = true;
        } else {
            SetCorrectColors();
            CorrectGuesses[round - 1] = false;
        }
        timeLeft = 0;
        ResetColors();
        FindObjectOfType<AudioManager>().Stop(GameplaySongs[round - 1, 0]);
    }

    public void ResetColors() {
        Option1Button.GetComponent<Image>().color = Color.black;
        Option2Button.GetComponent<Image>().color = Color.black;
        Option3Button.GetComponent<Image>().color = Color.black;
        Option4Button.GetComponent<Image>().color = Color.black;
    }

    public void SetColors(int OptionNum) {
        if (OptionNum == 1) {
            Option1Button.GetComponent<Image>().color = Color.green;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else if (OptionNum == 2) {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.green;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else if (OptionNum == 3) {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.green;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.green;
        }
    }

    public void SetCorrectColors() {
        if (value == 1) {
            Option1Button.GetComponent<Image>().color = Color.green;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else if (value == 2) {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.green;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else if (value == 3) {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.green;
            Option4Button.GetComponent<Image>().color = Color.red;
        } else {
            Option1Button.GetComponent<Image>().color = Color.red;
            Option2Button.GetComponent<Image>().color = Color.red;
            Option3Button.GetComponent<Image>().color = Color.red;
            Option4Button.GetComponent<Image>().color = Color.green;
        }
    }

    public void ScoringSystem() {
        GameScore += Convert.ToInt32(timeLeft) * Levels[LevSel - 1, 0];
        ScoreText.text = GameScore.ToString();
        PlayerPrefs.SetInt("GameScore", GameScore);
        if (CatSel == 1) {
            PopScore += Convert.ToInt32(timeLeft) * Levels[LevSel - 1, 0];
            PlayerPrefs.SetInt("PopScore", PopScore);
        } else if (CatSel == 2) {
            KpopScore += Convert.ToInt32(timeLeft) * Levels[LevSel - 1, 0];
            PlayerPrefs.SetInt("KpopScore", KpopScore);
        } else {
            ThemeSongsScore += Convert.ToInt32(timeLeft) * Levels[LevSel - 1, 0];
            PlayerPrefs.SetInt("ThemeSongsScore", ThemeSongsScore);
        }
    }

}
