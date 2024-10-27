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
	[SerializeField] UnityEngine.UI.Button restart;
	[SerializeField] UnityEngine.UI.Button start;
	[SerializeField] UnityEngine.UI.Button quit;
	[SerializeField] UnityEngine.UI.Button quit2;
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
		});
		quit.onClick.AddListener(() =>{
			Application.Quit();
		});
		quit2.onClick.AddListener(() =>{
			Application.Quit();
		});
		
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

}
