using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public float[] bounds = new float[2];

    public LayerMask platformLayer;
    public LayerMask enemyLayer;
    public LayerMask PlayerLayer;

    Vector3 spawnAccuracy = new Vector3(0.001f, 0.001f, 0.001f);
    Vector3 closenessToOthers = new Vector3(1f, 1f, 1f);
    float amountAbovePlatform = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireCube(transform.position, new Vector3(bounds[1], 0, bounds[0]));
    }

    public void SpawnEnemies(int num = 1)
    {
        for (int i = 0; i < num;)
        {
            //Find Viable Position
            Vector3 potentialPos = new Vector3(Random.Range(transform.position.x - (bounds[1] / 2), transform.position.x + (bounds[1] / 2)), transform.position.y, Random.Range(transform.position.z - (bounds[0] / 2), transform.position.z + (bounds[0] / 2)));
            Collider[] PlatformsColliders = Physics.OverlapBox(potentialPos, spawnAccuracy, Quaternion.identity, platformLayer);
            Collider[] EnemyColliders = Physics.OverlapBox(potentialPos, closenessToOthers, Quaternion.identity, enemyLayer);
            Collider[] PlayerColliders = Physics.OverlapBox(potentialPos, closenessToOthers, Quaternion.identity, enemyLayer);
            if(PlatformsColliders.Length > 0 && EnemyColliders.Length == 0 && PlayerColliders.Length == 0)
            {
                //Instantiate Prefab
                GameObject Clone = Instantiate(Enemy, new Vector3(potentialPos.x,potentialPos.y+amountAbovePlatform,potentialPos.z), Enemy.transform.rotation);
                i++;
            }
        }
    }
}
