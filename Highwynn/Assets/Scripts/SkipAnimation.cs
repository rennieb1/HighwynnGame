using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipAnimation : MonoBehaviour
{
    public Animator anim;
    private float value = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && value == 0)
        {
            anim.Play("FinalPhase", 0, 1);
            value++;
        }
        else
        {

        }
    }
}
