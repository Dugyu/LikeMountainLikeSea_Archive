﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiableFish
{
    // ------------ Amiable Fish ------------------------
    // Move towards player(s)

    public int id;
    public GameObject obj;
    public Transform trans;

    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public Queue<Vector3> forceQueue = new Queue<Vector3>();

    public float wanderTheta;

    public static List<GameObject> playersList = new List<GameObject>();


    static float maxSpeed = 0.05f;
    static float maxForce = 0.0005f;


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
    public AmiableFish(GameObject _fishTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_fishTemplate);
        trans = obj.transform;
    }

    public void Move()
    {
        ApplyForce();
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

        if (trans.forward == Vector3.up)
        {
            trans.LookAt(pos + vel, Vector3.forward);
        }
        else
        {
            trans.LookAt(pos + vel); 
        }
    }

    //------------ Apply Forces --------------------------
    void ApplyForce()
    {
        if (forceQueue.Count > 0)
        {
            Vector3 force = forceQueue.Dequeue();
            acc = force;
        }
    }

    //------------ Behaviours ----------------------------
    public void Wander()
    {
        float wanderCircleRadius = 0.5f;
        float wanderCircleDistance = 3;

        wanderTheta += Random.Range(-0.5f, 0.5f);

        Vector3 vz = obj.transform.forward;   // forward 
        Vector3 vy = obj.transform.up;        // up
        Vector3 vx = Vector3.Cross(vy, vz);   //

        Vector3 circlePt = vx * Mathf.Cos(wanderTheta) + vz * Mathf.Sin(wanderTheta);
        Vector3 target = pos + vz * wanderCircleDistance + circlePt * wanderCircleRadius;

        forceQueue.Enqueue(Seek(target));
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - pos;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
}
