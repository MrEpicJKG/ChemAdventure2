using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PointAndShoot : MonoBehaviour
{
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float starSpeed = 60.0f;
	[SerializeField] private float timeBetweenShots = 0.5f;
	[SerializeField] private float verticalJoystickTouchSafetyZoneHeight = 140;

	private Vector3 target;
	private GameObject player;
	private GameObject starSpawn;
	private Joystick joyStick;
	private Vector2 initJoyPos;
	private bool canAttk = true;
	private bool shouldAttk = false;
	private GameManager manager;


	// Start is called before the first frame update
	void Start()
    {
        //Cursor.visible = false;
		player = transform.parent.gameObject;
		starSpawn = transform.GetChild(0).gameObject;
		joyStick = GameObject.Find("MobileJoystick").GetComponent<Joystick>();
		initJoyPos = joyStick.transform.position;
		manager = GameObject.Find("GameController").GetComponent<GameManager>();

	}

    // Update is called once per frame
    void Update()
    {
		if (manager.isGameRunning == true)
		{
			if (Input.touchCount > 0)
			{ TryToAttack(); }
		}
    }

	private void TryToAttack()
	{
		float joyRange = joyStick.MovementRange;
		if (Input.touchCount == 1)
		{
			Touch touch1 = Input.GetTouch(0);
			if ((touch1.phase == TouchPhase.Began) && (touch1.position.x > initJoyPos.x + joyRange || touch1.position.x < initJoyPos.x - joyRange ||
				touch1.position.y > initJoyPos.y + verticalJoystickTouchSafetyZoneHeight / 2 || touch1.position.y < initJoyPos.y - verticalJoystickTouchSafetyZoneHeight / 2))
			{
				if (canAttk == true)
				{
					ThrowStar(touch1);
					canAttk = false;
					Invoke("ResetCanAttk", timeBetweenShots);
				}
			}
		}

		else if (Input.touchCount > 1)
		{
			 Touch touch2 = Input.GetTouch(1);
			if ((touch2.phase == TouchPhase.Began) && (touch2.position.x > initJoyPos.x + joyRange || touch2.position.x < initJoyPos.x - joyRange ||
				touch2.position.y > initJoyPos.y + verticalJoystickTouchSafetyZoneHeight / 2 || touch2.position.y < initJoyPos.y - verticalJoystickTouchSafetyZoneHeight / 2))
			{
				if (canAttk == true)
				{
					ThrowStar(touch2);
					canAttk = false;
					Invoke("ResetCanAttk", timeBetweenShots);
				}
			}
		}
	}

	private void ThrowStar(Touch touch)
	{
		target = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
		Vector3 difference = target - player.transform.position;
		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

		float distance = difference.magnitude;
		Vector2 direction = difference / distance;
		direction.Normalize();
		GameObject star = Instantiate(bulletPrefab, starSpawn.transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
		star.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * starSpeed, direction.y * starSpeed);
	}
	private void ResetCanAttk()
	{ canAttk = true; }
}
