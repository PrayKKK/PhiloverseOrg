using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelScript : MonoBehaviour
{
    Vector3 pos; //현재위치

    float delta = 0.02f; // 상하로 이동가능한 (y)최대값

    float speed = 3.0f; // 이동속도
    float rotSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        float rspeed = rotSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rspeed);
    }

}
