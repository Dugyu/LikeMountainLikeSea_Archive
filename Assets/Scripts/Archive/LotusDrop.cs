using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusDrop : MonoBehaviour
{
    public GameObject lotusTemplate;
    public Drop lotus;

    List<Drop> lotusGroup = new List<Drop>();

    int timer;
    // Start is called before the first frame update
    void Start()
    {
        lotus = new Drop(lotusTemplate, new Vector3(0,2,3));
        lotusGroup.Add(lotus);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 120)
        {
            timer = 0;
            lotus = new Drop(lotusTemplate, Random.insideUnitSphere * 2 + new Vector3(0, 2, 3));
            lotusGroup.Add(lotus);
        }

        foreach(Drop lotus in lotusGroup)
        {
            lotus.FallOnMesh();
            lotus.Move();
        }
        timer++;
    }
}
