using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFloating : MonoBehaviour
{
    public GameObject lanternTemplate_Red;
    public GameObject lanternTemplate_Blue;
    public GameObject lanternTemplate_LightBlue;


    private Lantern lantern;
    private List<Lantern> lanternList = new List<Lantern>();
    int timer = 0;
    int maxLanternCount = 20;
    int lanternCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {

            int lanternType = Random.Range(0, 3);
            switch (lanternType)
            {
                case 1:
                    lantern = new Lantern(lanternTemplate_Red);
                    break;
                case 2:
                    lantern = new Lantern(lanternTemplate_Blue);
                    break;
                default:
                    lantern = new Lantern(lanternTemplate_LightBlue);
                    break;
            }
            lantern.vel = Random.insideUnitSphere * 0.05f;
            lanternList.Add(lantern);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < lanternCount; j++)
        {
            lantern = lanternList[j];
            lantern.Floating();
            lantern.Boundary();
            lantern.Move();
        }

        if (timer % 16 == 0 && lanternCount < maxLanternCount-1)
        {
            lanternCount += 1;
        }

        if (timer < 330)
        {
            timer++;
        }
    }


}
