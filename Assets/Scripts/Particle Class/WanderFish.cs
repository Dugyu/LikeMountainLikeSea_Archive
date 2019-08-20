using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFish
{
    // ------------ Wander Fish : Move Freely ------------------------
 

    public int id;
    public GameObject obj;
    public Transform trans;


    public float wanderCircleRadius = 0.5f;
    public float wanderCircleDistance = 3;
    public float wanderTheta = 0;
    public float wanderThetaDelta = 0.2f;
    public float wanderThetaUpDown = 0;
    public float wanderThetaUpDownDelta = 0.05f;


    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public Queue<Vector3> wanderForceQueue = new Queue<Vector3>();
    public Queue<Vector3> boundaryForceQueue = new Queue<Vector3>();
    public Queue<Vector3> wanderUpDownForceQueue = new Queue<Vector3>();


    public static List<GameObject> playersList = new List<GameObject>();

    public float maxSpeed = 0.05f;
    public float maxForce = 0.0005f;


    //------------ Limited Resources ----------------------
    static LinkedList<int> unusedIndices = new LinkedList<int>();
    static int lastIndex = 1;
    static int getIndex()
    {
        int idx = 0;
        if (unusedIndices.Count != 0)
        {
            idx = unusedIndices.Last.Value;
            unusedIndices.RemoveLast();
        }
        else
        {
            idx = lastIndex;
            lastIndex++;
        }
        return idx;
    }
    static void releaseIndex(int idx)
    {
        unusedIndices.AddLast(idx);
    }

    //------------ Initializer ----------------------------
    public WanderFish(GameObject _fishTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_fishTemplate);
        trans = obj.transform;
        Place();
    }

    public void Place()
    {
        trans.localScale = Random.Range(0.5f,1.0f) * trans.localScale ;
    }

    //------------ Updates --------------------------------
    public void SetWander(float _wanderCircleRadius, float _wanderCircleDistance, float _wanderTheta,float _wanderThetaDelta)
    {
        wanderCircleRadius = _wanderCircleRadius;
        wanderCircleDistance = _wanderCircleDistance;
        wanderTheta = _wanderTheta;
        wanderThetaDelta = _wanderThetaDelta;
    }
    
    public void SetMaxSpeed(float _maxSpeed, float _maxForce)
    {
        maxSpeed = _maxSpeed;
        maxForce = _maxForce;
    }

    public void Move()
    {
        ApplyForce();
        vel += acc * 0.5f;
        vel *= 0.95f;
        pos += vel;        
        acc = Vector3.zero;           // acceleration is instant
        Boundary();
        Draw();
    }



    //------------ State Indicator---------------------------------



    //------------ Draw Obj ------------------------------
    void Draw()
    {
        trans.position = pos;
        trans.LookAt(pos + vel, trans.up); 

    }





    //------------ Apply Forces --------------------------
    void ApplyForce()
    {
        if (wanderForceQueue.Count > 0)
        {
            Vector3 force = wanderForceQueue.Dequeue();
            acc += force;
        }
        if (wanderUpDownForceQueue.Count > 0)
        {
            Vector3 force = wanderUpDownForceQueue.Dequeue();
            acc += force;
        }
        if (boundaryForceQueue.Count > 0)
        {
            Vector3 force = boundaryForceQueue.Dequeue();
            acc += force;
        }
    }

    //------------ High Level Behaviours ----------------------------
    public void Wander()
    {

        wanderTheta += Random.Range(-wanderThetaDelta, wanderThetaDelta);

        Vector3 vz = obj.transform.forward;   // forward 
        Vector3 vy = obj.transform.up;        // up
        Vector3 vx = Vector3.Cross(vy, vz);   // right

        Vector3 circlePt = vx * Mathf.Cos(wanderTheta) + vz * Mathf.Sin(wanderTheta);
        Vector3 target = pos + vz * wanderCircleDistance + circlePt * wanderCircleRadius;

        wanderThetaUpDown += Random.Range(-wanderThetaUpDownDelta, wanderThetaUpDownDelta);
        Vector3 circleUpDownPt = vy* Mathf.Cos(wanderThetaUpDown) + vz * Mathf.Sin(wanderThetaUpDown);
        Vector3 targetUpDown = pos + vz * wanderCircleDistance + circleUpDownPt * wanderCircleRadius;



        wanderUpDownForceQueue.Enqueue(Seek(targetUpDown)*0.1f);


        wanderForceQueue.Enqueue(Seek(target));
    }

    public void Boundary()
    {
        float boundaryDist = 0.8f;
        int layerMask = 1 << 16;
        RaycastHit hit;

        if(Physics.Raycast(pos, trans.forward, out hit, boundaryDist, layerMask))
        {

            Vector3 target = pos + hit.normal;


            // for smooth behaviour, let the force have longer effect
            for (int i = 0; i < 10; i++)
            {
                boundaryForceQueue.Enqueue(Seek(target));
            }
        }
    }

    //------------ Low Level Behaviours -----------------------------
    Vector3 Seek(Vector3 target)
    {
        //-------Seeking a Position (Target) using a maxSpeed--------

        Vector3 desired = target - pos;                     // Desired Direction
        desired.Normalize();                                // Desired Direction in Unit Vector (Magnitude = 1.0f)
        desired *= maxSpeed;                                // Desired Velocity in Desired Direction at Maximum Speed
        Vector3 steer = desired - vel;                      // Steer Force needed for transforming the Current Velocity into Desired Velovity
        steer = Vector3.ClampMagnitude(steer, maxForce);    // Limit Steer Force  
        return steer;
    }

    Vector3 SeekLimited(Vector3 target, float limitedSpeed)
    {
        //-------Seeking a Position (Target) using a customized speed--------

        Vector3 desired = target - pos;                     // Desired Direction
        desired.Normalize();                                // Desired Direction in Unit Vector (Magnitude = 1.0f)
        desired *= limitedSpeed;                            // Desired Velocity in Desired Direction at Maximum Speed
        Vector3 steer = desired - vel;                      // Steer Force needed for transforming the Current Velocity into Desired Velovity
        steer = Vector3.ClampMagnitude(steer, maxForce);    // Limit Steer Force 
        return steer;
    }

    Vector3 ArrivingAt(Vector3 target, float slowDownRange)
    {
        //-------Arriving at a Position (Target) using a customized speed--------
        //-------Seeking and Slow Down within the slowDownRange of the target

        float sqrSlowDownRange = slowDownRange * slowDownRange;
        float sqrDist = (target - pos).sqrMagnitude;
        if (sqrDist > sqrSlowDownRange)
        {
            return Seek(target);
        }
        else
        {
            float limitedSpeed = Mathf.Lerp(0, maxSpeed, Mathf.InverseLerp(0, sqrSlowDownRange, sqrDist));
            return SeekLimited(target, limitedSpeed);
        }
    }
}
