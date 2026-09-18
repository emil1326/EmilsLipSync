using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public GameObject toopen;
    public GameObject toopen2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            toopen.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            toopen2.SetActive(true);
        }
    }
}
