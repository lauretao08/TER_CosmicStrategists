using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Scout_test : MonoBehaviour
{
    public MeshRenderer unit_renderer;
    bool appear = true;
    bool disappear = false;

    Material myMaterial;
    float appearOverTime = 1.0f;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //unit_renderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;

        //=============================================
        myMaterial = unit_renderer.material;
        myMaterial.SetFloat("Vector1_A27884FF", -2);
       // Debug.Log("M : " + myMaterial.ToString());
       // Debug.Log("Edge : " + myMaterial.GetFloat("Vector1_A27884FF"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (appear)
        {
            appearOverTime += Time.deltaTime * speed;
            Debug.Log("Edge : "+myMaterial.GetFloat("Vector1_A27884FF"));
            myMaterial.SetFloat("Vector1_A27884FF", -2+appearOverTime);
            if (appearOverTime >= 6) appear = false;
        }
    }
}
