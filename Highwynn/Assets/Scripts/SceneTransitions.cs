using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
public Animator transitionAnim;
public string sceneName;
public Player player;


void Start()
{
     player = GameObject.FindObjectOfType<Player>();
}


void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine (LoadScene());
        }
    }

    IEnumerator LoadScene ()
    {
        transitionAnim.SetTrigger("transitionexit");
        yield return new WaitForSeconds (1.5f);
        player.SavePlayer();
        
        SceneManager.LoadScene(sceneName);

    }
}
