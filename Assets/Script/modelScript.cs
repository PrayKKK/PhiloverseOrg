using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelScript : MonoBehaviour
{
    Vector3 pos; //������ġ

    float delta = 0.02f; // ���Ϸ� �̵������� (y)�ִ밪

    float speed = 3.0f; // �̵��ӵ�
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
