using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover 
{
    // ------------ Clover Class -----------------------
    // ------------ Following Players Trails ------------

    public int id;
    public GameObject obj;
    public Transform trans;

    public Vector3 pos = Vector3.zero;



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
    public Clover(GameObject _objTemplate, Vector3 _pos, Transform _parent)
    {
        id = getIndex();
        obj = Object.Instantiate(_objTemplate,_pos,Quaternion.identity,_parent);
        trans = obj.transform;
        pos = _pos;
        trans.localPosition = pos;
    }

    //------------ Update Position ----------------------------
    public void Move(Vector3 _pos)
    {
        pos = _pos;
        trans.localPosition = _pos;
    }


}
