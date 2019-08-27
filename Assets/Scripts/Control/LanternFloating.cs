using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFloating : MonoBehaviour
{
    public GameObject lanternTemplate_Red;
    public GameObject lanternTemplate_Blue;
    public GameObject lanternTemplate_LightBlue;
    public GameObject peachBlossomTemplate;

    private Lantern lantern;
    private Lantern peachBlossom;
    private List<Lantern> lanternList = new List<Lantern>();
    private List<Lantern> peachBlossomList = new List<Lantern>();
    int timer = 0;
    int peachBlossomTimer = 0;
    int maxLanternCount = 20;
    int maxPeachBlossomCount = 30;
    int peachBlossomCount = 0;
    int lanternCount = 0;

    int interval = 16;

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
        foreach ( Lantern peachBlossom in peachBlossomList)
        {

            peachBlossom.Floating();
            peachBlossom.Boundary();
            peachBlossom.Move();
        }

        for (int j = 0; j < lanternCount; j++)
        {
            lantern = lanternList[j];
            lantern.Floating();
            lantern.Boundary();
            lantern.Move();
        }

        if (timer % interval == 0 && lanternCount < maxLanternCount-1)
        {
            lanternCount += 1;
        }

        if (timer < maxLanternCount * (interval+1))
        {
            timer++;
        }

        if (peachBlossomTimer % interval == 0 && peachBlossomCount < maxPeachBlossomCount - 1)
        {

            peachBlossom = new Lantern(peachBlossomTemplate);
            peachBlossom.vel = Random.insideUnitSphere * 0.05f;
            peachBlossomList.Add(peachBlossom);

            peachBlossomCount += 1;
        }

        if (peachBlossomTimer < maxPeachBlossomCount * (interval + 1))
        {
            peachBlossomTimer += 1;
        }
    }


}
