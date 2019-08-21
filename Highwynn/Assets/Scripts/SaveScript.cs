using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    [SerializeField]
    private Text saveText = null;
    private bool saveAllowed;
    //public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        saveText.gameObject.SetActive(false);
      //  Player = GameObject.FindObjectOfType<Player>();

    }
/*
    // Update is called once per frame
    void Update()
    {
        if (saveAllowed && Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
*/
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fire")
        {
            saveText.gameObject.SetActive(true);
            // Debug.Log("Fire Collided with Save");
            //    saveAllowed = true;
            Save();
          //  Destroy(gameObject);
        }
        if (collision.gameObject.name.Equals("Player"))
        {
            saveText.gameObject.SetActive(true);
            // Debug.Log("player Collided Save");
            //   saveAllowed = true;
            Save();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            saveText.gameObject.SetActive(false);
        //    saveAllowed = false;
        }
        if (collision.tag == "Fire")
        {
            saveText.gameObject.SetActive(false);
         //   saveAllowed = false;
            // Debug.Log("Fire Collidered with Save"); 
        }
    }

    private void Save()
    {
        //Destroy(gameObject);
        
        HighwynnGameManager.Instance().SetCheckpoint(gameObject);
    }
}
