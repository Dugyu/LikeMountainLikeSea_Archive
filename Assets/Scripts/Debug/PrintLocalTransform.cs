using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLocalTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       float x =  transform.localPosition.x;
        float xx = transform.position.x;

        //Debug.Log(x);
        Debug.Log(xx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
