using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInputManager : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform controlsSafetyRectTrans;
	//[SerializeField] private RectTransform leftRectTrans;
	//[SerializeField] private RectTransform rightRectTrans;
	//[SerializeField] private Platformer2DUserControl controls;

	private bool shouldJump = false;
	private bool shouldCrouch;

	[HideInInspector] public float axis = 0;

	private PlatformerCharacter2D player;

	public void Start()
	{
		player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
	}


	public void Update()
	{
		Touch[] touches = Input.touches;
		bool isLRDown = false;
		foreach (Touch touch in touches)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

			if (controlsSafetyRectTrans.rect.Contains(pos) == false)
			{
				RaycastHit2D hit;
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				hit = Physics2D.Raycast(ray.origin, ray.direction);
				if (hit.transform != null)
				{
					ObjectInteractController oic = hit.transform.GetComponent<ObjectInteractController>();
					if (oic != null)
					{
						oic.OnTapped();
					}
				}
			}
		}


		player.Move(axis, shouldCrouch, shouldJump);
			/*		else
			//		{
			//			//Detect and React to control button presses
			//			if (touch.phase == TouchPhase.Began)
			//			{
			//				if (rightRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go right
			//				{
			//					controls.shouldGoRight = true;
			//				}
			//				else if (leftRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go left
			//				{
			//					controls.shouldGoLeft = true;
			//				}
			//				Debug.Log("Touch Started");
			//			}
			//			else if (touch.phase == TouchPhase.Ended)
			//			{
			//				if (rightRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go right
			//				{
			//					controls.shouldGoRight = false;
			//				}
			//				else if (leftRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go left
			//				{
			//					controls.shouldGoLeft = false;
			//				}
			//				Debug.Log("Touch Ended");
			//			}
			//		}*/
	}
	public void OnJumpPress()
	{
		shouldJump = true;
		print("jump called");
	}

	public void OnDownPress()
	{
        if (shouldCrouch == true)
        {
            shouldCrouch = false;
        }
        else
        {
            shouldCrouch = true;
        }
        print("down called");
	}

    

}
