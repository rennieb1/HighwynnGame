using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
public Animator transitionAnim;
public string sceneName;
public Player player;
    public GameObject fadeOutPanel;


void Start()
{
     player = GameObject.FindObjectOfType<Player>();
}


void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine (LoadScene());
            fadeOutPanel.SetActive(true);
        }
    }

    IEnumerator LoadScene ()
    {

        fadeOutPanel.SetActive(true);
        yield return new WaitForSeconds (1.5f);
        player.SavePlayer();
        
        SceneManager.LoadScene(sceneName);

    }
}
