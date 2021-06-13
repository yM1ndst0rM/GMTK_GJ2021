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
	float speedX = 250.0f, speedY = 250.0f, maxSpeed = 250f;
	private Rigidbody2D player;
	private bool hasItem;
	float horizontalMove = 0f, verticalMove = 0f;
	private SpriteRenderer playerRenderer;
	public AudioClip pickUpSound, putDownSound;
	private AudioSource playerAudioSource;
	


	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		player = GetComponent<Rigidbody2D>();
		playerRenderer = GetComponent<SpriteRenderer>();
		playerAudioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(Input.GetAxisRaw("Vertical"));

		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			anim.SetBool("isWalking", true);
			//MovePlayer();
		}
		else
			anim.SetBool("isWalking", false);

		if (Input.GetKeyDown(KeyCode.E))
		{
			if(hasItem == false)
			{
				PickUpItem();
				hasItem = true;
			}
			else
			{
				PutDownItem();
				hasItem = false;
			}
		}
	}

	private void FixedUpdate()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal");
		verticalMove = Input.GetAxisRaw("Vertical");

		if (Input.GetAxisRaw("Horizontal") == -1)
			playerRenderer.flipX = true;
		else if(Input.GetAxisRaw("Horizontal") == 1)
			playerRenderer.flipX = false;


		player.velocity = new Vector3(horizontalMove * speedX, verticalMove * speedY, 0f);
	}

	void PickUpItem()
	{
		playerAudioSource.clip = pickUpSound;
		playerAudioSource.Play();
		anim.SetBool("hasItem", true);
	}

	void PutDownItem()
	{
		playerAudioSource.clip = putDownSound;
		playerAudioSource.Play();
		anim.SetBool("hasItem", false);
	}




}
