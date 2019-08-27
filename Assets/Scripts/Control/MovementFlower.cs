using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFlower : MonoBehaviour
{
    // Manager Class
    private EnvManager CloverManager;




    public Transform camTrans;
    public GameObject cloverTemplate;
    public Transform cloverParent;

    Vector3 lastStablePosition;
    Vector3 currentStablePosition;
    float accumulativeDist;
    Clover clover;
    int cloverCount;
    int maxCloverCount = 30;

    Vector3 height = new Vector3(0, -0.1f, 0);




    private void Awake()
    {
        CloverManager = EnvManager.Instance;

    }



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
            if (cloverCount < maxCloverCount)
            {
            clover = new Clover(cloverTemplate, currentStablePosition + height, cloverParent);
            CloverManager.AddClover(clover);
            }
            else
            {
                int index = cloverCount - maxCloverCount;
                CloverManager.MoveClover(index, currentStablePosition + height);
            }
            cloverCount += 1;
            lastStablePosition = currentStablePosition;
        }
    }


    float CalcSqrDistance()
    {
        return (currentStablePosition - lastStablePosition).sqrMagnitude;
    }
}
