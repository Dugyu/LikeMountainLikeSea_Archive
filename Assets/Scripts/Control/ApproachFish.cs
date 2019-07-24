using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachFish : MonoBehaviour
{
    public GameObject fishTemplate;
    public GameObject player;
    SensitiveFish sensitiveFish;
    // Start is called before the first frame update
    void Start()
    {
        sensitiveFish = new SensitiveFish(fishTemplate);
        fishTemplate.GetComponent<MeshRenderer>().enabled = false;
        SensitiveFish.AddEnemy(player);
    }

    // Update is called once per frame
    void Update()
    {

        sensitiveFish.Move();
    }
}
