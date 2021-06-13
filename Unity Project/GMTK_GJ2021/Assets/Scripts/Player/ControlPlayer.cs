using System;
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
    float horizontalMove = 0f, verticalMove = 0f;
    private SpriteRenderer playerRenderer;

    private bool _isCarryingInstrument;
    private InstrumentInteraction _instrumentInteraction;

    public bool IsCarryingInstrument
    {
        get => _isCarryingInstrument;
        set
        {
            if (_isCarryingInstrument == value) return;

            anim.SetBool("hasItem", value);
            _isCarryingInstrument = value;
        }
    }


    void Start()
    {
        _instrumentInteraction = GetComponent<InstrumentInteraction>();
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWalking",
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_isCarryingInstrument)
            {
                PickUpItem();
            }
            else
            {
                PutDownItem();
            }
        }
    }

    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerRenderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerRenderer.flipX = false;
        }

        player.velocity = new Vector3(horizontalMove * speedX, verticalMove * speedY, 0f);
    }

    void PickUpItem()
    {
        Debug.Log($"PickUp on {nameof(ControlPlayer)}");
        _instrumentInteraction.PickUpInstrument();
    }

    void PutDownItem()
    {
        _instrumentInteraction.PutDownInstrument();
    }
}