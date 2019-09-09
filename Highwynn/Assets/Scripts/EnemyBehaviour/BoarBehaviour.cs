﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBehaviour : EnemyBehavior
{
    protected override void PlayerSeen() {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BoarIdle") {
            anim.SetBool("seePlayer", true);
            anim.ResetTrigger("idleWalk");
        }
        anim.SetBool("lostPlayer", false);
    }

    protected override void UpdateBehaviour() {
        AnimatorClipInfo currentAnim = anim.GetCurrentAnimatorClipInfo(0)[0];

        switch(currentAnim.clip.name) {
            case "BoarSeePlayer":
                anim.SetBool("seePlayer", false);
                break;

            case "Walk":
                Move(10.0f);
                break;

            case "BoarRun":
                Move(20.0f);
                break;

            case "RunContinue":
                Move(40.0f);
                break;
        }

        if (distanceToTurnaround < 10.0f) {
            stopped = true;

            if (currentAnim.clip.name == "Walk" || 
                currentAnim.clip.name == "BoarIdle") 
            {
                movingRight = !movingRight;
                Flip();
                stopped = false;
            }
            else if (currentAnim.clip.name == "BoarRun" ||
                    currentAnim.clip.name == "RunContinue") 
            {
                Stop();
            }

            distanceToTurnaround = 100.0f;
        }
    }

    protected override void PlayerLost() {
        if (!anim.GetBool("lostPlayer")) {
            anim.SetBool("lostPlayer", true);
        }
    }

    protected override void Die() {
        Destroy(gameObject);
    }

    public override void Stop() {
        anim.Play("BoarStopping");
    }

    // Animation Event
    void EndIdle() {
        if (!anim.GetBool("seePlayer")) {
            DecideDirection();
            DecideMove();
        }
    }

    private void DecideDirection() {
        movingRight = (Random.value >= 0.5f);

        Flip();
    }

    private void DecideMove() {
        bool move = (Random.value >= 0.5f);
        if (move) {
            anim.SetBool("idleWalk", true);
            anim.SetBool("walkIdle", false);
        }
        else {
            anim.SetBool("walkIdle", true);
            anim.SetBool("idleWalk", false);
        }
    }

    void OnColliderEnter2D(Collider2D other) {
        if (other.gameObject.tag == "EnemyTurnaround") {
            Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision!");
        Debug.Log(other.gameObject.name);
    }

    void OnStopped() {
        if (stopped) {
            movingRight = !movingRight;
            Flip();
        }

        stopped = false;
    }
}