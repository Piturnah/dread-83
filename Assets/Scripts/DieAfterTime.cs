using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    public float timeLimit = 5;
    public bool ParticleSystem = false;
    public float ParticleLenienceTime = 5;

    // Update is called once per frame
    void Update()
    {
        if(timeLimit < 0)
        {           
            if (ParticleSystem)
            {
                GetComponent<ParticleSystem>().Stop();
                if(timeLimit < 0 - ParticleLenienceTime)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        timeLimit -= Time.deltaTime;
    }
}
