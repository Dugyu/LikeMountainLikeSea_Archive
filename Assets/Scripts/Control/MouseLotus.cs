using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLotus : MonoBehaviour
{
    // GameObjects Templates
    public Camera cam;
    public GameObject lotusTemplate;


    // Manager Class
    private EnvManager lotusCloverManager;


    private void Awake()
    {
        lotusCloverManager = EnvManager.Instance;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 PointInAir = ray.GetPoint(2.0f);

                Lotus lotusInAir = new Lotus(lotusTemplate, PointInAir);

                lotusCloverManager.AddLotus(lotusInAir);
            }
        }

    }


}
