using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamInstantiation : MonoBehaviour
{
    public GameObject smoke;
    public float[] bounds = new float[2];
    public float maxAmountOfSmokes = 5;
    public float timeBetweenSmokeSpawn = 1;
    public LayerMask platformLayer;

    float numOfSmokes = 0;
    float countDown;
    Vector3 smokeDistanceFromPlatform = Vector3.one * 2;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 1f);
        Gizmos.DrawWireCube(transform.position, new Vector3(bounds[1], 0, bounds[0]));
    }

    void Update()
    {
        if(numOfSmokes <= maxAmountOfSmokes && countDown < 0)
        {
            Vector3 potentialPos = new Vector3(Random.Range(transform.position.x-(bounds[1]/2), transform.position.x + (bounds[1]/2)),transform.position.y, Random.Range(transform.position.z - (bounds[0]/2), transform.position.z + (bounds[0]/2)));
            Collider[] hitColliders = Physics.OverlapBox(potentialPos, smokeDistanceFromPlatform, Quaternion.identity, platformLayer);
            if (hitColliders.Length == 0)
            {
                GameObject newsteam = Instantiate(smoke,potentialPos,smoke.transform.rotation);
                newsteam.transform.parent = transform;
                countDown = timeBetweenSmokeSpawn * Random.Range(0.9f,1.1f);
            }
        }

        numOfSmokes = transform.childCount;
        countDown -= Time.deltaTime;
    }
}
