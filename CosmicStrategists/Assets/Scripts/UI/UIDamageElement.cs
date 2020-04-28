using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageElement : MonoBehaviour
{
    // Start is called before the first frame update
    public float travel_time;
    public float travel_increment;
    public bool direction_up=true;

    private float time_alive = 0.0f;

    public Text text_element;

    void Start()
    {
        
    }

    public void setNumber(string nb)
    {
        this.text_element.text = nb;
    }


    public void SetParent(GameObject parent)
    {
        //transform.parent = parent.transform;
        transform.SetParent(parent.transform);
        transform.position = parent.transform.position;
        if (direction_up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 20.0f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 20.0f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time_alive += Time.deltaTime;

        if (direction_up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + travel_increment, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - travel_increment, transform.position.z);
        }

        if (time_alive >= travel_time)
        {
            Destroy(this.gameObject);
            Debug.Log("DELETE UI STUFF");
        }
        
    }
}
