using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInputManager : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler;
{
    [SerializeField] private RectTransform controlsSafetyRectTrans;
    [SerializeField] private RectTransform leftRectTrans;
    [SerializeField] private RectTransform rightRectTrans;
    [SerializeField] private Platformer2DUserControl controls;

	void Update()
    {

		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMin, controlsSafetyRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMin, controlsSafetyRectTrans.rect.yMax))aaaaaa, Color.white);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMin, controlsSafetyRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMax, controlsSafetyRectTrans.rect.yMin)), Color.white);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMin, controlsSafetyRectTrans.rect.yMax)), Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMax, controlsSafetyRectTrans.rect.yMax)), Color.white);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMax, controlsSafetyRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(controlsSafetyRectTrans.rect.xMax, controlsSafetyRectTrans.rect.yMax)), Color.white);

		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMin, rightRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMin, rightRectTrans.rect.yMax)), Color.green);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMin, rightRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMax, rightRectTrans.rect.yMin)), Color.green);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMin, rightRectTrans.rect.yMax)), Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMax, rightRectTrans.rect.yMax)), Color.green);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMax, rightRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(rightRectTrans.rect.xMax, rightRectTrans.rect.yMax)), Color.green);

		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMin, leftRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMin, leftRectTrans.rect.yMax)), Color.blue);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMin, leftRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMax, leftRectTrans.rect.yMin)), Color.blue);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMin, leftRectTrans.rect.yMax)), Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMax, leftRectTrans.rect.yMax)), Color.blue);
		//Debug.DrawLine(Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMax, leftRectTrans.rect.yMin)), Camera.main.ScreenToViewportPoint(new Vector2(leftRectTrans.rect.xMax, leftRectTrans.rect.yMax)), Color.blue);
		Touch[] touches = Input.touches;
        bool isLRDown = false;
        foreach(Touch touch in touches)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(touch.position);

            Debug.DrawLine(new Vector2(pos.x - 0.5f, pos.y), new Vector2(pos.x + 0.5f, pos.y), Color.red);
            Debug.DrawLine(new Vector2(pos.x, pos.y - 0.5f), new Vector2(pos.x, pos.y + 0.5f), Color.red);

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
   //         else
   //         {
   //             //Detect and React to control button presses
   //             if(touch.phase == TouchPhase.Began)
   //             {
   //                 if(rightRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go right
   //                 {
   //                     controls.shouldGoRight = true;
			//		}
   //                 else if(leftRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go left
   //                 {
   //                     controls.shouldGoLeft = true;
			//		}
   //                 Debug.Log("Touch Started");
			//	}
   //             else if(touch.phase == TouchPhase.Ended)
   //             {
   //                 if (rightRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go right
   //                 {
   //                     controls.shouldGoRight = false;
   //                 }
   //                 else if (leftRectTrans.rect.Contains(Camera.main.ScreenToViewportPoint(touch.position)) == true) //go left
   //                 {
   //                     controls.shouldGoLeft = false;
   //                 }
   //                 Debug.Log("Touch Ended");
   //             }
			//}
		}

        
    }

	public void OnJumpPress()
	{
		controls.m_Jump = true;
		print("jump called");
	}

	//   public void OnLeftPress()
	//   {
	//       controls.shouldGoLeft = true;
	//       controls.shouldGoRight = false;
	//       print("left called");
	//}

	//   public void OnRightPress()
	//   {
	//       controls.shouldGoRight = true;
	//       controls.shouldGoLeft = false;
	//       print("right called");
	//}

	public void OnDownPress()
	{
        if (controls.shouldCrouch == true)
        {
            controls.shouldCrouch = false;
        }
        else
        {
            controls.shouldCrouch = true;
        }
        print("down called");

        
	}

    

}
