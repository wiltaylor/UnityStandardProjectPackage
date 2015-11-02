using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{

    public float VisableTime = 5f;
    public GameObject NextObject;

    void Update()
    {
        VisableTime -= Time.deltaTime;

        if (!(VisableTime <= 0f)) return;

        Destroy(gameObject);
        NextObject.SetActive(true);
    }

    public void OnClick()
    {
        Destroy(gameObject);
        NextObject.SetActive(true);
    }
}
