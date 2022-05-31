using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameRunning = true;
    public bool clickCapturedByControls = false;
    public bool clickCapturedByInteractScript = false;
    public bool clickCapturedByUI = false;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

	private void LateUpdate()
	{
        clickCapturedByInteractScript = false;
        clickCapturedByControls = false;
        clickCapturedByUI = false;
	}
}
