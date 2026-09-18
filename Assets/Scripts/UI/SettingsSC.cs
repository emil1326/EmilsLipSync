using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSC : MonoBehaviour
{
    public Button save;
    public TMP_InputField smoothing;
    public TMP_InputField gain;
    public TMP_InputField Fps;
    public Toggle hqimmages;

    private OVRLipSyncContext GainSC;
    private OVRLipSyncContextTextureFlip SmoothSC;
    private OVRLipSyncMicInput MicIn;
    private float newgain;
    private int fpstoget;
    // Start is called before the first frame update
    void Start()
    {
        save.onClick.AddListener(SaveAndApply);

        //load data
        MicIn = FindAnyObjectByType<OVRLipSyncMicInput>();

        smoothing.text = PlayerPrefs.GetInt("Smooth").ToString();
        gain.text = PlayerPrefs.GetFloat("Gain").ToString();
        MicIn.selectedDevice = PlayerPrefs.GetString("MicDevice");

        fpstoget = PlayerPrefs.GetInt("FpsTarget");
        Fps.text = fpstoget.ToString();

        if (PlayerPrefs.GetInt("IsHQ") == 0)
        {
            hqimmages.isOn = false;
        }
        else
        {
            hqimmages.isOn = true;
        }

        SaveAndApply();
    }

    public void SaveAndApply()
    {
        if (float.Parse(gain.text) < 0 || float.Parse(gain.text) > 4)
        {
            newgain = Mathf.Clamp(float.Parse(gain.text), 0f, 4f);
        }
        else
        {
            newgain = float.Parse(gain.text);
        }

        GainSC = FindAnyObjectByType<OVRLipSyncContext>();
        SmoothSC = FindAnyObjectByType<OVRLipSyncContextTextureFlip>();
        MicIn = FindAnyObjectByType<OVRLipSyncMicInput>();

        if (MicIn.selectedDevice != PlayerPrefs.GetString("MicDevice"))
        {
            PlayerPrefs.SetString("MicDevice", MicIn.selectedDevice);
        }

        MicIn.selectedDevice = PlayerPrefs.GetString("MicDevice");

        PlayerPrefs.SetFloat("Gain", newgain);
        PlayerPrefs.SetInt("Smooth", int.Parse(smoothing.text));

        GainSC.gain = newgain;
        SmoothSC.smoothAmount = int.Parse(smoothing.text);

        fpstoget = int.Parse(Fps.text);
        Application.targetFrameRate = fpstoget;
        PlayerPrefs.SetInt("FpsTarget", fpstoget);

        if (hqimmages.isOn)
        {
            PlayerPrefs.SetInt("IsHQ", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsHQ", 0);
        }
    }
}
