using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class TouchInputManager : MonoBehaviour
{
    [SerializeField] private RectTransform controlsSafetyRectTrans;
    [SerializeField] private RectTransform leftRectTrans;
    [SerializeField] private RectTransform rightRectTrans;
    [SerializeField] private Platformer2DUserControl controls;

	void Update()
    {
        Touch[] touches = Input.touches;
        bool isLRDown = false;
        foreach(Touch touch in touches)
        {
            Vector3 pos = touch.position;
            if (controlsSafetyRectTrans.rect.Contains(pos) == false)
            {
                RaycastHit2D hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                hit = Physics2D.Raycast(ray.origin, ray.direction);
                ObjectInteractController oic = hit.transform.GetComponent<ObjectInteractController>();
                if (oic != null)
                {
                    oic.OnTapped();
				}
            }
            else
            {
                if(leftRectTrans.rect.Contains(pos) == true || rightRectTrans.rect.Contains(pos) == true)
                {
                    isLRDown = true;
				}
			}
		}

        
    }

    public void OnJumpPress()
    {
        controls.m_Jump = true;
        print("jump called");
	}

    public void OnLeftPress()
    {
        controls.shouldGoLeft = true;
        controls.shouldGoRight = false;
        print("left called");
	}

    public void OnRightPress()
    {
        controls.shouldGoRight = true;
        controls.shouldGoLeft = false;
        print("right called");
	}

    public void OnDownPress()
    {
        controls.shouldCrouch = true;
        print("down called");
	}

}
