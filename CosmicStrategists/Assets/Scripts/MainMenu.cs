using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nouveau
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1.0f;

    public void PlayGame(){
        
        LoadNextLevel();
    }

    public void QuitGame(){
    	Debug.Log("Quit!");
    	Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Pause
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }



}
