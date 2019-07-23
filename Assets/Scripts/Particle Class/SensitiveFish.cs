using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitiveFish
{
    // ------------ Sensitive Fish ------------------------
    // Move away if too close to humankind

    public int id;
    public GameObject obj;

    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public Queue<Vector3> forceQueue = new Queue<Vector3>();

    // Once an enemy enters the actionRange,
    // fish will consider all enemies inside sensingRange 
    // to choose a better direction to run away
    static List<GameObject> enemyList = new List<GameObject>();
    static float sensingRange = 1.0f;
    static float actionRange = 0.25f;
    public bool inDanger = false;
    public List<Vector3> dangerList = new List<Vector3>();

    public float m = 1.0f;

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
    public SensitiveFish(GameObject _fishTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_fishTemplate);
    }

    public void Move()
    {
         vel += acc;
         vel *= 0.95f;
         pos += vel;
         obj.transform.position = pos;
         acc = Vector3.zero;                       // acceleration is instant
    }

    public void ApplyForce()
    {
        Vector3 force = forceQueue.Dequeue();
        acc = force / m;
    }

    public void AddImpuseForce(int duration, Vector3 force)
    {
        for (int i = 0; i < duration; i++)
        {
            forceQueue.Enqueue(force);
        }
    }

    public Vector3 CalculateForce()
    {
        if (!inDanger) return Vector3.zero;
        else
        {
            Vector3 awayDirection = Vector3.zero;
            foreach (Vector3 dir in dangerList)
            {
                awayDirection += dir;
            }
            return awayDirection * dangerList.Count;
        }
    }


    public int DangerDetect(GameObject enemy)
    {
        Vector3 ePos = enemy.transform.position;
        Vector3 direction = ePos - pos;  // towards this fish
        float sqrDist = direction.sqrMagnitude;
        if(sqrDist < sensingRange)
        {
            dangerList.Add(direction.normalized);
            if(sqrDist < actionRange)
            {
                inDanger = true;
                return 2;
            }
            return 1;
        }
        return 0;
    }
}
