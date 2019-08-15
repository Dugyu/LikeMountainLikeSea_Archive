using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusBloom : MonoBehaviour
{
    public GameObject lotusTemplate;
    public List<GameObject> lotusGroup;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 frontPoint = new Vector3(0.0F, -1.0F, 0.0F);
        for (int i = 0; i<200; i++)
        {
            GameObject lotus = Instantiate(lotusTemplate);

            float r = Random.Range(0.5f, 8.0f);
            float rad = Random.Range(0.0f, Mathf.PI * 2);

            float x = Mathf.Cos(rad) * r + frontPoint.x;
            float z = Mathf.Sin(rad) * r + frontPoint.z;
            float y = Random.Range(-0.5f, -0.25f);
            lotus.transform.position = new Vector3(x, y, z);
            float s = Random.Range(0.025f, 0.1f);
            lotus.transform.localScale = new Vector3(s, s, s);
            lotus.transform.Rotate(Vector3.up, Random.Range(0.0f, Mathf.PI));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
