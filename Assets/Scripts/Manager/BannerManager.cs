using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{
    public static Vector3 gegeOrigin = new Vector3(0.47f, 0.5f, 2.52f);
    public static Vector3 heluoOrigin = new Vector3(-0.42f, 0.5f, 2.17f);
    public static Vector3 xixiOrigin = new Vector3(-1.37f,0,1.75f);
    public static Vector3 rupiOrigin = new Vector3(-1.99f, 0.0f, 1.4f);
    public static Vector3 boOrigin = new Vector3(1.2f, 0.28f, 1.58f);
    public static Vector3 wenyaoOrigin = new Vector3(1.6f, 0.28f, 0.93f);



    private static BannerManager _instance;
    public static BannerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BannerManager>();
            }
            return _instance;
        }
    }


    private string currentBanner { get; set;}
    private bool inBanner { get; set;}

    public void setCurrentBanner(string s)
    {
        currentBanner = s;
    }

    public void setInBanner(bool value)
    {
        inBanner = value;
    }
    public bool InBanner
    {
        get
        {
            return inBanner;
        }
    }

    public string CurrentBanner
    {
        get
        {
            return currentBanner;
        }
    }




}
