using UnityEngine.Audio;

namespace Assets.Scripts.Audio
{
    public class VolumeController
    {
        private readonly AudioMixer _mixer;

        public VolumeController(AudioMixer mixer)
        {
            _mixer = mixer;
        }

        public float Master
        {
            get
            {
                var ret = default(float);
                _mixer.GetFloat("MasterVol", out ret);
                return ret;

            }
            set { _mixer.SetFloat("MasterVol", value); }
        }

        public float Music
        {
            get
            {
                var ret = default(float);
                _mixer.GetFloat("MusicVol", out ret);
                return ret;

            }
            set { _mixer.SetFloat("MusicVol", value); }
        }
        public float SFX
        {
            get
            {
                var ret = default(float);
                _mixer.GetFloat("SFXVol", out ret);
                return ret;

            }
            set { _mixer.SetFloat("SFXVol", value); }
        }
        
    }
}
