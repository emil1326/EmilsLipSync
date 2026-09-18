using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeSC : MonoBehaviour
{
    public GameObject face;
    public GameObject body;

    public bool FsOn;
    public bool BsOn;
    public float FsAmnt;
    public float BsAmnt;

    private OVRLipSyncMicInput micInput;

    private void Start()
    {
        micInput = FindAnyObjectByType<OVRLipSyncMicInput>();
    }

    public void GetVars(bool faceshake, bool bodyshake, float faceshakeammount, float bodyshakeammount)
    {
        FsOn = faceshake;
        BsOn = bodyshake;
        FsAmnt = faceshakeammount;
        BsAmnt = bodyshakeammount;
    }

    void Update()
    {
        float currVolume = micInput.GetAveragedVolume();

        if (FsOn)
        {
            ShakeObject(face, FsAmnt * currVolume);
        }

        if (BsOn)
        {
            ShakeObject(body, BsAmnt * currVolume);
        }
    }

    void ShakeObject(GameObject gameObject, float shakeAmount)
    {
        Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
        gameObject.transform.localPosition = shakeOffset + new Vector3(0,0.05f,0);
    }

}
