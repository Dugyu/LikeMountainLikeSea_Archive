using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwarm : MonoBehaviour
{

    // Manager  
    private EnvManager lotusCloverManager;
    int timer;
    int lotusLastCount;
    int cloverLastCount;
    // Object Lists
    List<Lotus> localLotusList = new List<Lotus>();
    List<Clover> localCloverList = new List<Clover>();
    List<SwarmFish> swarmFishSchool = new List<SwarmFish>();

    // Templates
    public GameObject swarmFishTemplate;


    private void Awake()
    {
        lotusCloverManager = EnvManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        lotusLastCount = localLotusList.Count;
        localLotusList = lotusCloverManager.LotusList;
        


        // New Lotus Added
        if (lotusLastCount < localLotusList.Count)
        {
            SwarmFish fish = new SwarmFish(swarmFishTemplate, Random.insideUnitSphere * 1);
            swarmFishSchool.Add(fish);
            foreach (SwarmFish swarmFish in swarmFishSchool)
            {
                swarmFish.lotusCloverTarget = localLotusList[lotusLastCount].pos;
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
                    swarmFish.lotusCloverTarget = localLotusList[selected].pos;
                    swarmFish.Swarm(swarmFishSchool);
                    swarmFish.WanderThroughLotus();
                    swarmFish.Move();
                }
            }
            else if (timer % 400 == 0)
            {
                Vector3 currentClover = lotusCloverManager.ReturnLastClover();
                foreach (SwarmFish swarmFish in swarmFishSchool)
                {
                    swarmFish.lotusCloverTarget = currentClover;
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

        if(timer == 4800)
        {
            timer = 0;
        }

        timer++;

    }





}
