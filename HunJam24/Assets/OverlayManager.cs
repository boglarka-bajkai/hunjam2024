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
	[SerializeField] UnityEngine.UI.Button restart;
	[SerializeField] UnityEngine.UI.Button start;
	[SerializeField] UnityEngine.UI.Button quit;
	[SerializeField] UnityEngine.UI.Button backToMenu;
	[SerializeField] UnityEngine.UI.Button backToMenu2;

	static OverlayManager _instance;
	void Awake() {
		if (_instance != null) Destroy(this);
		_instance = this;
		restart.onClick.AddListener(() =>{
			MapLoader.Instance.RestartMap();
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
		backToMenu.onClick.AddListener(() =>{
			MapLoader.Instance.BackToMenu();
			loseScreen.SetActive(false);
			menuScreen.SetActive(true);
		});
		backToMenu2.onClick.AddListener(() =>{
			MapLoader.Instance.BackToMenu();
			winScreen.SetActive(false);
			gameOverlay.SetActive(false);
			menuScreen.SetActive(true);
		});
		loseScreen.SetActive(false);
		winScreen.SetActive(false);
		menuScreen.SetActive(true);
		gameOverlay.SetActive(false);
	}
	public static OverlayManager Instance => _instance;

	

	public void ShowLoseScreen() {
		loseScreen.SetActive(true);
		MapLoader.playing = false;
	}

	public void ShowWinScreen() { 
		winScreen.SetActive(true);
		MapLoader.playing = false;
		restart.gameObject.SetActive(false);
	}


}
