using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBigCamera : MonoBehaviour
{

    
    public Highwynn.Cam2DFollow camera2DFollow;
    public ChangeCameraTargets changeCameraTargets;


    public ActivatorCameraShift activatorCameraShift;





    // Start is called before the first frame update
    void Start()
    {
        activatorCameraShift = FindObjectOfType<ActivatorCameraShift>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activatorCameraShift.bigView == true)
        {
            if (other.tag == "Player")
            {                
                camera2DFollow.enabled = true;
                changeCameraTargets.enabled = false;
                Debug.Log("CHANGE CAMERA Follow Player");
                activatorCameraShift.bigView = false;
            }
        }
    }
}

