using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern
{
    public int id;
    public GameObject obj;
    public Transform trans;

    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public Queue<Vector3> wanderForceQueue = new Queue<Vector3>();
    public Queue<Vector3> upForceQueue = new Queue<Vector3>();

    public float wanderCircleRadius = 0.5f;
    public float wanderCircleDistance = 3;
    public float wanderTheta = 0;
    public float wanderThetaDelta = 0.2f;

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
    public Lantern(GameObject _lanternTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_lanternTemplate);
        trans = obj.transform;
        Place();
        Move();
    }

    public void Place()
    {
        //trans.localScale = Random.Range(0.5f, 1.0f) * trans.localScale;
        Vector2 posXY = Random.insideUnitCircle * 3.0f;
        pos = new Vector3(posXY.x, -1.8f, posXY.y);
    }

    //------------ Updates --------------------------------
    public void SetWander(float _wanderCircleRadius, float _wanderCircleDistance, float _wanderTheta, float _wanderThetaDelta)
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
        ApplyForces();
        vel += acc * 0.5f;
        vel *= 0.95f;
        pos += vel;
        acc = Vector3.zero;           // acceleration is instant
        Draw();
    }

    //------------ Draw Obj ------------------------------
    void Draw()
    {
        trans.position = pos;
        //trans.LookAt(pos + vel, trans.up);
    }


    //------------ Apply Forces --------------------------
    void ApplyForces()
    {
        if (wanderForceQueue.Count > 0)
        {
            Vector3 force = wanderForceQueue.Dequeue();
            acc += force;
        }
        if (upForceQueue.Count > 0)
        {
            Vector3 force = upForceQueue.Dequeue();
            acc += force;
        }
    }
    //------------ High Level Behaviours ----------------------------
    public void Floating()
    {
        Vector3 force = new Vector3(0, 0.0008f, 0);
        upForceQueue.Enqueue(force);
    }


    public void Boundary()
    {

        if (pos.y > 3.0f)
        {
            Place();
            Move();
        }
    }
}
