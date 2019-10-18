using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicBookSkip : MonoBehaviour
{
    public Animator anim;
    private float value = 0;
    public string animationame;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && value == 0)
        {
            anim.Play(animationame, 0, 0.99f);
            value++;
        }
        else
        {

        }
    }
}
