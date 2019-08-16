using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmFish
{


    // ------------ Swarm Fish ------------------------
    // Swarm Swarm Swarm

    public int id;
    public GameObject obj;
    public Transform trans;

    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;

    public float maxSpeed = 0.05f;
    public float maxForce = 0.0005f;

    public Lotus lotusTarget;


    public Queue<Vector3> wanderLotusForceQueue = new Queue<Vector3>();
    public Queue<Vector3> separeteForceQueue = new Queue<Vector3>();
    public Queue<Vector3> alignForceQueue = new Queue<Vector3>();
    public Queue<Vector3> cohesionForceQueue = new Queue<Vector3>();

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
    public SwarmFish(GameObject _objTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_objTemplate);
        trans = obj.transform;
    }
    public SwarmFish(GameObject _objTemplate, Vector3 _pos)
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
    void ApplyAllForces()
    {
        ApplyForceQueue(wanderLotusForceQueue);
        ApplyForceQueue(separeteForceQueue);
        ApplyForceQueue(alignForceQueue);
        ApplyForceQueue(cohesionForceQueue);
    }

    void ApplyForceQueue(Queue<Vector3> forceQueue)
    {
        if (forceQueue.Count > 0)
        {
            acc += forceQueue.Dequeue();
        }
    }

    //------------ High Level Behaviours -----------------------------
    public void Swarm(List<SwarmFish> fishSchool)
    {
        float desiredSeparation = 0.09f;  // separation
        float neighborDist = 1.0f;  // align and cohesion

        Vector3[] sums = new Vector3[3];  // store the sum of separation, alignment, cohesion
        int[] counts = new int[3];  // store the number of fish in each condition
        float[] ratio = new float[] {5.0f, 1.0f, 1.0f};  // store the ratio of these three forces


        foreach (SwarmFish other in fishSchool)
        {
            // do not count itself
            if (other.id != id)
            {
                Vector3 difference = pos - other.pos;   // pointing from neighbor towards self
                float sqrDist = difference.sqrMagnitude;
                difference /= sqrDist;  // Normalize and Weight by distance =  divided by squareMagnitude

                // Separate
                if ((sqrDist > 0) && (sqrDist < desiredSeparation))
                {
                    sums[0] += difference;
                    counts[0] += 1;
                }

                // Alignment and Cohesion
                if ((sqrDist > 0) && (sqrDist < neighborDist))
                {
                    sums[1] += other.vel;
                    sums[2] += other.pos;
                    counts[1] += 1;
                    counts[2] += 1;
                }
            }
        }


        for(int i = 0; i < 3; i++)
        {
            if (counts[i] > 0)
            {
               Vector3 desired = sums[i] / counts[i];
               Vector3 steer;
               switch (i)
                {
                    case 1:
                        // Alignment
                        steer = SeekVelocity(desired, maxSpeed);
                        alignForceQueue.Enqueue(steer * ratio[i]);
                        break;
                    case 2:
                        // Cohesion
                        steer = ArrivingAt(desired, 0.3f);
                        cohesionForceQueue.Enqueue(steer * ratio[i]);
                        break;
                    default:
                        // Separation
                        steer = SeekVelocity(desired, maxSpeed);
                        for(int j = 0; j < 10; j++)
                        {
                            separeteForceQueue.Enqueue(steer * ratio[i]);
                        }
                        break;
                }
            }
        }
    }
    public void WanderThroughLotus()
    {
        wanderLotusForceQueue.Enqueue(ArrivingAt(lotusTarget.pos, 0.5f) * 1.0f);
    }
    //------------ Low Level Behaviours -----------------------------
    Vector3 SeekVelocity(Vector3 desired, float speed)
    {
        desired.Normalize();
        desired *= speed;
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
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
