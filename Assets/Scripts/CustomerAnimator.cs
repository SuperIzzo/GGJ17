﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    private static string ANIM_SPEED = "Speed";
    private static string ANIM_PICKUP = "Pickup";
    private static string ANIM_LEAVE = "Leave";
    private static string ANIM_HIT = "Hit";
    private static string ANIM_SWIPE = "Swipe";
    private static string ANIM_HOLDING = "Holding";
    private static string ANIM_IDLE_BLEND = "Idle Blend";

    Animator animator;
    CustomerAI customer;
    IPawn pawn;
    float idleBlend;


    void Start()
    {
        pawn = GetComponentInParent<IPawn>();        
        animator = GetComponent<Animator>();

        pawn.onPickupItem += OnPickupItem;
        pawn.onLeaveItem += OnLeaveItem;
        pawn.onHit += OnHit;

        StartCoroutine( ChangeIdleBlend() );
    }

    private void OnHit()
    {
        animator.SetTrigger( ANIM_HIT );
    }

    private void OnLeaveItem()
    {
        animator.SetTrigger( ANIM_LEAVE );
    }

    private void OnPickupItem()
    {
        animator.SetTrigger( ANIM_PICKUP );
    }    

    void Update()
    {
        if( Constants.bGameOver )
        {
            return;
        }

        animator.SetFloat( ANIM_SPEED, pawn.speed );
        animator.SetBool( ANIM_HOLDING, pawn.isHoldingItem );
        animator.SetBool( ANIM_SWIPE, pawn.isSwippingItems );
    }

    private IEnumerator ChangeIdleBlend()
    {
        while( true )
        {
            idleBlend = Random.value;
            animator.SetFloat( ANIM_IDLE_BLEND, idleBlend );
            yield return new WaitForSeconds( Random.Range( 1, 10 ) );            
        }
    }
}
