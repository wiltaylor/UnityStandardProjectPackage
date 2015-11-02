using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource MusicSource;
    public string StartPlayList = "Main";
    private MusicPlaylist[] _playlists;
    private MusicPlaylist _currentPlayList;
    private AudioClip _currentTrack;
    void Start()
    {
        _playlists = GetComponents<MusicPlaylist>();
        SelectPlaylist(StartPlayList);
    }

    public void SelectPlaylist(string trackName)
    {
        _currentPlayList = _playlists.FirstOrDefault(p => p.Name == trackName);

        if (_currentPlayList == null)
        {
            MusicSource.Stop();
            _currentTrack = null;
            Debug.Log("Playlist " + trackName + " does not exist!");
            return;
        }

        if (_currentTrack != null)
        {
            if (!_currentPlayList.IsTrackInList(_currentTrack.name))
            {
                MusicSource.Stop();
                _currentTrack = _currentPlayList.GetNextTrack();
                MusicSource.clip = _currentTrack;
                MusicSource.Play();
            }
        }
    }

    void Update()
    {
        if (MusicSource.isPlaying) return;
        if (_currentPlayList == null) return;

        _currentTrack = _currentPlayList.GetNextTrack();
        MusicSource.clip = _currentTrack;
        MusicSource.Play();
    }

    public enum VolumeItem
    {
        Master,
        Music,
        SFX
    }

    public void SetVolume(VolumeItem item, float value)
    {
        switch (item)
        {
            case VolumeItem.Master:
                Mixer.SetFloat("MasterVol", value);
                break;
            case VolumeItem.Music:
                Mixer.SetFloat("MusicVol", value);
                break;
            case VolumeItem.SFX:
                Mixer.SetFloat("SFXVol", value);
                break;
        }
    }

    public float GetVolume(VolumeItem item)
    {
        var vol = 0f;

        switch (item)
        {
            case VolumeItem.Master:
                Mixer.GetFloat("MasterVol", out vol);
                break;
            case VolumeItem.Music:
                Mixer.GetFloat("MusicVol", out vol);
                break;
            case VolumeItem.SFX:
                Mixer.GetFloat("SFXVol", out vol);
                break;
        }

        return vol;
    }
}
