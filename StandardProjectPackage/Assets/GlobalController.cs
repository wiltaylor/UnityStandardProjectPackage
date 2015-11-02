using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance { private set; get;}

    [HideInInspector]
    public AudioController AudioController;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        AudioController = GetSubItem<AudioController>();
    }

    T GetSubItem<T>()
    {
        var ret = GetComponent<T>();

        if (ret == null)
        {
            ret = GetComponentInChildren<T>();
        }

        return ret;
    }
}
