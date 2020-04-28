using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricHit : MonoBehaviour
{
    public GameObject BeamOfLight;
    public GameObject HitEffect;
    public GameObject Particles;
    public GameObject Sphere;
    public GameObject SpericalElectricity1;
    public GameObject SpericalElectricity2;


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
            BeamOfLight.SetActive(true);
            HitEffect.SetActive(true);
            Particles.SetActive(true);
            Sphere.SetActive(true);
            SpericalElectricity1.SetActive(true);
            SpericalElectricity2.SetActive(true);
        }

        else
        {
            BeamOfLight.SetActive(false);
            HitEffect.SetActive(false);
            Particles.SetActive(false);
            Sphere.SetActive(false);
            SpericalElectricity1.SetActive(false);
            SpericalElectricity2.SetActive(false);
        }

    }
}
