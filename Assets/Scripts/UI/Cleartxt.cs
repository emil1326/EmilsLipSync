using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cleartxt : MonoBehaviour
{
    public TextMeshProUGUI toclear;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(Clear);
    }

    private void Update()
    {
        if (toclear.text == string.Empty)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
        else
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "X";
        }
    }

    void Clear()
    {
        toclear.text = string.Empty;
    }
}
