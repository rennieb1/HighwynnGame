using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLoader : MonoBehaviour
{
public string sceneName;

    void Start()
    {
        StartCoroutine (LoadScene());    
    }

    IEnumerator LoadScene ()
    {       
        yield return new WaitForSeconds (1.5f);
        SceneManager.LoadScene(sceneName);
    }
}