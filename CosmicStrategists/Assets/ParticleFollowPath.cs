using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{
    public string pathName = "ArtilleryStrikePath01";
    public float time = 5;
    public bool active = false;


    public GameObject beamStrike;

    public GameObject smoke_small;
    public GameObject smoke_big;
    public GameObject fragment;
    public GameObject sphereParticles;
    public GameObject beamLight;


    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easetype", iTween.EaseType.easeInOutSine, "time",time));

    }

    private void Update()
    {
        if(iTween.Count(gameObject) > 0)
        {
            active = true;
        }


        if (active)
        {
            smoke_small.SetActive(true);
            smoke_big.SetActive(true);
            fragment.SetActive(true);
            sphereParticles.SetActive(true);
            beamLight.SetActive(true);
        }

        if(iTween.Count(gameObject) <= 0)
        {
            iTween.Stop();
            beamStrike.SetActive(false);
        }
    }


}
