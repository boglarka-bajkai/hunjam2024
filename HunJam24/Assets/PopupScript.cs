using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

public class PopupScript : MonoBehaviour
{
	public int starsCount;
	public int stepsCount;
	public Sprite starEmpty;
	public Sprite starFull;
	public Image[] stars;
	public TMP_Text StepText;
	public GameObject popUpPanel;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		// For testing, comment this out in production
		//SetNumbers(starsCount, stepsCount);
    }

	void SetNumbers(int stepsCount, int starsCount = 0)
	{
		popUpPanel.SetActive(true);
		for (int i = 0; i < Math.Min(starsCount, 3); i++)
		{
			stars[i].GetComponent<Image>().sprite = starFull;
		}

		StepText.SetText($"Steps taken: {stepsCount}");
	}
}
