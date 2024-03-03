using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction cannonMove;
	private InputAction cannonRotate;

	private Vector2 moveInput;
	private float moveX;
	private float newXPosition;
	private float moveAmount;

	[Header("References required")]
	[SerializeField] private GameObject cannonBarrel;

	[Header("Cannon attribute values")]
	[Range(5f, 20f)]
	[SerializeField] private float moveSpeed;
	[Range(1f, 20f)]
	[SerializeField] private float rotationSpeed;
	[Range(10f, 40f)]
	[SerializeField] private float xMovementClamp;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		if(playerInput != null )
		{
			cannonMove = playerInput.actions["CannonMove"];
			cannonRotate = playerInput.actions["CannonRotate"];
		}
	}

	private void Update()
	{
		CheckCannonMove();
		//CheckCannonRotate();
	}

	#region CannonMove

	private void CheckCannonMove()
	{
		if(cannonMove != null && cannonMove.IsPressed()) 
		{
			MoveCannon();
		}
	}
	private void MoveCannon()
	{
		moveInput = cannonMove.ReadValue<Vector2>();

		moveX = moveInput.x;
		moveAmount = moveX * moveSpeed * Time.deltaTime;

		//this.gameObject.transform.Translate(Vector3.right * moveAmount);

		newXPosition = Mathf.Clamp(transform.position.x + moveAmount, -xMovementClamp, xMovementClamp);
		transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
	}

	#endregion

	#region CannonRotate

	private void CheckCannonRotate()
	{
		if (cannonRotate != null && cannonRotate.IsPressed())
		{
			RotateCannon();
		}
	}
	private void RotateCannon()
	{

	}

	#endregion
}
