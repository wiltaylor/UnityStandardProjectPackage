using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;
    public GameObject CloseObject;

    //private AudioController _audioController;

    void Start()
    {
        //_audioController = GlobalController.Instance.AudioController;
       // MasterSlider.value = _audioController.GetVolume(AudioController.VolumeItem.Master);
       // SFXSlider.value = _audioController.GetVolume(AudioController.VolumeItem.SFX);
       // MusicSlider.value = _audioController.GetVolume(AudioController.VolumeItem.Music);
    }

    public void OnMusicChange()
    {
       // _audioController.SetVolume(AudioController.VolumeItem.Music, MusicSlider.value);
    }

    public void OnSFXChange()
    {
       // _audioController.SetVolume(AudioController.VolumeItem.SFX, SFXSlider.value);
    }

    public void OnMasterChange()
    {
       // _audioController.SetVolume(AudioController.VolumeItem.Master, MasterSlider.value);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        CloseObject.SetActive(true);
    }
}
