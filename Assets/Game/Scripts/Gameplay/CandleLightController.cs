using UnityEngine;

namespace Gameplay 
{
    public class CandleLightController : MonoBehaviour
    {
        [SerializeField] Light _fireLight;
        [SerializeField] private float flickerStrength = .7f;
        [SerializeField] private float flickerSpeed = 2f;

        private float _baseIntensity;

        void Start()
        {
            _baseIntensity = _fireLight.intensity;
        }

        // Update is called once per frame
        void Update()
        {
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
            _fireLight.intensity = _baseIntensity + (noise - 0.5f) * flickerStrength * 2f;
        }
    }
}