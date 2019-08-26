using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFlower : MonoBehaviour
{
    public Transform camTrans;
    Vector3 lastStablePosition;
    Vector3 currentStablePosition;
    float accumulativeDist;
    public GameObject cloverTemplate;
    GameObject clover;
   

    // Start is called before the first frame update
    void Start()
    {
        lastStablePosition = camTrans.position;
        currentStablePosition = camTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentStablePosition = camTrans.position;

        accumulativeDist = CalcSqrDistance();
        if (accumulativeDist > 0.36f)
        {
            clover = Instantiate(cloverTemplate, currentStablePosition, Quaternion.identity);
            lastStablePosition = currentStablePosition;
        }
    }


    float CalcSqrDistance()
    {
        return (currentStablePosition - lastStablePosition).sqrMagnitude;
    }
}
