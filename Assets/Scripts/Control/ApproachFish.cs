using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachFish : MonoBehaviour
{
    public GameObject fishTemplate;
    public GameObject player;
    SensitiveFish sensitiveFish;
    List<SensitiveFish> sensitiveFishShoal = new List<SensitiveFish>();
    // Start is called before the first frame update
    void Start()
    {
        SensitiveFish.AddEnemy(player);
        for (int i = 0; i < 10; i++)
        {
            sensitiveFish = new SensitiveFish(fishTemplate);
            sensitiveFish.pos = Random.insideUnitSphere * 2 + fishTemplate.transform.position;
            sensitiveFishShoal.Add(sensitiveFish);
        }

        fishTemplate.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (SensitiveFish fish in sensitiveFishShoal)
        {
            fish.Move();
        }
    }
}
