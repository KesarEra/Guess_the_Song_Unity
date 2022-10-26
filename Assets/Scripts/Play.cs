using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

	static public string[,] Songs = new string[180, 2]{
        {"Uptown Funk",	"Mark Ronson"},
        {"Shape of You", "Ed Sheeran"},
        {"Hello", "Adele"},
        {"Work", "Rihanna"},
        {"Despacito", "Luis Fonsi"},
        {"Firework", "Katy Perry"},
        {"Watermelon Sugar", "Harry Styles"},
        {"Levitating", "Dua Lipa"},
        {"Therefore I Am", "Billie Eilish"},
        {"Shake It Off", "Taylor Swift"},
        {"Old Town Road", "Lil Nas X"},
        {"Happy", "Pharrell Williams"},
        {"Chained to the Rhythm", "Katy Perry"},
        {"Monster", "Shawn Mendes"},
        {"New Rules", "Dua Lipa"},
        {"ily", "surf mesa"},
        {"Lonely", "Justin Bieber"},
        {"Holiday", "Lil Nas X"},
        {"Adore You", "Harry Styles"},
        {"All About That Bass", "Meghan Trainor"},
        {"Wreaking Ball", "Miley Cyrus"},
        {"Havana", "Camila Cabello"},
        {"Blank Space", "Taylor Swift"},
        {"Closer", "The Chainsmokers"},
        {"Mood", "24kGoldn"},
        {"No Time To Die", "Billie Eilish"},
        {"Positions", "Ariana Grande"},
        {"Rare", "Selena Gomez"},
        {"Rain On Me", "Lady Gaga"},
        {"Physical", "Dua Lipa"},
        {"Chandelier", "Sai"},
        {"Sorry", "Justin Bieber"},
        {"Bad Guy", "Billie Eilish"},
        {"Sandcastles", "Beyonce"},
        {"Golden", "Harry Styles"},
        {"Circles", "Post Malone"},
        {"Wishing Well", "Juice WRLD"},
        {"Tyler Herro", "Jack Harlow"},
        {"Midnight Sky", "Miley Cyrus"},
        {"Shallow", "Lady Gaga"},
        {"Rockstar", "Post Malone"},
        {"Drag Me Down", "One Direction"},
        {"Bad Liar", "Selena Gomez"},
        {"No Tears Left to Cry", "Ariana Grande"},
        {"Savage Love", "Jason Derulo"},
        {"Wake Me Up", "Avicii"},
        {"Dark Horse", "Katy Perry"},
        {"Just Like Fire", "P!nk"},
        {"Imagine", "John Lennon"},
        {"Rolling in the Deep", "Adele"},
        {"Call me Maybe", "Carly Rae Jepsen"},
        {"Just the Way You Are", "Bruno Mars"},
        {"7 Summers", "Morgan Wallen"},
        {"Diamonds", "Sam Smith"},
        {"Bang!", "AJR"},
        {"Hey Jude", "The Beatles"},
        {"What'd I Say", "Ray Charles"},
        {"Vulnerable", "Selena Gomez"},
        {"pov", "Ariana Grande"},
        {"Good Time", "Niko Moon"},
        {"Dynamite", "BTS"},
        {"DNA", "BTS"},
        {"P.O.P", "J-Hope"},
        {"Magic Island", "TXT"},
        {"Honeymoon", "BAP"},
        {"Lovesick Girls", "Blackpink"},
        {"TT", "Twice"},
        {"BBIBBI", "IU"},
        {"Freak", "Kim Sawol"},
        {"Falling", "Kim Feel"},
        {"Blue & Grey", "BTS"},
        {"Scenery", "V"},
        {"Seoul", "RM"},
        {"Our Summer", "TXT"},
        {"1004", "BAP"},
        {"Our Page", "SHINee"},
        {"Kill This Love", "Blackpink"},
        {"Feel Special", "Twice"},
        {"Ray", "Kim Sawol"},
        {"Karma", "Demian"},
        {"Pretty Savage", "Blackpink"},
        {"Tear", "BTS"},
        {"Winter Bear", "V"},
        {"Promise", "Jimin"},
        {"Eternally", "TXT"},
        {"Coffee Shop", "BAP"},
        {"Hikikomori", "Bang Yongguk"},
        {"Dance The Night Away", "Twice"},
        {"Eight", "IU"},
        {"Map of Dreams", "Kim Sawol"},
        {"Mikrokosmos", "BTS"},
        {"Honsool", "Agust D"},
        {"Drama", "TXT"},
        {"Moondance", "BAP"},
        {"Ya", "Bang Yongguk"},
        {"Good Evening", "SHINee"},
        {"Key", "Kim Sawol"},
        {"Devil", "Kim Sawol"},
        {"Yes", "Demian"},
        {"Happening", "AKMU"},
        {"Zombie", "DAY6"},
        {"Stay Here", "Gaho"},
        {"Neverland", "Holland"},
        {"Killing Me", "iKON"},
        {"Blue", "Onew"},
        {"Just One Day", "BTS"},
        {"Tonight", "Jin"},
        {"Still With You", "Jungkook"},
        {"Coming Home", "Bang Yongguk"},
        {"Snow Field", "Kim Sawol"},
        {"Cassette", "Demian"},
        {"Holo", "Lee Hi"},
        {"Nan Chun", "Se So Neon"},
        {"Winter Flower", "Younha"},
        {"Any Song", "Zico"},
        {"Face", "Woosung"},
        {"Lonely Night", "Yoon Du Jun"},
        {"Love Shot", "EXO"},
        {"My Ocean", "Jeong Sewoon"},
        {"When It Snows", "1415"},
        {"Sonic the Hedgehog", "Sonic the Hedgehog "},
        {"Arthur", "Arthur"},
        {"X-Men", "X-Men"},
        {"Spider-Man", "Spider-Man"},
        {"One Punch Man", "One Punch Man"},
        {"Tokyo Ghoul", "Tokyo Ghoul"},
        {"Spongebob Squarepants", "Spongebob Squarepants"},
        {"Pink Panther", "Pink Panther"},
        {"Adventure Time", "Adventure Time"},
        {"The Jetsons", "The Jetsons"},
        {"The Simpsons", "The Simpsons"},
        {"The Flintstones", "The Flintstones"},
        {"Scooby-Doo", "Scooby-Doo"},
        {"Animaniacs", "Animaniacs"},
        {"Dragon Ball Z", "Dragon Ball Z"},
        {"Attack On Titan", "Attack On Titan"},
        {"Death Note", "Death Note"},
        {"The Tick", "The Tick"},
        {"Teen Titans", "Teen Titans"},
        {"Family Guy", "Family Guy"},
        {"Inspector Gadget", "Inspector Gadget"},
        {"Speed Racer", "Speed Racer"},
        {"Hong Kong Phooey", "Hong Kong Phooey"},
        {"Super Chicken", "Super Chicken"},
        {"Mighty Mouse", "Mighty Mouse"},
        {"Underdog", "Underdog"},
        {"Pinky and The Brain", "Pinky and The Brain"},
        {"Fullmetal Alchemist", "Fullmetal Alchemist"},
        {"Ghost In The Shell", "Ghost In The Shell"},
        {"Naruto", "Naruto"},
        {"Yuri!!! On Ice", "Yuri!!! On Ice"},
        {"Death Parade", "Death Parade"},
        {"Ben 10", "Ben 10"},
        {"DuckTales", "DuckTales"},
        {"Gravity Falls", "Gravity Falls"},
        {"Popeye The Sailor Man", "Popeye The Sailor Man"},
        {"Rick and Morty", "Rick and Morty"},
        {"Cowboy Bebop", "Cowboy Bebop"},
        {"The Smurfs", "The Smurfs"},
        {"Alvin and the Chipmunks", "Alvin and the Chipmunks"},
        {"Darkwing Duck", "Darkwing Duck"},
        {"Heathcliff", "Heathcliff"},
        {"Tiny Toon Adventures", "Tiny Toon Adventures"},
        {"Freakazoid", "Freakazoid"},
        {"Road Runner", "Road Runner"},
        {"Noragami", "Noragami"},
        {"Pokemon", "Pokemon"},
        {"The Looney Tunes Show", "The Looney Tunes Show"},
        {"My Hero Academia", "My Hero Academia"},
        {"Code Geass", "Code Geass"},
        {"Horrid Henry", "Horrid Henry"},
        {"TailSpin", "TailSpin"},
        {"Goof Troop", "Goof Troop"},
        {"The Magic School Bus", "The Magic School Bus"},
        {"Taz-Mania", "Taz-Mania"},
        {"Josie & the Pussycats", "Josie & the Pussycats"},
        {"Jem & the Holograms", "Jem & the Holograms"},
        {"ThunderCats", "ThunderCats"},
        {"Teenage Mutant Ninja Turtles", "Teenage Mutant Ninja Turtles"},
        {"The Transformers", "The Transformers"}
    };
	static public int CatSel;
	static public int LevSel;
    static public int[,] Levels = new int[5,2]{ 
    					{5, 10}, 
                        {7, 20}, 
                        {7, 30},
                        {10, 45},
                        {10, 60} 
                    };
	static public int OverallScore, PopScore, KpopScore, ThemeSongsScore;

	void Start() {
		PopScore = PlayerPrefs.GetInt("PopScore");
		KpopScore = PlayerPrefs.GetInt("KpopScore");
		ThemeSongsScore = PlayerPrefs.GetInt("ThemeSongsScore");
		OverallScore = PopScore + KpopScore + ThemeSongsScore;
		PlayerPrefs.SetInt("OverallScore", OverallScore);
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Play") {
			GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + PlayerPrefs.GetInt("OverallScore").ToString();
		} else if (scene.name == "Pop") {
			GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Pop Score: " + PopScore.ToString();
		} else if (scene.name == "Kpop") {
			GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Kpop Score: " + KpopScore.ToString();
		} else if (scene.name == "ThemeSongs") {
			GameObject.Find("ScoreUpdate").GetComponent<TMPro.TextMeshProUGUI>().text = "Theme Songs Score: " + ThemeSongsScore.ToString();
		}
	}

	public void CategorySelection(int CategorySelected) {
		CatSel = CategorySelected;
		LevSel = 1;
	}

	public void LevelSelection(int LevelSelected) {
		LevSel = LevelSelected;
		Debug.Log("Rounds = " + Levels[(LevSel - 1), 0]);//For checking purposes.
	}

}