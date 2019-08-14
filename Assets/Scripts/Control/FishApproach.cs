using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishApproach : MonoBehaviour
{
    private BannerManager bannerManager;

    int xixiTimer;
    int xixiLoop;

    public GameObject xixiTemplate;
    public GameObject cam;
    Vector3 xixiOrigin = new Vector3(-1.37f,0,1.75f);

    SensitiveFish xixi;
    List<SensitiveFish> xixiShoal = new List<SensitiveFish>();


    private void Awake()
    {
        bannerManager = BannerManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        SensitiveFish.AddEnemy(cam);




    }

    // Update is called once per frame
    void Update()
    {
        if (bannerManager.InBanner && bannerManager.CurrentBanner != null)
        {
            string currentBanner = bannerManager.CurrentBanner;

            switch (currentBanner)
            {
                case "xixi":
                    XixiUpdate();
                    break;
                default:
                    break;
            }
        }

        foreach (SensitiveFish fish in xixiShoal)
        {
            fish.Move();
        }



    }


    void XixiUpdate()
    {

        if (xixiTimer % 64 == 0 && xixiLoop < 5)
        {
            xixi = new SensitiveFish(xixiTemplate);

            xixi.pos = Random.insideUnitSphere * 0.5f + xixiOrigin;
            xixiShoal.Add(xixi);
            xixiLoop++;
        }

        if (xixiTimer == 130)
        {
            xixiTimer = 0;

        }
        xixiTimer++;
    }




}
