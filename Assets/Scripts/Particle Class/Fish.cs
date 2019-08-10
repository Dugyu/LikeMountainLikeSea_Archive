using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{

    // self
    public int id;

    public GameObject obj;
    public Transform trans;
    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public static Vector3 outterTarget;

    public float m = 1.0f;
    public float maxSpeed = 0.05f;
    public float maxForce = 0.0005f;

    Vector3 instTarget;
    Vector3 smoothTarget;
    public float targetingSpeed = 0.01f; //0 to 1


    // ------------Limited resources--------------------
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


    // --------------Initialize-----------------
    public Fish(GameObject _fishTemplate)
    {
        id = getIndex();
        obj = Object.Instantiate(_fishTemplate);
        trans = obj.transform;
    }

    // --------------------------------------
    public void Move()
    {
        //smoothTarget = pos + vel;
        //acc += Seek(outterTarget)/m *1.5f;
        //vel += acc;
        //vel = Vector3.ClampMagnitude(vel, maxSpeed);
        //instTarget = pos + vel;
        //smoothTarget = smoothTarget * (1.0f - targetingSpeed) + instTarget * targetingSpeed;
        //if (trans.forward == Vector3.up)
        //{
        //    trans.LookAt(smoothTarget, Vector3.forward);
        //}
        //else
        //{
        //    trans.LookAt(smoothTarget);
        //}
        //pos += vel * 0.95f;
        //acc = Vector3.zero;
        //trans.position = pos;

        acc += Seek(outterTarget)/m *1.5f;
        vel += acc * 0.5f;
        vel *= 0.95f;
        pos += vel;
        acc = Vector3.zero;
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





    public void Run(List<Fish> fishs)
    {
        IntoShoal(fishs);
        Move();
    }

    void IntoShoal(List<Fish> fishs)
    {
        Vector3 sep = Separate(fishs);
        Vector3 ali = Align(fishs);
        Vector3 coh = Cohesion(fishs);

        sep *= (1.0f);
        ali *= (1.5f);
        coh *= (0.8f);

        ApplyForce(sep);
        ApplyForce(ali);
        ApplyForce(coh);

    }

    void ApplyForce(Vector3 force)
    {
        acc += force;
    }


    // -----------------Behaviours----------------

    Vector3 Separate(List<Fish> fishs)
    {
        float desiredSeparation =0.8f;
        Vector3 steer = Vector3.zero;
        int count = 0;

        // For every fish in the system, check if it's too close
        foreach(Fish f in fishs)
        {
            if (f.id != id){
                float d = (f.pos - pos).magnitude;
                if((d > 0) && (d < desiredSeparation))
                {
                    Vector3 diff = pos - f.pos;   // pointing away from neighbor
                    diff.Normalize();
                    diff /= d;        // Weight by distance
                    steer += diff;
                    count++;
                }
            }
        }

        
        if (count > 0)
        {
            steer /= count;
        }

        if(steer.sqrMagnitude > 0)
        {
            steer.Normalize();
            steer *= maxSpeed;
            steer -= vel;   // steering = desired - current
            steer = Vector3.ClampMagnitude(steer, maxForce);
        }


        return steer;
    }
    Vector3 Align(List<Fish> fishs)
    {
        float neighborDist = 6.0f;
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (Fish f in fishs)
        {
            if (f.id != id)
            {
                float d = (f.pos - pos).magnitude;
                if((d>0) && (d < neighborDist))
                {
                    sum += f.vel;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            sum /= count;

            sum.Normalize();
            sum *= maxSpeed;
            Vector3 steer = sum - vel;
            steer = Vector3.ClampMagnitude(steer, maxForce);
            return steer;

        }
        else
        {
            return Vector3.zero;
        }

    }
    Vector3 Cohesion(List<Fish> fishs)
    {
        float neighborDist = 6.0f;
        Vector3 sum = Vector3.zero;
        int count = 0;
        foreach(Fish f in fishs)
        {
            float d = (f.pos - pos).magnitude;
            if ((d > 0) && (d < neighborDist))
            {
                sum += f.pos;
                count++;
            }
        }

        if(count > 0)
        {
            sum /= count;
            return Seek(sum);
        }
        else
        {
            return Vector3.zero;
        }
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
