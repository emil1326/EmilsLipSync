using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsSC : MonoBehaviour
{
    public GameObject[] disable;
    public GameObject[] enable;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(Buttonhasbeenclicked);
    }


    void Buttonhasbeenclicked()
    {
        foreach (GameObject todisable in disable)
        {
            todisable.SetActive(false);
        }

        foreach (GameObject toenable in enable)
        {
            toenable.SetActive(true);
        }
    }
}
