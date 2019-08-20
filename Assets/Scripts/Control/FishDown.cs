using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDown : MonoBehaviour
{

    private BannerManager bannerManager;

    int rupiTimer;
    int rupiLoop;

    public GameObject rupiTemplate;
    private GameObject rupi;
    Vector3 rupiOrigin; // = new Vector3(-1.99f, 0.78f, 1.4f);

    
    private void Awake()
    {
        bannerManager = BannerManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        rupiOrigin = BannerManager.rupiOrigin;
    }

    // Update is called once per frame
    void Update()
    {
        if (bannerManager.InBanner && bannerManager.CurrentBanner != null)
        {
            string currentBanner = bannerManager.CurrentBanner;

            switch (currentBanner)
            {
                case "rupi":
                    RupiUpdate();
                    break;
                default:
                    break;
            }
        }


    }


    void RupiUpdate()
    {
        if (rupiTimer % 64 == 0 && rupiLoop < 40)
        {
            rupi = Instantiate(rupiTemplate);
            rupi.transform.position = Random.insideUnitSphere * 0.5f + rupiOrigin;
            rupiLoop++;
        }

        if (rupiTimer == 130)
        {
            rupiTimer = 0;

        }
        rupiTimer++;
    }


}
