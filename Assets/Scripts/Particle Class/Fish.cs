using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{

    // self
    public GameObject obj;
    public Vector3 pos = Vector3.zero;
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;

    public Vector3 target;

    public float m = 1.0f;
    public float maxSpeed = 0.02f;
    public float maxForce = 0.01f;

    public Fish(GameObject _fishTemplate)
    {

        obj = Object.Instantiate(_fishTemplate);
    }

    public void Move()
    {
        
        acc = Seek(target)/m;
        vel *= 0.8f;
        vel += acc;
        vel = Vector3.ClampMagnitude(vel, maxSpeed);
        if(obj.transform.forward == Vector3.up)
        {
            obj.transform.LookAt(pos + vel,Vector3.forward);
        }
        else
        {
            obj.transform.LookAt(pos + vel);
        }

        pos += vel;
        obj.transform.position = pos;

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
