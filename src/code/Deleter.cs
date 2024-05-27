using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public int maxAix;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (Math.Abs(position.x) > maxAix || Math.Abs(position.y) > maxAix || Math.Abs(position.z) > maxAix)
        {
            Destroy(gameObject);
        }
        
    }
}
