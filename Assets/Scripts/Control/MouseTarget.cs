using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    private Ray ray;
    public Camera cam;
    public GameObject fishTemplate;
    public Fish fish;
    // Start is called before the first frame update
    void Start()
    {
        fish = new Fish(fishTemplate);
        fishTemplate.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 target = ray.GetPoint(10.0f);
        fish.target = target;
        fish.Move();
    }
}
