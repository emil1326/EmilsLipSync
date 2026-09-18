using UnityEngine;
using UnityEngine.UI;

public class UpDownSc : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject pannel;

    // Start is called before the first frame update
    void Start()
    {
        scrollbar.onValueChanged.AddListener(ModifyHeight);
    }

    void ModifyHeight(float height)
    {
        float newheight = (height * 800) - 425;
        pannel.transform.SetLocalPositionAndRotation(new Vector3(-10, newheight, 0), Quaternion.identity);
    }
}
