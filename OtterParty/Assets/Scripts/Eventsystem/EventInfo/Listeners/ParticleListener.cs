using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.ParticleEvent, InstantiateParticles);
    }

    private void InstantiateParticles(BaseEventInfo e)
    {
        TransformEventInfo te = e as TransformEventInfo;
        if(te != null)
        {
            Vector3 objectPosition = te.objectTransformPosition;
            Quaternion objectRotation = te.objectTransformRotation;
            GameObject particleObject = te.objectParticleSystem;
            if(particleObject != null)
            {
                var particles = Instantiate(particleObject, objectPosition, objectRotation);
                var ps = particles.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(ps, ps.main.duration);
                }
            }
        }

    }
}
