using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;

    public Animator transition;
    public float transitionTime = 1.0f;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("test");
        	if(GameIsPaused){
        		Resume();
        	}
        	else{
        		Pause();
        	}
        }
    }

    public void Resume(){
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	GameIsPaused = false;
    }

    void Pause(){
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;
    }

    public void LoadMenu(){
    	Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        StartCoroutine(LoadLevel("Menu"));
    }

    public void QuitGame(){
    	Application.Quit();
    }

    public void LoadOptionMenu(){
    	Time.timeScale = 1f;
        StartCoroutine(LoadLevel("MenuOption"));
        //SceneManager.LoadScene("MenuOption");
    }

    IEnumerator LoadLevel(string scene)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Pause
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(scene);
    }


}
