using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{

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
