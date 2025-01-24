using System.IO;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OverlayManager : MonoBehaviour
{
	[SerializeField] GameObject loseScreen;
	[SerializeField] GameObject winScreen;
	[SerializeField] GameObject menuScreen;
	[SerializeField] GameObject gameOverlay;
	[SerializeField] GameObject buildOverlay;
	[SerializeField] GameObject uploadOverlay;
	[SerializeField] UnityEngine.UI.Button restart;
	[SerializeField] UnityEngine.UI.Button start;
	[SerializeField] UnityEngine.UI.Button quit;
	[SerializeField] UnityEngine.UI.Button quit2;
	[SerializeField] UnityEngine.UI.Button buildMap;
	[SerializeField] UnityEngine.UI.Button testBuild;
	[SerializeField] UnityEngine.UI.Button quitBuild;
	static OverlayManager _instance;
	void Awake() {
		if (_instance != null) Destroy(this);
		_instance = this;
		restart.onClick.AddListener(() =>{
			MapLoader.Instance.RestartMap();
			Character.movingCount--;
			loseScreen.SetActive(false);
		});
		start.onClick.AddListener(() =>{
			MapLoader.Instance.StartGame();
			menuScreen.SetActive(false);
			gameOverlay.SetActive(true);
		});
		quit.onClick.AddListener(() =>{
			Application.Quit();
		});
		quit2.onClick.AddListener(() =>{
			Application.Quit();
		});
		buildMap.onClick.AddListener(() => {
			MapLoader.Instance.StartMapEditor();
			menuScreen.SetActive(false);
			buildOverlay.SetActive(true);
		});
		testBuild.onClick.AddListener(() => {
			buildOverlay.SetActive(false);
			gameOverlay.SetActive(true);
			TilePlacer.Instance.TestMap();
		});
		loseScreen.SetActive(false);
		winScreen.SetActive(false);
		menuScreen.SetActive(true);
		gameOverlay.SetActive(false);
	}
	public static OverlayManager Instance => _instance;


	public void ShowLoseScreen() {
		loseScreen.SetActive(true);
		Character.movingCount++;
	}

	public void ShowWinScreen() { 
		winScreen.SetActive(true);
		Character.movingCount++;
		restart.gameObject.SetActive(false);
	}

	public void BackToEditor(){
		buildOverlay.SetActive(true);
		gameOverlay.SetActive(false);
	}

	public void ShowUploadOverlay() {
		gameOverlay.SetActive(false);
		buildOverlay.SetActive(false);

	}

}
