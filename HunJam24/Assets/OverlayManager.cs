using System.IO;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OverlayManager : MonoBehaviour
{
	[SerializeField] GameObject loseScreen;
	[SerializeField] UnityEngine.UI.Button restart;
	static OverlayManager _instance;
	void Awake() {
		if (_instance != null) Destroy(this);
		_instance = this;
		restart.onClick.AddListener(() =>{
			MapLoader.Instance.RestartMap();
			Character.movingCount--;
			loseScreen.SetActive(false);
		});
	}
	public static OverlayManager Instance => _instance;


	public void ShowLoseScreen() {
		loseScreen.SetActive(true);
		Character.movingCount++;
	}

}
