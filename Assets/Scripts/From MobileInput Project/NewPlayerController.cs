using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


//NOTE: I copied a lot (though not all) of this code from my PlayerController script in my 2DTools Assignment (Mostly I redesigned the GetInputAndMove Function to work with the joystick)
[RequireComponent(typeof(Rigidbody2D))]
public class NewPlayerController : MonoBehaviour
{
	[Header("Move Forces")]
	[SerializeField] private float groundSidewaysMoveForce = 2.0f;
	[SerializeField] private float airborneSidewaysMoveForce = 0.5f;
	[SerializeField] private float groundSidewaysMoveMaxSpeed = 4.0f;
	[SerializeField] private float airborneSidewaysMoveMaxSpeed = 1.0f;
	[SerializeField] private float jumpForce = 10;

	[Header("Max Values")]
	[SerializeField] private float maxYVelocity = 6;

	[Header("Control Stuff")]
	[SerializeField] [Range(0.01f, 0.99f)] private float minJoystickPercentToTriggerMovement_X = 0.1f;

	[HideInInspector] public bool canJump = true; //this is whether or not the player is able to jump
	[HideInInspector] public bool isAirborne = false; //this is pretty self-explanitory...

	private Rigidbody2D rb;
	private GameManager manager;
	private bool prevRunningState = true;
	private Vector2 prePauseVelocity;
	private float horizInput = 0;
	private int numJumpsSinceLeavingGround = 0;

	// Start is called before the first frame update
	void Start()
    {
		manager = GameObject.Find("GameController").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		prePauseVelocity = Vector2.zero;
	}

    // Update is called once per frame
    void Update()
    {
		if (manager.isGameRunning == true)
		{ UpdateMovement(); }
		else if(manager.isGameRunning == false && prevRunningState == true)
		{
			//if the game was paused this tick, then store the players velocity and then freeze their position.
			prePauseVelocity = rb.velocity;
			rb.constraints = RigidbodyConstraints2D.FreezeAll; 
		}

		prevRunningState = manager.isGameRunning;
	}

	private void UpdateMovement()
	{
		horizInput = CrossPlatformInputManager.GetAxis("Horizontal");
		//this, plus the giant if at the end of GetUserInput, are my slidey player work-around. Basiclly, when you release a movement key, 
		//it sets the X movement constrant to true then sets it back to false at the start of the next frame.
		if (rb.constraints != RigidbodyConstraints2D.FreezeRotation)
		{ rb.constraints = RigidbodyConstraints2D.FreezeRotation; }

		//if the game was unpaused this tick, set the players velocity back to the value it was at prior to pausing.
		if (prevRunningState == false)
		{ rb.velocity = prePauseVelocity; }

		GetJoyInputAndMove();

		//do a raycast to each side to see if we are hitting a wall and if canJump is false. if true, set canJump to true.
		if ((CastRay(Vector2.right, 0.55f) == true || CastRay(Vector2.left, 0.55f) == true) && canJump == false)
		{ canJump = true; }

		isAirborne = !CastRay(Vector2.down, 0.55f);

		//disable jumping if we have already double jumped
		if (numJumpsSinceLeavingGround >= 2)
		{ canJump = false; }

		//set a max cap for the Y velocity, to prevent people from rocketing up the walls by spamming the jump key.
		if (rb.velocity.y > maxYVelocity)
		{ rb.velocity.Set(rb.velocity.x, maxYVelocity); }

		//prevent occational player rotation on hitting tile corner bug
		if (transform.eulerAngles != Vector3.zero)
		{ transform.eulerAngles = Vector3.zero; }
	}
	[HideInInspector] public void DoJump()
	{
		if (canJump == true)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			//Play Jump SFX
			//manager.sfxAudioSource_Gen.PlayOneShot(manager.jumpSFX);
			isAirborne = true;
			numJumpsSinceLeavingGround++;
		}
	}
	private void GetJoyInputAndMove()
	{
		//print("H: " + CrossPlatformInputManager.GetAxis("Horizontal") + "   ## V: " + CrossPlatformInputManager.GetAxis("Vertical"));
		
		if (horizInput < -minJoystickPercentToTriggerMovement_X) //try to move left
		{
			if (isAirborne == false && rb.velocity.x > -groundSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(groundSidewaysMoveForce * horizInput, 0), ForceMode2D.Force); }
			else if (isAirborne == true && rb.velocity.x > -airborneSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(airborneSidewaysMoveForce * horizInput, 0), ForceMode2D.Force); }

			GetComponent<SpriteRenderer>().flipX = true;
		}

		if (horizInput > minJoystickPercentToTriggerMovement_X) //try to move right
		{
			if (isAirborne == false && rb.velocity.x < groundSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(groundSidewaysMoveForce * horizInput, 0), ForceMode2D.Force); }
			else if (isAirborne == true && rb.velocity.x < airborneSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(airborneSidewaysMoveForce * horizInput, 0), ForceMode2D.Force); }

			GetComponent<SpriteRenderer>().flipX = false;
		}

		if(isAirborne == false && horizInput < minJoystickPercentToTriggerMovement_X && horizInput > -minJoystickPercentToTriggerMovement_X)
		{ rb.constraints = RigidbodyConstraints2D.FreezePosition; }
		else if (isAirborne == false && horizInput < minJoystickPercentToTriggerMovement_X && horizInput > -minJoystickPercentToTriggerMovement_X)
		{ rb.constraints = RigidbodyConstraints2D.FreezePositionX; }
	}
	private bool CastRay(Vector2 direction, float distance)
	{
		bool didHitEnviro = false;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Enviroment"));
		if (hit.transform != null)
		{ didHitEnviro = true; }
		return didHitEnviro;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enviroment")
		{
			isAirborne = false;
			canJump = true;
			numJumpsSinceLeavingGround = 0;
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enviroment")
		{ isAirborne = true; }
	}
}
