using UnityEngine;
using UnityEngine.Events;

public class GrenadeTrigger : MonoBehaviour
{
    public int timer = 3;
    private bool boom = false;
    float countdown;
    private bool alreadyExploded = false;
    public GameObject _particleSystem;
    protected GameObject Particle_System;
    public float timeToLive = 10f;

    public UnityEvent onExplode;
    public UnityEvent onEffectGone;

    private void Start()
    {
        countdown = timer;
    }
    private void Update()
    {
        if (transform.childCount < 3)
            boom = true;
        if (boom)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !alreadyExploded)
            {
                Explode();
                alreadyExploded = true;
            }
        }
    }

    public virtual void DestroyGrenade()
    {
        Destroy(Particle_System, timeToLive);
        Destroy(gameObject, timeToLive);
    }

    private void OnDestroy()
    {
        Debug.Log("DestroyGrenade");
        onEffectGone.Invoke();
    }

    protected virtual void Explode()
    {
        Particle_System = Instantiate(_particleSystem, transform);
        Debug.Log("Explode");
        onExplode.Invoke();
    }
}
