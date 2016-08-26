using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.Scripts.Music
{
    public class MusicPlayer : ITickable, IInitializable
    {
        private readonly AudioMixer _mixer;
        private readonly AudioSource _musicSource;
        private readonly MusicPlaylist[] _playlists;
        private MusicPlaylist _currentPlaylist;
        private AudioClip _currentTrack;
        private int _trackindex;
        private int _number = Random.Range(0, 5);

        public MusicPlayer(AudioMixer mixer, AudioSource source, MusicPlaylist[] playlists)
        {
            _mixer = mixer;
            _musicSource = source;
            _playlists = playlists;
        }

        public void Tick()
        {
            Debug.Log("Tocking - " + _number.ToString());

            if (_musicSource.isPlaying)
                return;
            if (_currentTrack == null)
                return;

            NextTrack();
        }

        public void StartPlayList(string name)
        {
            _currentPlaylist = _playlists.FirstOrDefault(p => p.Name == name);

            if (_currentPlaylist == null || _currentPlaylist.Tracks.Length == 0)
            {
                _musicSource.Stop();
                _currentTrack = null;
                return;
            }
            else
            {
                SelectTrack(0);
            }
        }

        private void SelectTrack(int index)
        {
            if (_currentPlaylist.Tracks.Length == 0)
                return;

            if (_trackindex > _currentPlaylist.Tracks.Length)
                _trackindex = 0;

            _currentTrack = _currentPlaylist.Tracks[_trackindex];
            if(_musicSource.isPlaying)
                _musicSource.Stop();

            _musicSource.clip = _currentTrack;
            _musicSource.Play();
        }

        private void NextTrack()
        {
            _trackindex++;

            SelectTrack(_trackindex);
        }

        public void Initialize()
        {
            Debug.Log("Init - " + _number.ToString());
            StartPlayList("Main");
        }
    }
}
