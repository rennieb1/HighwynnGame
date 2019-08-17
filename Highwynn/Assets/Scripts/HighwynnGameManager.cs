using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwynnGameManager : MonoBehaviour
{

    GameObject currentSavePoint;
    GameObject player;
    private static HighwynnGameManager _instance = null;

    static public HighwynnGameManager Instance()
    {
        return _instance;
    }

    void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this)
           {
                Debug.Log("There can be only one");
            
                Destroy(gameObject);
            }
            
        }
        else
        {
            Debug.Log("First initialisation of manager");
            _instance = this;
 //           DontDestroyOnLoad(gameObject); 
        }
        
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerToLastCheckpoint()
    {
        if (player != null && currentSavePoint != null)
        {
            Debug.Log("Resetting player to last checkpoint");
            player.transform.position = currentSavePoint.transform.position;
            player.SetActive(true);
        }
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        Debug.Log("Saving Checkpoint");
        currentSavePoint = checkpoint;
    }
}
