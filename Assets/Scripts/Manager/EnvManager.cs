﻿using System;
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
    private List<Clover> cloverList = new List<Clover>();

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

    public List<Clover> CloverList
    {
        get
        {
            lock (cloverList)
            {
                return new List<Clover>(cloverList);
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
    
    public void AddClover(Clover clover)
    {
        lock (cloverList)
        {
            cloverList.Add(clover);
        }
    }

    public void MoveClover(int index, Vector3 Position)
    {
        Clover clover = cloverList[index];
        clover.Move(Position);
    }

    public Vector3 ReturnLastClover()
    {
        if (cloverList.Count > 0) { return cloverList[cloverList.Count - 1].pos; }
        else
        {
            return Vector3.zero;
        }
        
    }

    public Vector3 ReturnLastLotus()
    {
        if (lotusList.Count > 0) { return cloverList[lotusList.Count - 1].pos; }
        else
        {
            return Vector3.zero;
        }
    }
}
