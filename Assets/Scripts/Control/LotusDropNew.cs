using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LotusDropNew : MonoBehaviour
{
    // Manager  
    private EnvManager LotusManager;
    int timer;

    //Object Lists
    List<Lotus> localLotusList = new List<Lotus>();
    List<RiseFish> riseFishSchool = new List<RiseFish>();

    List<int> selectedLotus = new List<int>();

    // Templates
    public GameObject riseFishTemplate;

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



        if(timer % 16 == 0 && selectedLotus.Count > 0)
        {
            foreach(int index in selectedLotus)
            {
                float ran = Random.Range(0, 1.0f);
                if (ran < 0.05f)
                {
                    riseFishSchool.Add(new RiseFish(riseFishTemplate, localLotusList[index].pos));
                }
            }
        }


        foreach(RiseFish fish in riseFishSchool)
        {
            fish.RiseToMesh();
            fish.Move();
        }

        for (int i = riseFishSchool.Count - 1; i > -1; i--)
        {
            RiseFish fish = riseFishSchool[i];
            if (fish.IsOutRange(4.6f))
            {
                riseFishSchool.RemoveAt(i);
                fish.Release();
                fish = null;
            }

        }


        if (timer == 120)
        {
            timer = 0;
            selectedLotus = RandomSelect(Enumerable.Range(0, localLotusList.Count).ToList(), 0.3f, 5);

        }

        timer++;

    }
        
    // ----------------- Helper Function ------------------------

    List<int> RandomSelect(List<int> candidates, float ratio, int maxCount)
    {

        int choice = (int)Mathf.Floor(candidates.Count * ratio);
        if (choice > maxCount){
            choice = maxCount;
        }
        List<int> selected = new List<int>();
        for (int i = 0; i < choice; i++)
        {

            selected.Add(Random.Range(0, candidates.Count));         
            candidates.RemoveAt(selected[i]);
        }
        return selected;
    }


}
