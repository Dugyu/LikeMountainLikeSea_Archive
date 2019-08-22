﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWander : MonoBehaviour
{

    //--------------For Generating Wandering Fish--------------
    //--------------Gege and Heluo----------------------------


    //Manager
    private BannerManager bannerManager;

    int heluoTimer;
    int heluoLoop;
    int gegeTimer;
    int gegeLoop;
    int wenyaoTimer;
    int wenyaoLoop;
    int boTimer;
    int boLoop;
    int xixiTimer;
    int xixiLoop;
    int rupiTimer;
    int rupiLoop;

    // Store temporary and long-term Fish Objects
    WanderFish gege;
    WanderFish heluo;
    WanderFish wenyao;
    WanderFish bo;
    WanderFish xixi;
    WanderFish rupi;


    List<WanderFish> gegeShoal = new List<WanderFish>();
    List<WanderFish> heluoShoal = new List<WanderFish>();
    List<WanderFish> wenyaoShoal = new List<WanderFish>();
    List<WanderFish> boShoal = new List<WanderFish>();
    List<WanderFish> xixiShoal = new List<WanderFish>();
    List<WanderFish> rupiShoal = new List<WanderFish>();

    // Fish Instantiate Templates
    public GameObject gegeTemplate; 
    public GameObject heluoTemplate;
    public GameObject wenyaoTemplate_swim;
    public GameObject wenyaoTemplate_sway;
    public GameObject boTemplate;
    public GameObject xixiTemplate;
    public GameObject rupiTemplate;

    Vector3 gegeOrigin; 
    Vector3 heluoOrigin;
    Vector3 wenyaoOrigin;
    Vector3 boOrigin;
    Vector3 xixiOrigin;
    Vector3 rupiOrigin;


    private void Awake()
    {
        bannerManager = BannerManager.Instance;
    }



    // Start is called before the first frame update
    void Start()
    {
        gegeOrigin = BannerManager.gegeOrigin;
        heluoOrigin = BannerManager.heluoOrigin;
        wenyaoOrigin = BannerManager.wenyaoOrigin;
        boOrigin = BannerManager.boOrigin;
        xixiOrigin = BannerManager.xixiOrigin;
        rupiOrigin = BannerManager.rupiOrigin;

        // GEGE
        for (int i = 0; i < 1; i++)
        {
            gege = new WanderFish(gegeTemplate);
            gege.pos = Random.insideUnitSphere  +  gegeOrigin;
            gege.SetWander(1.0f,4.0f,0.0f,0.2f);
            gege.SetMaxSpeed(0.1f,0.0005f);
            gegeShoal.Add(gege);
        }
        // Heluo
        for (int i = 0; i < 1; i++)
        {
            heluo = new WanderFish(heluoTemplate);
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
                case "wenyao":
                    WenyaoUpdate();
                    break;
                case "bo":
                    BoUpdate();
                    break;
                case "xixi":
                    XixiUpdate();
                    break;
                case "rupi":
                    RupiUpdate();
                    break;
                default:
                    break;
            }
        }


        foreach (WanderFish fish in gegeShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }

        foreach (WanderFish fish in heluoShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }

        foreach (WanderFish fish in wenyaoShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }

        foreach (WanderFish fish in boShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }
        foreach (WanderFish fish in xixiShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }
        foreach (WanderFish fish in rupiShoal)
        {
            fish.Wander();
            fish.Boundary();
            fish.Move();
        }
    }


    void GegeUpdate()
    {
        if (gegeTimer % 32 == 0 && gegeLoop < 40)
        {
            gege = new WanderFish(gegeTemplate);
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
        if (heluoTimer % 64 == 0 && heluoLoop < 20)
        {
            heluo = new WanderFish(heluoTemplate);
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

    void WenyaoUpdate()
    {
        if (wenyaoTimer % 128 == 0 && wenyaoLoop < 30)
        {
            wenyao = new WanderFish(wenyaoTemplate_sway);
            wenyao.pos = Random.insideUnitSphere * 0.5f + wenyaoOrigin;
            wenyao.SetWander(0.2f, 2.0f, 0.0f, 0.3f);
            wenyaoShoal.Add(wenyao);
            wenyaoLoop++;
        }
        else if (wenyaoTimer % 64 == 0 && wenyaoLoop < 30)
        {
            wenyao = new WanderFish(wenyaoTemplate_swim);
            wenyao.pos = Random.insideUnitSphere * 0.5f + wenyaoOrigin;
            wenyao.SetWander(0.2f, 2.0f, 0.0f, 0.3f);
            wenyaoShoal.Add(wenyao);
            wenyaoLoop++;
        }
        
        if (wenyaoTimer == 130)
        {
            wenyaoTimer = 0;
        }
        wenyaoTimer++;
    }

    void BoUpdate()
    {
        if (boTimer % 32 == 0 && boLoop < 40)
        {
            bo = new WanderFish(boTemplate);
            bo.pos = Random.insideUnitSphere + boOrigin;
            bo.SetWander(1.0f, 4.0f, 0.0f, 0.2f);
            bo.SetMaxSpeed(0.1f, 0.0005f);
            boShoal.Add(bo);
            boLoop++;
        }
        if (boTimer == 120)
        {
            boTimer = 0;
        }
        boTimer++;
    }


    void XixiUpdate()
    {
        if (xixiTimer % 64 == 0 && xixiLoop < 20)
        {
            xixi = new WanderFish(xixiTemplate);
            xixi.pos = Random.insideUnitSphere * 0.5f + xixiOrigin;
            xixi.SetWander(0.2f, 2.0f, 0.0f, 0.3f);
            xixiShoal.Add(xixi);
            xixiLoop++;
        }
        if (xixiTimer == 130)
        {
            xixiTimer = 0;
        }
        xixiTimer++;
    }

    void RupiUpdate()
    {
        if (rupiTimer % 128 == 0 && rupiLoop < 10)
        {
            rupi = new WanderFish(rupiTemplate);
            rupi.pos = Random.insideUnitSphere * 0.5f + rupiOrigin;
            rupi.SetWander(0.2f, 2.0f, 0.0f, 0.3f);
            rupiShoal.Add(rupi);
            rupiLoop++;
        }
        if (rupiTimer == 130)
        {
            rupiTimer = 0;
        }
        rupiTimer++;
    }
}
