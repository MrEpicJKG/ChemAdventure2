using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

public class TouchInputManager2 : MonoBehaviour
{
    [SerializeField] [Range(0.01f, 0.99f)] private float minJoystickMoveThresholdPrcnt = 0.1f;
    [SerializeField] private Vector2 joystickSafetyZoneSize;
    

    [HideInInspector] public bool shouldJump = false;
    [HideInInspector] public bool shouldCrouch = false;
    [HideInInspector] public float attkDelaySecs = 0.1f; //this will be set by the currently equipped weapon

    private Joystick joystick;
    private Vector2 initJoyPos;
    private GameManager manager;
    private PlatformerCharacter2D characterControl;
    private float horizInput = 0;
    private bool playerCanAttk = true;
    private bool playerWantsToAttk = false;
    //private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.Find("MobileJoystick").GetComponentInChildren<Joystick>();
        initJoyPos = joystick.transform.position;
        manager = GameObject.Find("GameController").GetComponent<GameManager>();
        characterControl = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameRunning == true)
        {
            if(Input.touchCount > 0)
            {
                DetermineMoveOrAttack();
			}
        }
    }

	private void FixedUpdate()
	{
        if (manager.isGameRunning == true)
        {
            characterControl.Move(horizInput, shouldCrouch, shouldJump);
            shouldJump = false;
        }
	}

	private void DetermineMoveOrAttack()
    {
        float joyRange = joystick.MovementRange;
        if(Input.touchCount == 1)
        {
            Touch touch1 = Input.GetTouch(0);
            if ((touch1.phase == TouchPhase.Began) && 
                touch1.position.x > initJoyPos.x + (joystickSafetyZoneSize.x / 2) || 
                touch1.position.x < initJoyPos.x - (joystickSafetyZoneSize.x / 2) ||
                touch1.position.y > initJoyPos.y + (joystickSafetyZoneSize.y / 2) ||
                touch1.position.y < initJoyPos.y - (joystickSafetyZoneSize.y / 2))
                {
                    Attack();
				}
            else
            {
                Move();
			}

        }
        else if (Input.touchCount > 1)
        {
            bool touchUsedForAttk = false;
            Touch touch1 = Input.GetTouch(0);
            if ((touch1.phase == TouchPhase.Began) &&
                touch1.position.x > initJoyPos.x + (joystickSafetyZoneSize.x / 2) ||
                touch1.position.x < initJoyPos.x - (joystickSafetyZoneSize.x / 2) ||
                touch1.position.y > initJoyPos.y + (joystickSafetyZoneSize.y / 2) ||
                touch1.position.y < initJoyPos.y - (joystickSafetyZoneSize.y / 2))
            {
                if (playerCanAttk == true)
                {
                    Attack();
                    touchUsedForAttk = true;
                    playerCanAttk = false;
                    Invoke("ResetCanAttk", attkDelaySecs);
                }
            }
            else
            {
                Move();
            }

            Touch touch2 = Input.GetTouch(1);
            if ((touch2.phase == TouchPhase.Began) &&
                touch2.position.x > initJoyPos.x + (joystickSafetyZoneSize.x / 2) ||
                touch2.position.x < initJoyPos.x - (joystickSafetyZoneSize.x / 2) ||
                touch2.position.y > initJoyPos.y + (joystickSafetyZoneSize.y / 2) ||
                touch2.position.y < initJoyPos.y - (joystickSafetyZoneSize.y / 2))
            {
                if (playerCanAttk == true)
                {
                    if (touchUsedForAttk == false)
                    {
                        Attack();
                        playerCanAttk = false;
                        Invoke("ResetCanAttk", attkDelaySecs);
                    }
                }
                
            }
            else
            {
                Move();
            }

        }
    }

    private void Move()
    {
        float HInput = CrossPlatformInputManager.GetAxis("Horizontal");
        if(HInput < -minJoystickMoveThresholdPrcnt || HInput > minJoystickMoveThresholdPrcnt)
        {
            horizInput = HInput;
		}
        else
        {
            horizInput = 0;
		}
    }

    private void Attack()
    {
        
	}

    public void OnCrouchTap()
    {
        if(shouldCrouch == true)
        {
            shouldCrouch = false;
		}
        else
        {
            shouldCrouch = true;
		}
	}

    public void OnJumpTap()
    {
        shouldJump = true;
	}
    
    private void ResetCanAttk()
    {
        playerCanAttk = true;
	}
}
