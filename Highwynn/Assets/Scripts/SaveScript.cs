using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    [SerializeField]
    private Text saveText;
    private bool saveAllowed;
    // Start is called before the first frame update
    void Start()
    {
        saveText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (saveAllowed && Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            saveText.gameObject.SetActive(true);
            saveAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            saveText.gameObject.SetActive(false);
            saveAllowed = false;
        }
    }

    private void Save()
    {
        //Destroy(gameObject);
        HighwynnGameManager.Instance().SetCheckpoint(gameObject);
    }
}
