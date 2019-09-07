using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBehaviour : EnemyBehavior
{
    protected override void PlayerSeen() {
        Debug.Log("Boar PlayerSeen()");
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BoarIdle") {
            anim.SetTrigger("seePlayer");
        }
        anim.SetBool("lostPlayer", false);
    }

    protected override void UpdateBehaviour() {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BoarSeePlayer") {
            anim.ResetTrigger("seePlayer");
        }
    }

    protected override void PlayerLost() {
        Debug.Log("Boar PlayerLost()");
        if (!anim.GetBool("lostPlayer")) {
            anim.SetBool("lostPlayer", true);
        }
    }

    protected override void Die() {
        Debug.Log("Boar Die()");
        Destroy(this);
    }
}
