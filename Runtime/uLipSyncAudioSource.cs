using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace uLipSync
{

    [RequireComponent(typeof(AudioSource))]
    public class uLipSyncAudioSource : MonoBehaviour
    {
        //public AudioFilterReadEvent onAudioFilterRead { get; private set; } = new AudioFilterReadEvent();
        public UnityEvent<float[], int> audioFilterRead = new();

        [Range(0f, 1f)]
        [SerializeField] private float volume = 1f;
        public float Volume { get => volume; set => volume = value; }

        private Queue<float[]> queue = new();
        private int channels;

        public void OnAudioFilterRead(float[] input, int channels)
        {
            this.channels = channels;
            queue.Enqueue(input);
            if (volume == 1) return;
            for (int i = 0; i < input.Length; i++) input[i] = input[i] * volume;
        }

        private void Update()
        {
            if (queue.Count == 0) return;
            var chunk = new List<float>(queue.Dequeue());
            while (queue.Count > 0)
                chunk.AddRange(queue.Dequeue());
            audioFilterRead.Invoke(chunk.ToArray(), channels);
        }
    }

}
