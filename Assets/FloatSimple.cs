using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSimple : MonoBehaviour
{
    Vector3 pos;
    float staggerTime;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        staggerTime = Random.Range(-30.0f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        pos.y = -1 + Mathf.Sin((Time.frameCount + staggerTime) / 35.0f) / 50;
        transform.position = pos;

    }
}
