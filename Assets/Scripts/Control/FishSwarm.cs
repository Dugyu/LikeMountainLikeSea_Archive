using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwarm : MonoBehaviour
{

    // Manager  
    private EnvManager LotusManager;
    int timer;

    // Object Lists
    List<Lotus> localLotusList = new List<Lotus>();
    List<SwarmFish> swarmFishSchool = new List<SwarmFish>();

    // Templates
    public GameObject swarmFishTemplate;


    private void Awake()
    {
        LotusManager = EnvManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        int lotusLastCount = localLotusList.Count;
        localLotusList = LotusManager.LotusList;

        // New Lotus Added
        if (lotusLastCount < localLotusList.Count)
        {
            SwarmFish fish = new SwarmFish(swarmFishTemplate, Random.insideUnitSphere * 1);
            swarmFishSchool.Add(fish);
            foreach (SwarmFish swarmFish in swarmFishSchool)
            {
                swarmFish.lotusTarget = localLotusList[lotusLastCount];
                swarmFish.Swarm(swarmFishSchool);
                swarmFish.WanderThroughLotus();
                swarmFish.Move();
            }
        }
        // Nothing Changed since Last Time
        else if(lotusLastCount > 0)
        {
            if(timer == 400)
            {
                int selected = Random.Range(0, lotusLastCount);
                foreach (SwarmFish swarmFish in swarmFishSchool)
                {
                    swarmFish.lotusTarget = localLotusList[selected];
                    swarmFish.Swarm(swarmFishSchool);
                    swarmFish.WanderThroughLotus();
                    swarmFish.Move();
                }
            }
            else
            {
                foreach (SwarmFish swarmFish in swarmFishSchool)
                {
                    swarmFish.Swarm(swarmFishSchool);
                    swarmFish.WanderThroughLotus();
                    swarmFish.Move();
                }
            }
        }

        if(timer == 480)
        {
            timer = 0;
        }

        timer++;

    }





}
