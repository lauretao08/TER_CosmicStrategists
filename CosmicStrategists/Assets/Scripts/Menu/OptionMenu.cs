using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
	public AudioMixer audioMixer;
    public Animator transition;
    public float transitionTime = 1.0f;

    public TMP_Dropdown resolutionDropdown;
	Resolution[] resolutions;

	public Game game;


	void Start(){
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;
		for(int i = 0; i<resolutions.Length; i++){
			string option = resolutions[i].width + " x " + resolutions[i].height;

			if(!options.Contains(option))
				options.Add(option);

			if(resolutions[i].width == Screen.width &&
			   resolutions[i].height == Screen.height){
				currentResolutionIndex = i;
			}

		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			gameObject.SetActive(false);
		}
	}

	public void SetResolution(int resolutionIndex){
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		if(game !=null){
			game.rearrange_card_placement();
		}
	}


    public void SetVolume(float volume){
    	audioMixer.SetFloat("volume",Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex){
    	QualitySettings.SetQualityLevel(qualityIndex);
    	//Debug.Log("setting changed!" + qualityIndex);
    }

   	public void setFullScreen( bool isFullscreen){
   		Screen.fullScreen = isFullscreen;
   	}

   	public void LoadMenuScene(){
        StartCoroutine(LoadLevel("Menu"));
    }

   	public void LoadBackScene(){
		gameObject.SetActive(false);
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
