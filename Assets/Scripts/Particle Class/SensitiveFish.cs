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

}
