using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_ArtilleryStrike : MonoBehaviour
{

    public bool active = false;
    public GameObject particleSysteme;
    public GameObject explosion;

    private void Start()
    {
        particleSysteme.SetActive(true);
        explosion.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            particleSysteme.SetActive(true);
            explosion.SetActive(true);
        }
        else
        {
            particleSysteme.SetActive(false);
            explosion.SetActive(false);
        }
    }
}
