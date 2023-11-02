using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
	public int gameSpeed = 1;
	public TextMeshProUGUI gameSpeedText;
	// Update is called once per frame
	void Update()
	{
		//Set the ui Text
		gameSpeedText.text = "X" + gameSpeed.ToString();
		//update speed of our game • reitinGressmigersiones peed
		Time.timeScale = gameSpeed;
	}
	public void ChangeSpeed()
	{
		//as long as gameSpeed is not 4 allow us to increase level
		if (gameSpeed < 4)
		{
			gameSpeed++;
		}
		// if game speed reach to 4 with the next push button condition is false and give gameSpeed value 1
		else if (gameSpeed == 4)
		{
			gameSpeed = 1;
		}
	}
}

