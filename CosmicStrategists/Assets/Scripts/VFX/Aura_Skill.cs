using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Skill : MonoBehaviour
{
    public GameObject Base;
    public GameObject Particles;
    public GameObject VerticalCut;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Base.SetActive(true);
            Particles.SetActive(true);
            VerticalCut.SetActive(false);
        }

        else
        {
            Base.SetActive(false);
            Particles.SetActive(false);
            VerticalCut.SetActive(false);
        }

    }
}
