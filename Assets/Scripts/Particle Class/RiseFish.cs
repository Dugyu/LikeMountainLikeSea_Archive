using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseFish 
{
    public int id;
    public GameObject obj;
    public Transform trans;

    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;

    public float maxSpeed = 0.1f;
    public float maxForce = 0.0005f;

    public Queue<Vector3> risingForceQueue = new Queue<Vector3>();

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

    //------------ Initializers ----------------------------
    public RiseFish(GameObject _objTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_objTemplate);
        trans = obj.transform;
    }
    public RiseFish(GameObject _objTemplate, Vector3 _pos)
    {
        id = getIndex();
        obj = Object.Instantiate(_objTemplate);
        trans = obj.transform;

        pos = _pos;
    }
    public void Release()
    {
        releaseIndex(id);
        Object.Destroy(obj);
        
        obj = null;
        trans = null;
    }
    //------------ Update Position,Velocity,Acceleration ----------------------------
    public void Move()
    {
        ApplyAllForces();
        vel += acc * 0.5f;
        vel *= 0.95f;
        pos += vel;
        acc = Vector3.zero;           // acceleration is instant
        Draw();
    }

    void Draw()
    {
        trans.position = pos;
    }

    //------------ State Indicator---------------------------------

    public bool IsOutRange(float uperLimit)
    {
        if (pos.y > uperLimit )
        {
            return true;
        }
        return false;
    }

    //------------ Apply Forces --------------------------
    void ApplyAllForces()
    {
        ApplyForceQueue(risingForceQueue);
    }
    void ApplyForceQueue(Queue<Vector3> forceQueue)
    {
        if (forceQueue.Count > 0)
        {
            acc += forceQueue.Dequeue();
        }
    }

    //------------ High Level Behaviours ----------------------------
    public void RiseToMesh()
    {

        int layerMask = 1 << 15;
        RaycastHit hit;

        if (Physics.Raycast(pos, Vector3.up, out hit, layerMask))
        {
            Vector3 target = hit.point;
            risingForceQueue.Enqueue(ArrivingAt(target, 2.0f));
        }

    }



    //------------ Low Level Behaviours -----------------------------

    Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - pos;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
    Vector3 SeekLimited(Vector3 target, float limitedSpeed)
    {
        // seeking using a customized maxspeed

        Vector3 desired = target - pos;
        desired.Normalize();
        desired *= limitedSpeed;
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
    Vector3 ArrivingAt(Vector3 target, float slowDownRange)
    {
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
