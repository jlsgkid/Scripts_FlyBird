using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // スクリプト冒頭に追加
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	enum State{

		Ready,
		Play,
		GameOver

	}
	State state;
	int score;
	public AzarashiController flybird;
	public GameObject blocks;
	public Text scoreLabel;
	public Text stateLabel;


	// Use this for initialization
	void Start () {
		Ready();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate(){

		switch(state){

		case State.Ready:
			if( Input.GetButtonDown("Fire1") ) GameStart();
			break;
		case State.Play:
			if( flybird.IsDead() ) GameOver();
			break;
		case State.GameOver:
			if( Input.GetButtonDown("Fire1")  ) Reload();
			break;
		}
	}

	void Ready(){
		state = State.Ready;
		flybird.SetSteerActive(false);
		blocks.SetActive(false);

		//label
		scoreLabel.text = "Score :" + 0;
		stateLabel.gameObject.SetActive (true);
		stateLabel.text = "Touch start !";
	}

	void GameStart(){
		state = State.Play;
		//flybird.gameObject.transform.position.x = -0.2f;
		flybird.gameObject.transform.position = new Vector3 (-0.2f, flybird.gameObject.transform.position.y, 0);
		flybird.SetSteerActive(true);
		blocks.SetActive(true);
		flybird.Flap();

		stateLabel.gameObject.SetActive (false);
		stateLabel.text = "";
	}

	void GameOver(){

		state = State.GameOver;
		ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
		foreach( ScrollObject so in scrollObjects ){
			so.enabled = false;
		}

		stateLabel.gameObject.SetActive (true);
		//string[] CellContent = new string[]{"Game Over !\nTouch restart!"};
		string str = "Game Over~\nTouch restart!";
		stateLabel.color = Color.red;
		stateLabel.text = str.Replace ("\\n", "\n");
	}

	void Reload(){
		//Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene("Flybird1"); // Application.LoadLevel()から置き換え
	}

	public void IncreaseScore(){
		score += 10;
		scoreLabel.text = "Score :" + score;
	}


}
