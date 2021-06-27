using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    float timeLimit = 5;

    // Update is called once per frame
    void Update()
    {
        if(timeLimit < 0)
        {
            Destroy(gameObject);
        }
        timeLimit -= Time.deltaTime;
    }
}
