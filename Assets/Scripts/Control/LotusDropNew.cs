using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusDropNew : MonoBehaviour
{
    // Manager Class
    private EnvManager LotusManager;
    List<Lotus> localLotusList = new List<Lotus>();
    private void Awake()
    {
        LotusManager = EnvManager.Instance;

    }


    // Update is called once per frame
    void Update()
    {
        localLotusList = LotusManager.LotusList;
        foreach (Lotus lotus in localLotusList)
        {
            lotus.FallOnMesh();
            lotus.Move();
        }
    }
}
