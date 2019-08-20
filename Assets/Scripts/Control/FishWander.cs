using System.Collections;
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

    // Store temporary and long-term Fish Objects
    WanderFish gege;
    WanderFish heluo;
    WanderFish wenyao;
    WanderFish bo;
    List<WanderFish> gegeShoal = new List<WanderFish>();
    List<WanderFish> heluoShoal = new List<WanderFish>();
    List<WanderFish> wenyaoShoal = new List<WanderFish>();
    List<WanderFish> boShoal = new List<WanderFish>();
    // Fish Instantiate Templates
    public GameObject gegeTemplate; 
    public GameObject heluoTemplate;
    public GameObject wenyaoTemplate;
    public GameObject boTemplate;

    Vector3 gegeOrigin; //= new Vector3(0.47f, 0.5f, 2.52f);
    Vector3 heluoOrigin; //= new Vector3(-0.42f,0.5f, 2.17f);

    private void Awake()
    {
        bannerManager = BannerManager.Instance;
    }



    // Start is called before the first frame update
    void Start()
    {
        gegeOrigin = BannerManager.gegeOrigin;
        heluoOrigin = BannerManager.heluoOrigin;




        // GEGE
        for (int i = 0; i < 1; i++)
        {
            gege = new WanderFish(gegeTemplate);
            gege.pos = Random.insideUnitSphere  +  gegeOrigin;
            gege.SetWander(1.0f,4.0f,0.0f,0.2f);
            gege.SetMaxSpeed(0.1f,0.0005f);
            gegeShoal.Add(gege);
        }

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
                default:
                    break;
            }
        }


        foreach (WanderFish fish in gegeShoal)
        {
            fish.Wander();
            fish.Move();
        }

        foreach (WanderFish fish in heluoShoal)
        {
            fish.Wander();
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

}
