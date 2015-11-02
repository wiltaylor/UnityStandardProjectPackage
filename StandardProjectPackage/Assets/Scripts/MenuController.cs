using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

    public GameObject HowToPlayObject;
    public GameObject OptionsObject;
    public GameObject ExitConfirmationObject;
    public GameObject ExitButton;

    public string NewGameLevel = "";
    public bool HideExitInEditor = false;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WebGLPlayer ||
            Application.platform == RuntimePlatform.WindowsWebPlayer || (HideExitInEditor && Application.isEditor))
        {
            ExitButton.SetActive(false);
        }
    }

    public void OnNewGame()
    {

        if (ExitConfirmationObject.activeInHierarchy)
            return;

        Application.LoadLevel(NewGameLevel);
    }

    public void OnHowToPlay()
    {
        if (ExitConfirmationObject.activeInHierarchy)
            return;


        HowToPlayObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnOptions()
    {
        if (ExitConfirmationObject.activeInHierarchy)
            return;

        OptionsObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnYesExit()
    {
        Application.Quit();
    }

    public void OnNoExit()
    {
        ExitConfirmationObject.SetActive(false);
    }

    public void OnExit()
    {
        ExitConfirmationObject.SetActive(true);
    }

    public void OnCredits()
    {
        Application.LoadLevel("Credits");
    }
}
