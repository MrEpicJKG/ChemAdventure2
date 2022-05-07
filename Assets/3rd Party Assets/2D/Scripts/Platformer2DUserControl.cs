using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        [HideInInspector] public bool m_Jump;
        [HideInInspector] public bool shouldGoLeft;
        [HideInInspector] public bool shouldGoRight;
        [HideInInspector] public bool shouldCrouch;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                if (Input.GetKeyDown(KeyCode.W) == true || CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    m_Jump = true;
                }
                else
                {
                    m_Jump = false;
				}
            }

            //if (!shouldGoLeft)
            //{
                // Read the jump input in Update so button presses aren't missed.
                if (Input.GetKeyDown(KeyCode.A) == true || CrossPlatformInputManager.GetButtonDown("Left"))
                {
                    shouldGoLeft = true;
                    shouldGoRight = false;
                }
                else if (Input.GetKeyUp(KeyCode.A) == true || CrossPlatformInputManager.GetButtonUp("Left"))
                {
                    shouldGoLeft = false;
                }
            //}

           // if (!shouldGoRight)
           // {
                // Read the jump input in Update so button presses aren't missed.
                if (Input.GetKeyDown(KeyCode.D) == true || CrossPlatformInputManager.GetButtonDown("Right"))
                {
                    shouldGoRight = true;
                    shouldGoLeft = false;
                }
                else if (Input.GetKeyUp(KeyCode.D) == true || CrossPlatformInputManager.GetButtonUp("Right"))
                {
                    shouldGoRight = false;
                }
           // }

            if (!shouldCrouch)
            {
                // Read the jump input in Update so button presses aren't missed.
                if (Input.GetKeyDown(KeyCode.S) == true || CrossPlatformInputManager.GetButtonDown("Crouch"))
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
            }

        }


        private void FixedUpdate()
        {
            // Read the inputs.
            float h = 0;
            if(shouldGoRight == true)
            {
                h = 1;
			}
            else if(shouldGoLeft == true)
            {
                h = -1;
			}
            else
            {
                h = 0;
			}
            // Pass all parameters to the character control script.
            m_Character.Move(h, shouldCrouch, m_Jump);
            print("h: " + h.ToString() + "   Left: " + shouldGoLeft + "   Right: " + shouldGoRight + "   Crouch: " + shouldCrouch.ToString() + "   Jump: " + m_Jump);
            m_Jump = false;
            shouldCrouch = false;
        }
    }
}
