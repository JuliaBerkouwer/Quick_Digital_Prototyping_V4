using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDetection : MonoBehaviour
{

    public ParticleSystem particleLauncher;
    List<ParticleCollisionEvent> collisionEvents;
    public float hSliderValue = 0.3f;
    public float maxSliderValue = 0.6f;
    public float addedLifeTime = 0.02f;
    private float minSlideValue = 0.0f;




    //public List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void Start()
    {
        particleLauncher = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        var main = particleLauncher.main;
        main.startLifetime = hSliderValue;
    }
    void OnParticleCollision(GameObject collision)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, collision, collisionEvents);
        SubstracktingLifetime();

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Destroy(collision.gameObject);
        }

    }

    void OnGUI()
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(0, 45, 100, 30), hSliderValue, minSlideValue, maxSliderValue);
    }

    private void SubstracktingLifetime()
    {
        if ((hSliderValue > 0.1f) && (hSliderValue < maxSliderValue))
        {
            hSliderValue = hSliderValue - addedLifeTime;
        }
        if (hSliderValue < 0.1f)
        {
            hSliderValue = 0.0f;
        }
    }

    public void AddLifetimeParticles()
    {
        if ((hSliderValue >= 0.0f) && (hSliderValue < maxSliderValue))
        {
            hSliderValue = hSliderValue + addedLifeTime;
        }
        if (hSliderValue > 0.4f)
        {
            hSliderValue = 0.5f;
        }
    }
}