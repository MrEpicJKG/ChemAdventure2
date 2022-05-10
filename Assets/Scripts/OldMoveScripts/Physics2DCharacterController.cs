using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physics2DCharacterController : MonoBehaviour
{
	#region Public Vars
	public float groundSidewaysMoveForce = 2.0f;
	public float airborneSidewaysMoveForce = 0.5f;
	public float groundSidewaysMoveMaxSpeed = 4.0f;
	public float airborneSidewaysMoveMaxSpeed = 1.0f;
	public float jumpForce = 10;
	public float maxYVelocity = 6;
	//public float maxHealth = 100;
	#endregion
	#region Hidden Public Vars
	public bool isLeftBtnDown = false;
	public bool isRightBtnDown = false;
	public bool isJumpBtnDown = false;

	public bool prevLeftBtnState = false;
	public bool prevRightBtnState = false;
	public bool prevJumpBtnState = false;

	//[HideInInspector] public float currHealth;
	#endregion
	#region Private Vars
	private Rigidbody2D rb;
	[SerializeField] private bool canJump = true; //this is whether or not the player is able to jump
	[SerializeField] private bool shouldJump = false; //this holds whether or not the jump key was pressed this tick
	[SerializeField] private bool isAirborne = false; //this is pretty self-explanitory...
	private GameManager manager;
	[SerializeField] private int numJumpsSinceLeavingGround = 0;
	[SerializeField] private bool prevRunningState = true;
	private Vector2 prePauseVelocity;
	private bool isMovingRight = true; // if false, the player is going left
	private BoxCollider2D boxCollider;
	#endregion
	#region Default Events
	// Start is called before the first frame update
	void Start()
	{
		manager = GameObject.Find("GameController").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		prePauseVelocity = Vector2.zero;
		boxCollider = GetComponent<BoxCollider2D>();
		//currHealth = maxHealth;
	}

	// Update is called once per frame
	void Update()
	{
		if (manager.isGameRunning == true)
		{
			//this, plus the giant if at the end of GetUserInput, are my slidey player work-around. Basiclly, when you release a movement key, 
			//it sets the X movement constrant to true then sets it back to false at the start of the next frame.
			if (rb.constraints != RigidbodyConstraints2D.FreezeRotation)
			{ rb.constraints = RigidbodyConstraints2D.FreezeRotation; }

			//if the game was unpaused this tick, set the players velocity back to the value it was at prior to pausing.
			if(prevRunningState == false)
			{ rb.velocity = prePauseVelocity; }

			//bool isDead = TestForDeath();
			//if(isDead == true)
			//{
			//	manager.isGameRunning = false;
			//	manager.GoToLoseScene();
			//}

			GetInputAndMove();

			//do a raycast to each side to see if we are hitting a wall and if canJump is false. if true, set canJump to true.
			//if ((CastRay(Vector2.right, 1) == true || CastRay(Vector2.left, 1) == true) && canJump == false)
			//{ canJump = true; }

			//disable jumping if we have already double jumped
			if (numJumpsSinceLeavingGround >= 2)
			{ canJump = false; }

			//jump
			DoJump();

			//set a max cap for the Y velocity, to prevent people from rocketing up the walls by spamming the jump key.
			if (rb.velocity.y > maxYVelocity)
			{ rb.velocity.Set(rb.velocity.x, maxYVelocity); }

			//prevent occational player rotation on hitting tile corner bug
			if(transform.eulerAngles != Vector3.zero)
			{ transform.eulerAngles = Vector3.zero; }

			//##### DEBUG #####
			Debug.DrawRay(transform.position, new Vector3(rb.velocity.x, 0, 0) / 2, Color.red);
			Debug.DrawRay(transform.position, new Vector3(0, rb.velocity.y, 0) / 2, Color.green);
			Debug.DrawRay(transform.position, new Vector3(rb.velocity.x, rb.velocity.y, 0) / 2, Color.yellow);

			Debug.DrawRay(boxCollider.bounds.min, new Vector3(rb.velocity.x, rb.velocity.y, 0) / 2, Color.white);
			Debug.DrawRay(new Vector3(boxCollider.bounds.min.x, boxCollider.bounds.max.y, 0), new Vector3(rb.velocity.x, rb.velocity.y, 0) / 2, Color.white);
			Debug.DrawRay(new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y, 0), new Vector3(rb.velocity.x, rb.velocity.y, 0) / 2, Color.white);
			Debug.DrawRay(boxCollider.bounds.max, new Vector3(rb.velocity.x, rb.velocity.y, 0) / 2, Color.white);

			//##### END DEBUG #####

			//update the "prev" variables to the new values from this tick. 
			prevLeftBtnState = isLeftBtnDown;
			prevRightBtnState = isRightBtnDown;
			prevJumpBtnState = isJumpBtnDown;
		}
		else if(manager.isGameRunning == false && prevRunningState == true)
		{
		//if the game was paused this tick, then store the players velocity and then freeze their position.
			prePauseVelocity = rb.velocity;
			rb.constraints = RigidbodyConstraints2D.FreezeAll; 
		}

		prevRunningState = manager.isGameRunning;
	}
	#endregion
	#region Functions
	private void DoJump()
	{
		if (canJump == true && shouldJump == true)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			//Play Jump SFX
			//manager.sfxAudioSource_Gen.PlayOneShot(manager.jumpSFX);
			isAirborne = true;
			shouldJump = false;
			numJumpsSinceLeavingGround++;
		}
	}
	private void GetInputAndMove()
	{

		if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true || isLeftBtnDown == true) //try to move left
		{
		if (isAirborne == false && rb.velocity.x > -groundSidewaysMoveMaxSpeed)
		{ rb.AddForce(new Vector2(-groundSidewaysMoveForce, 0), ForceMode2D.Force); }
		else if (isAirborne == true && rb.velocity.x > -airborneSidewaysMoveMaxSpeed)
		{ rb.AddForce(new Vector2(-airborneSidewaysMoveForce, 0), ForceMode2D.Force); }

		GetComponent<SpriteRenderer>().flipX = true;
		}

		if(Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true || isRightBtnDown == true)
		{  
			if (isAirborne == false && rb.velocity.x<groundSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(groundSidewaysMoveForce, 0), ForceMode2D.Force); }
			else if (isAirborne == true && rb.velocity.x<airborneSidewaysMoveMaxSpeed)
			{ rb.AddForce(new Vector2(airborneSidewaysMoveForce, 0), ForceMode2D.Force); }

			GetComponent<SpriteRenderer>().flipX = false;
		}
		
		
		if((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.Space) == true || (isJumpBtnDown == true && prevJumpBtnState == false)) && (isAirborne == false || numJumpsSinceLeavingGround < 2))
		{ shouldJump = true; }

		//if the player is on the ground and you just released A, D, Left Arrow, Right Arrow, Left Button, or Right Button or you have any combination of 2 of those pressed down at the same time, freeze the character's X position for 1 frame.
		if (isAirborne == false && (Input.GetKeyUp(KeyCode.A) == true || Input.GetKeyUp(KeyCode.D) == true ||
		Input.GetKeyUp(KeyCode.LeftArrow) == true || Input.GetKeyUp(KeyCode.RightArrow) == true ||
		(isLeftBtnDown == false && prevLeftBtnState == true) ||
		(isRightBtnDown == false && prevRightBtnState == true) ||
		(Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.D) == true) ||
		(Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.RightArrow) == true) ||
		(Input.GetKey(KeyCode.D) == true && Input.GetKey(KeyCode.LeftArrow) == true) ||
		(Input.GetKey(KeyCode.LeftArrow) == true && Input.GetKey(KeyCode.RightArrow) == true) ||
		(isLeftBtnDown == true && isRightBtnDown == true)))
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
	//private bool TestForDeath()
	//{
	//	bool isDead = false;
	//	if(currHealth <= 0)
	//	{ isDead = true; }
	//	return isDead;
	//}
	#endregion
	#region //Trigger Enter/Exit
	private void OnTriggerEnter2D(Collider2D other)
	{
		//List<Collider2D> overlapResults = new List<Collider2D>(10);
		//LayerMask mask = LayerMask.GetMask("Enviroment");
		//ContactFilter2D filter = new ContactFilter2D();
		//filter.layerMask = mask;
		if (other.gameObject.tag == "Enviroment" && CastRay(Vector2.down, 1.1f) == true)// && RightEdgeCollider.OverlapCollider(filter, overlapResults) == 0 && LeftEdgeCollider.OverlapCollider(filter, overlapResults) == 0)
		{ 
			isAirborne = false;
			canJump = true;
			numJumpsSinceLeavingGround = 0;
			//rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		//List<Collider2D> overlapResults = new List<Collider2D>(10);
	//	LayerMask mask = LayerMask.GetMask("Enviroment");
		//ContactFilter2D filter = new ContactFilter2D();
		//filter.layerMask = mask;
	if (other.gameObject.tag == "Enviroment")//&& RightEdgeCollider.OverlapCollider(filter, overlapResults) == 0 && LeftEdgeCollider.OverlapCollider(filter, overlapResults) == 0)
		{ isAirborne = true; }
	}
	#endregion
}
