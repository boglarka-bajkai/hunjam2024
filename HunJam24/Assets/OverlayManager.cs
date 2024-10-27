using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OverlayManager : MonoBehaviour
{
	[SerializeField] GameObject loseScreen;
	[SerializeField] GameObject winScreen;
	static OverlayManager _instance;
	void Awake() {
		if (_instance != null) Destroy(this);
		_instance = this;
	}
	public static OverlayManager Instance => _instance;


	public void ShowLoseScreen() {
		loseScreen.SetActive(true);
		//Time.timeScale = 0f;
	}

	public void ShowWinScreen() { 
		winScreen.SetActive(true);
	}

}
