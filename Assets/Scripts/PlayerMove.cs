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
	private Vector2 rotateInput;
	private float rotationX;
	private float rotationZ;
	private float targetXRotation;
	private float targetZRotation;
	private float rotationAmountX;
	private float rotationAmountZ;

	[Header("References required")]
	[SerializeField] private GameObject cannonBarrel;

	[Header("Cannon attribute values")]
	[Range(5f, 20f)]
	[SerializeField] private float moveSpeed;
	[Range(1f, 100f)]
	[SerializeField] private float rotationSpeed;
	[Range(10f, 40f)]
	[SerializeField] private float xMovementClamp;
	[Range(10f, 90f)]
	[SerializeField] private float xRotationClamp;
	[Range(10f, 90f)]
	[SerializeField] private float zRotationClamp;

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
		CheckCannonRotate();
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
		rotateInput = cannonRotate.ReadValue<Vector2>();

		rotationX = rotateInput.y;
		rotationZ = rotateInput.x;

		//Inverting
		rotationAmountX = rotationX * rotationSpeed *Time.deltaTime;
		rotationAmountZ = -rotationZ * rotationSpeed * Time.deltaTime;

		//!!NOTE!! ->Mathf.Repeat is nothing but getting the modulo so Mathf.Repeat(5, 3) = 2 because 5 divided by 3 gives a remainder of 2<- !!NOTE!!
		// !!NOTE!! unity bounds euler angles between 0 to 360 only, does not allow to go into negatives !!NOTE!!

		//dding 180, repeating, and then subtracting 180, the result is a value that ranges from -180 to 180, allowing for negative rotations 
		float currentXRotation = Mathf.Repeat(cannonBarrel.transform.eulerAngles.x + 180f, 360f) - 180f;
		float currentZRotation = Mathf.Repeat(cannonBarrel.transform.eulerAngles.z + 180f, 360f) - 180f;

		targetXRotation = Mathf.Clamp(currentXRotation + rotationAmountX, -xRotationClamp, xRotationClamp);
		targetZRotation = Mathf.Clamp(currentZRotation + rotationAmountZ, -zRotationClamp, zRotationClamp);

		Quaternion targetRotation = Quaternion.Euler(targetXRotation, cannonBarrel.transform.eulerAngles.y, targetZRotation);
		cannonBarrel.transform.rotation = Quaternion.Slerp(cannonBarrel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	#endregion
}
