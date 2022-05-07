using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTimer : MonoBehaviour
{
    float startTime = 0;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0) == true)
		{
			startTime = Time.unscaledTime;
		}
		else if(Input.GetMouseButtonUp(0) == true)
		{
			print("Tap Duration: " + (Time.unscaledTime - startTime) + " secs.");
		}
	}
}
