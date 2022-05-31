using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.UI;


public class AimController : MonoBehaviour
{
    public float attkDelaySecs = 0.25f; //public because it will be set by the weapon
    [SerializeField] private float canControlFacingDirectionDelaySecs = 0.5f;
    [SerializeField] private Transform playerRoot;
    [SerializeField] private Transform playerUpperBody;
    [SerializeField] private Transform playerNearUpperArm;
    [SerializeField] private Transform playerFarUpperArm;
    [SerializeField] private Transform playerHead;
    [SerializeField] private RectTransform leftBtnTrans;
    [SerializeField] private RectTransform rightBtnTrans;
    [SerializeField] private float maxAngleOffset = 30;
    [SerializeField] private bool usingRangedWeapon = true; //false = melee weapon

    private bool canAttk = true;
    private PlatformerCharacter2D charControl;
    private TouchInputManager2 TIM;
    private GameManager manager;
	void Start()
	{
        manager = GameObject.Find("GameController").GetComponent<GameManager>();
        charControl = GameObject.Find("PlayerRoot").GetComponent<PlatformerCharacter2D>();
        TIM = GameObject.Find("GameController").GetComponent<TouchInputManager2>();
	}

	//// Update is called once per frame
	//void Update()
	//{

	//}

	public void RotatePlayer(Touch touch)
    {
        if (manager.isGameRunning == true)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
            Vector3 difference = target - playerUpperBody.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion lookRot = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            Quaternion halfLookRot = Quaternion.Euler(0.0f, 0.0f, rotationZ / 2);

            playerUpperBody.rotation = halfLookRot;
            playerFarUpperArm.rotation = halfLookRot;
            playerNearUpperArm.rotation = halfLookRot;
            playerHead.rotation = halfLookRot;
        }
    }

    //----- Tap Events -----
    public void OnLeftAttkTap()
    {
        Invoke("ResetCanControlFacing", canControlFacingDirectionDelaySecs);
        charControl.canControlFacingDirection = false;
        charControl.m_FacingRight = false;
        if (playerRoot.localScale.x != -1)
        {
            playerRoot.localScale = new Vector3(-1, playerRoot.localScale.y, playerRoot.localScale.z);
		}

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && leftBtnTrans.rect.Contains(touch.position) == true)
            {
                RotatePlayer(touch);
                if (canAttk == true)
                {
                    TIM.Attack();
                    Invoke("ResetCanAttk", attkDelaySecs);
                    canAttk = false;
                    print("Attacked to the Left!! (Touch #1)");
                }
            }
        }
        else if(Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began && leftBtnTrans.rect.Contains(touch.position) == true)
            {
                RotatePlayer(touch);
                if (canAttk == true)
                {
                    TIM.Attack();
                    Invoke("ResetCanAttk", attkDelaySecs);
                    canAttk = false;
                    print("Attacked to the Left!! (Touch #2)");
                }
            }
        }
	}

    public void OnRightAttkTap()
    {
        Invoke("ResetCanControlFacing", canControlFacingDirectionDelaySecs);
        charControl.canControlFacingDirection = false;
        charControl.m_FacingRight = true;
        if (playerRoot.localScale.x != 1)
        {
            playerRoot.localScale = new Vector3(1, playerRoot.localScale.y, playerRoot.localScale.z);
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && rightBtnTrans.rect.Contains(touch.position) == true)
            {
                RotatePlayer(touch);
                if (canAttk == true)
                {
                    TIM.Attack();
                    Invoke("ResetCanAttk", attkDelaySecs);
                    canAttk = false;
                    print("Attacked to the Right!! (Touch #1)");
                }
            }
        }
        else if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began && rightBtnTrans.rect.Contains(touch.position) == true)
            {
                RotatePlayer(touch);
                if (canAttk == true)
                {
                    TIM.Attack();
                    Invoke("ResetCanAttk", attkDelaySecs);
                    canAttk = false;
                    print("Attacked to the right!! (Touch #2)");
                }
            }
        }
    }

    private void ResetCanControlFacing()
    {
        charControl.canControlFacingDirection = true;
	}

    private void ResetCanAttk()
    {
        canAttk = true;
	}


}
