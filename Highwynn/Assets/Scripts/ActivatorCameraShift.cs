using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorCameraShift : MonoBehaviour
{
    public GameObject lockArea;
    public Highwynn.Camera2DFollow camera2DFollow;
    public ChangeCameraTargets changeCameraTargets;

    public bool bigView;



    // Start is called before the first frame update
    void Start()
    {
        bigView = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (bigView == false)
        {
            if (other.tag == "Player")
            {
                lockArea.SetActive(true);
                camera2DFollow.enabled = false;
                changeCameraTargets.enabled = true;
                Debug.Log("CHANGE CAMERA Scene Lock");
                bigView = true;
            }
        }
    }
}
