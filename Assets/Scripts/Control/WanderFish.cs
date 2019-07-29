using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFish : MonoBehaviour
{
    public GameObject fishTemplate;
    AmiableFish amiableFish;
    List<AmiableFish> amiableFishShoal = new List<AmiableFish>();
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            amiableFish = new AmiableFish(fishTemplate);
            amiableFish.pos = Random.insideUnitSphere * 2 + fishTemplate.transform.position;
            amiableFishShoal.Add(amiableFish);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AmiableFish fish in amiableFishShoal)
        {
            fish.Wander();
            fish.Move();
        }
        
    }
}
