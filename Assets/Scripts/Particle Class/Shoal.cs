using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoal
{
    List<Fish> fishs;

    public Shoal()
    {
        fishs = new List<Fish>();
    }

    public void AddFish(Fish f)
    {
        fishs.Add(f);
    }

    public void Run()
    {
        foreach(Fish f in fishs)
        {
            f.Run(fishs);
        }
    }
}
