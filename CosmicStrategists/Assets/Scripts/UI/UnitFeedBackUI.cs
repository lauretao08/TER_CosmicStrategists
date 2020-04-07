using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitFeedBackUI : MonoBehaviour
{
    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_description;
    public TextMeshProUGUI card_cost;
    public TextMeshProUGUI card_health;

    public RawImage card_hp_img;


    public void DisplayCardInfo(Card card)
    {
        card_name.text = card.card_name;
        card_description.text = card.card_description;
        card_cost.text = card.card_base_energy_cost.ToString();
        //optimize this getComponent !
        if (card.GetComponent(typeof(Card_Unit)))
        {
            card_health.gameObject.SetActive(true);
            card_hp_img.gameObject.SetActive(true);
            card_health.text = card.card_hp.ToString();
        }
        else
        {
            card_health.gameObject.SetActive(false);
            card_hp_img.gameObject.SetActive(false);
        }
        
    }

    public void Show(bool on)
    {
        if (on)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
