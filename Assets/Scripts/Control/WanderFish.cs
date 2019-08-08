using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFish : MonoBehaviour
{
    public GameObject gegeTemplate;
    public GameObject heluoTemplate;
    AmiableFish gege;
    AmiableFish heluo;

    List<AmiableFish> gegeShoal = new List<AmiableFish>();
    List<AmiableFish> heluoShoal = new List<AmiableFish>();

    // Start is called before the first frame update
    void Start()
    {
        // GEGE
        for (int i = 0; i < 20; i++)
        {
            gege = new AmiableFish(gegeTemplate);

            gege.pos = Random.insideUnitSphere  + gegeTemplate.transform.position ;
            gege.SetWander(1.0f,4.0f,0.0f,0.2f);
            gege.SetMaxSpeed(0.1f,0.0005f);
            gegeShoal.Add(gege);
        }
        gegeTemplate.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            heluo = new AmiableFish(heluoTemplate);

            heluo.pos = Random.insideUnitSphere + heluoTemplate.transform.position;
            heluo.SetWander(0.5f,2.0f, 0.0f, 0.3f);
            
            heluoShoal.Add(heluo);
        }
        heluoTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AmiableFish fish in gegeShoal)
        {
            fish.Wander();
            fish.Move();
        }

        foreach (AmiableFish fish in heluoShoal)
        {
            fish.Wander();
            fish.Move();
        }
    }
}
