using System.Collections.Generic;
using UnityEngine;

public class ModelSC : MonoBehaviour
{
    public GameObject background;
    public GameObject face;
    public GameObject mouth;
    public GameObject body;

    public string _____________________________________ = "_____________________________________";

    public bool faceshake;
    public bool bodyshake;
    public float camerazoom;
    public float backgroundzoom;
    public float avatarzoom;
    public float faceshakeammount;
    public float bodyshakeammount;
    public string avatarname;
    public float blinkduration;
    public float blinkinterval;
    public bool randomblink;

    private BlinkSC blinksc;
    private ShakeSC shakesc;

    // Start is called before the first frame update
    void Start()
    {
        blinksc = FindAnyObjectByType<BlinkSC>();
        shakesc = FindAnyObjectByType<ShakeSC>();
    }

    public void LoadData(List<string> data)
    {
        faceshake = bool.Parse(data[1]);
        bodyshake = bool.Parse(data[2]);
        camerazoom = float.Parse(data[3]);
        backgroundzoom = float.Parse(data[4]);
        avatarzoom = float.Parse(data[5]);
        faceshakeammount = float.Parse(data[6]);
        bodyshakeammount = float.Parse(data[7]);
        avatarname = data[8];
        blinkduration = float.Parse(data[9]);
        blinkinterval = float.Parse(data[10]);
        randomblink = bool.Parse(data[11]);

        blinksc.ReceivedData(blinkinterval, blinkduration, randomblink);
        shakesc.GetVars(faceshake, bodyshake, faceshakeammount, bodyshakeammount);
        Movestuff();
    }

    void Movestuff()
    {
        Camera.main.orthographicSize = camerazoom * 5;
        background.transform.localScale = new Vector3(backgroundzoom, backgroundzoom, backgroundzoom);
        // avatar ==> mouth face body
        mouth.transform.localScale = new Vector3(avatarzoom, avatarzoom, avatarzoom);    
        face.transform.localScale = new Vector3(avatarzoom, avatarzoom, avatarzoom);    
        body.transform.localScale = new Vector3(avatarzoom, avatarzoom, avatarzoom);    
    }
}
