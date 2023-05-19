using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    int a = 1;

    void Update()
    {
        if (transform.position.y < -5.0f) { a = 1; }
        else if (transform.position.y > -2.0f) { a = -1; }

        transform.Translate(Vector3.up * 3.0f *
            Time.deltaTime * a);
    }
}
