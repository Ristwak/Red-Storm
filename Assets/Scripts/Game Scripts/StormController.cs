using UnityEngine;

public class StormController : MonoBehaviour
{
    [Header("Storm Settings")]
    public ParticleSystem dustParticles;
    public AudioSource windAudio;
    public float fogStartDensity = 0f;
    public float fogMaxDensity = 0.05f;
    public float stormBuildTime = 60f;

    private float stormTimer = 0f;
    private bool stormActive = false;
    private bool stormEnding = false;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = fogStartDensity;
    }

    public void BeginStorm()
    {
        stormActive = true;
        stormTimer = 0f;
    }

    public void EndStorm()
    {
        stormEnding = true;
    }

    void Update()
    {
        if (stormActive && !stormEnding)
        {
            stormTimer += Time.deltaTime;
            float t = stormTimer / stormBuildTime;

            RenderSettings.fogDensity = Mathf.Lerp(fogStartDensity, fogMaxDensity, t);
            if (windAudio) windAudio.volume = Mathf.Lerp(0.2f, 1f, t);

            var emission = dustParticles.emission;
            emission.rateOverTime = Mathf.Lerp(10, 200, t);
        }
        else if (stormEnding)
        {
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, fogStartDensity, Time.deltaTime);
            if (windAudio) windAudio.volume = Mathf.Lerp(windAudio.volume, 0f, Time.deltaTime);
        }
    }
}
