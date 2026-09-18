using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BlinkSC : MonoBehaviour
{
    public float interval = 0;
    public float speed = 0;
    public bool randomblink;

    public Material[] mats;

    private void Start()
    {
        StartCoroutine(Blinking());
    }

    public void ReceivedData(float between, float time, bool randomblinke)
    {
        interval = between;
        speed = time;
        randomblink = randomblinke;
    }

    private IEnumerator Blinking()
    {
        if (randomblink)
        {
            float randomPercentage = Random.Range(0.02f, 0.2f);
            float randomDelay = interval * (1f + randomPercentage);
            float randomDelaySpeed = speed * (1f + randomPercentage);

            this.gameObject.GetComponent<MeshRenderer>().material = mats[1];
            yield return new WaitForSeconds(randomDelay);

            this.gameObject.GetComponent<MeshRenderer>().material = mats[0];
            yield return new WaitForSeconds(randomDelaySpeed);
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mats[1];
            yield return new WaitForSeconds(interval);

            this.gameObject.GetComponent<MeshRenderer>().material = mats[0];
            yield return new WaitForSeconds(speed);
        }

        StartCoroutine(Blinking());
    }
}
