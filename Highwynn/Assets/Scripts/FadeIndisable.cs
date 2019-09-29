using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeIndisable : MonoBehaviour
{
    public GameObject fadeInPanel;

     
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    IEnumerator LoadScene()
    {       
        yield return new WaitForSeconds(2f);
        fadeInPanel.SetActive(false);

    }
}
