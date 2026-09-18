using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseRemeinder : MonoBehaviour
{
    public Toggle check;

    void Start()
    {
        try
        {
            PlayerPrefs.GetInt("showfirstaid");
        }
        catch 
        {
            PlayerPrefs.SetInt("showfirstaid", 0);
        }
           

        if (PlayerPrefs.GetInt("showfirstaid") == 0)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(CloseIt);            
        }
        else
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    void CloseIt()
    {
        if (check.isOn)
        {
            PlayerPrefs.SetInt("showfirstaid", 1);
        }
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
