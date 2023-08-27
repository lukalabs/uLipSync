using UnityEngine;

namespace uLipSync
{

    [RequireComponent(typeof(AudioSource))]
    public class uLipSyncAudioSource : MonoBehaviour
    {
        public AudioFilterReadEvent onAudioFilterRead { get; private set; } = new AudioFilterReadEvent();

        [Range(0f, 1f)]
        [SerializeField] private float volume = 1f;
        public float Volume { get => volume; set => volume = value; }

        public void OnAudioFilterRead(float[] input, int channels)
        {
            onAudioFilterRead.Invoke(input, channels);
            if (volume == 1) return;
            for (int i = 0; i < input.Length; i++) input[i] = input[i] * volume;
        }
    }

}
