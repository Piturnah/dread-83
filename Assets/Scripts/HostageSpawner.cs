using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageSpawner : MonoBehaviour
{
    public float[] bounds = new float[2];
    public GameObject hostage;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,0,1, 1f);
        Gizmos.DrawWireCube(transform.position, new Vector3(bounds[1],0,bounds[0]));
    }

    public void spawnHostage(int num = 1)
    {
        float instantiationDistance = bounds[1] / (num+1);
        for(int i = 0; i < num; i++)
        {
            Instantiate(hostage, new Vector3(transform.position.x - (bounds[1]/2) + (instantiationDistance * (i + 1)), transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}