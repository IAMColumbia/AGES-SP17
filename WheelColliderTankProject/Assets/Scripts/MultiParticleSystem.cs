using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Effects
{
    public class MultiParticleSystem : MonoBehaviour
    {
        public float duration = 0;

        ParticleSystem[] systems;

        private void Start()
        {
            systems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in systems)
            {
                duration = Mathf.Max(duration, system.duration);
                system.Clear();
                system.Stop();
            }
        }

        public void Play()
        {
            foreach (ParticleSystem system in systems)
            {
                system.Play();
            }
        }

        public void Stop()
        {
            foreach (ParticleSystem system in systems)
            {
                duration = Mathf.Max(duration, system.duration);
                system.Stop();
            }
        }
    }
}