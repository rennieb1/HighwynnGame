using UnityEngine;

public class PauseMenu : MonoBehaviour
{

public GameObject gamePauseMenu;


    public static bool gameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()

    {
        gamePauseMenu.SetActive(false);
        Time.timeScale = 1;     
        gameIsPaused = false;
     
    }

    public void Pause()
    {
        gamePauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
      
    }

}
