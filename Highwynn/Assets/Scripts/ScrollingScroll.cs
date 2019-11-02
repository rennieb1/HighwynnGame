using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingScroll : MonoBehaviour
{
    public GameObject scrollBar; 
    public Image scrollBarImage;
    public float barload = 0.1f;
    public float startvalue = 0;
    public GameObject WispImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startvalue == 1)
        {
            scrollBarImage.fillAmount = barload;

            barload += Time.deltaTime / 2;
            if (barload >= 1)
            {
                barload = 1;
            }
        }
        else
        {

        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("enter");
            scrollBar.SetActive(true);
            startvalue = 1;
            WispImage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scrollBar.SetActive(false);
            startvalue = 0;
            barload = 0.1f;
            WispImage.SetActive(false);
        }
    }
}
