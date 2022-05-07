using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MouseUpController : MonoBehaviour
{
    [SerializeField] private Platformer2DUserControl controls;
    public ButtonType buttonType = ButtonType.Left;

    public enum ButtonType { Left, Right, Crouch};

	public void OnMouseUpAsButton()
	{
		if(buttonType == ButtonType.Left)
		{
			if(controls.shouldGoLeft == true)
			{
				controls.shouldGoLeft = false;
			}
		}
		else if(buttonType == ButtonType.Right)
		{
			if(controls.shouldGoRight == true)
			{
				controls.shouldGoRight = false;
			}
		}
		else if(buttonType == ButtonType.Crouch)
		{
			if(controls.shouldCrouch == true)
			{
				controls.shouldCrouch = false;
			}
		}
	}
}
