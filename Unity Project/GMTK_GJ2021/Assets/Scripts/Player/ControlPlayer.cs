using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

	/* Class: ControlPlayer
	 * 
	 * Purpose: This class controls the following player abilities
	 *      Movement
	 *      Pickup item
	 *      Place item
	 * 
	 */

	Animator anim;
	float speedX = 1.0f, speedY = 1.0f;
	public CharacterController player;
	bool holdingDown;


    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

		//Catch movement keypresses.
		if (Input.GetKeyDown(KeyCode.W)){
			MoveUp();
			holdingDown = true;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			MoveRight();
			holdingDown = true;
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			MoveLeft();
			holdingDown = true;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			MoveDown();
			holdingDown = true;
		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			anim.SetBool("isWalking", false);
			Debug.Log("Character is idle");
		}


		if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("isWalking", false);
			Debug.Log("Character is no longer moving up");
		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			anim.SetBool("isWalking", false);
			Debug.Log("Character is idle");
		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			anim.SetBool("isWalking", false);
			Debug.Log("Character is idle");
		}

		if (Input.GetKeyUp(KeyCode.A))
		{
			anim.SetBool("isWalking", false);
			Debug.Log("Character is idle");
		}
	}

	void MoveUp()
	{
		anim.SetBool("isWalking", true);
		Debug.Log("Character is moving up");
		player.transform.Translate(new Vector3(0f, speedY * Time.deltaTime, 0));
	}

	void MoveRight()
	{
		anim.SetBool("isWalking", true);
		Debug.Log("Character is moving right");
	}

	void MoveDown()
	{
		anim.SetBool("isWalking", true);
		Debug.Log("Character is moving down");
	}

	void MoveLeft()
	{
		anim.SetBool("isWalking", true);
		Debug.Log("Character is moving left");
	}
}
