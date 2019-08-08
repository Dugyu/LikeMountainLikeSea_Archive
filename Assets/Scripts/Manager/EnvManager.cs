using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class EnvManager: MonoBehaviour
{
    // Singleton Instance
    private static EnvManager _instance;
    public static EnvManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnvManager>();
            }
            return _instance;
        }
    }

    private List<Lotus> lotusList= new List<Lotus>();

    public List<Lotus> LotusList
    {
        get
        {
            lock (lotusList)
            {
                return new List<Lotus>(lotusList);
            }
        }
    }

    public void AddLotus(Lotus lotus)
    {
        lock (lotusList)
        {
            lotusList.Add(lotus);
        }
    }
    
}
