using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Boom;



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
            Boom.SetActive(true);
            
        }

        else
        {
            Boom.SetActive(false);
        }

    }
}
