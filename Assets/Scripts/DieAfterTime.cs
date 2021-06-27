using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    float timeLimit = 1;

    // Update is called once per frame
    void Update()
    {
        if(timeLimit < 0)
        {
            Destroy(this);
        }
        timeLimit -= Time.deltaTime;
    }
}
