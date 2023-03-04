using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public GameObject BulletPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
}
