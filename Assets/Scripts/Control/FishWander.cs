﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWander : MonoBehaviour
{

    //Manager
    int timer;
    int loop;

    private BannerManager bannerManager;

  

    int heluoTimer;
    int heluoLoop;
    int gegeTimer;
    int gegeLoop;


    //Object Lists
    List<AmiableFish> gegeShoal = new List<AmiableFish>();
    List<AmiableFish> heluoShoal = new List<AmiableFish>();


    // Templates
    public GameObject gegeTemplate;
    public GameObject heluoTemplate;
    AmiableFish gege;
    AmiableFish heluo;

    Vector3 gegeOrigin = new Vector3(0, 0.5f, 2.52f);
    Vector3 heluoOrigin = new Vector3(-1.5f,0.5f, 1.68f);


    private void Awake()
    {
        bannerManager = BannerManager.Instance;
    }



    // Start is called before the first frame update
    void Start()
    {


        // GEGE
        for (int i = 0; i < 1; i++)
        {
            gege = new AmiableFish(gegeTemplate);
            gege.pos = Random.insideUnitSphere  +  gegeOrigin;
            gege.SetWander(1.0f,4.0f,0.0f,0.2f);
            gege.SetMaxSpeed(0.1f,0.0005f);
            gegeShoal.Add(gege);
        }

        for (int i = 0; i < 1; i++)
        {
            heluo = new AmiableFish(heluoTemplate);
            heluo.pos = Random.insideUnitSphere + heluoOrigin;
            heluo.SetWander(0.2f,2.0f, 0.0f, 0.3f);
            heluoShoal.Add(heluo);
        }
      
    }

    // Update is called once per frame
    void Update()
    {


        
        if(bannerManager.InBanner && bannerManager.CurrentBanner != null)
        {
            string currentBanner = bannerManager.CurrentBanner;
            
            switch (currentBanner)
            {
                case "gege" :
                    GegeUpdate();
                    break;
                case "heluo" :
                    HeluoUpdate();
                    break;
                default:
                    break;
            }
        }


        foreach (AmiableFish fish in gegeShoal)
        {
            fish.Wander();
            fish.Move();
        }

        foreach (AmiableFish fish in heluoShoal)
        {
            fish.Wander();
            fish.Move();
        }

        //if (timer % 32 == 0 && loop < 20)
        //{
        //    gege = new AmiableFish(gegeTemplate);
        //    gege.pos = Random.insideUnitSphere + gegeOrigin;
        //    gege.SetWander(1.0f, 4.0f, 0.0f, 0.2f);
        //    gege.SetMaxSpeed(0.1f, 0.0005f);
        //    gegeShoal.Add(gege);
        //    loop++;
        //}



        //if (timer == 120)
        //{
        //    timer = 0;

        //}

        timer++;

    }


    void GegeUpdate()
    {


        if (gegeTimer % 32 == 0 && gegeLoop < 40)
        {
            gege = new AmiableFish(gegeTemplate);
            gege.pos = Random.insideUnitSphere + gegeOrigin;
            gege.SetWander(1.0f, 4.0f, 0.0f, 0.2f);
            gege.SetMaxSpeed(0.1f, 0.0005f);
            gegeShoal.Add(gege);
            gegeLoop++;
        }



       
        if (gegeTimer == 120)
        {
            gegeTimer = 0;

        }

        gegeTimer++;
    }

    void HeluoUpdate()
    {
        if (timer % 64 == 0 && heluoLoop < 10)
        {
            heluo = new AmiableFish(heluoTemplate);

            heluo.pos = Random.insideUnitSphere * 0.5f + heluoOrigin;
            heluo.SetWander(0.2f, 2.0f, 0.0f, 0.3f);
            heluoShoal.Add(heluo);
            heluoLoop++;
        }



        if (heluoTimer == 130)
        {
            heluoTimer = 0;

        }
        heluoTimer++;
    }








}