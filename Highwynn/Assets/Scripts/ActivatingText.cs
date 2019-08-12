using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatingText : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.gameObject.SetActive(false);
            //    saveAllowed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.gameObject.SetActive(true);
        }
    }
}
