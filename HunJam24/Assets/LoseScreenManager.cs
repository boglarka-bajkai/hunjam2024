using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoseScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		var bg = GameObject.Find("Screenshot").GetComponent<UnityEngine.UI.Image>();
		Texture2D Tex2D;
		byte[] FileData;
		if (File.Exists("Died.png"))
		{
			FileData = File.ReadAllBytes("Died.png");
			Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
			if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
				bg.sprite = Sprite.Create(Tex2D, new Rect(0, 0, Tex2D.width, Tex2D.height), new Vector2(0, 0), 100.0f);
		}
		
    }
}
