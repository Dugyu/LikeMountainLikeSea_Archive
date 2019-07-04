using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    private Ray ray;
    public Camera cam;
    public GameObject fishTemplate;
    public Fish fish;
    public Shoal shoal;
    // Start is called before the first frame update
    void Start()
    {
        shoal = new Shoal();
        fish = new Fish(fishTemplate);
        Vector3 target = ray.GetPoint(5.0f);
        Vector3 origin = ray.GetPoint(0.5f);
        fish.pos = origin;
        Fish.outterTarget = target;
        shoal.AddFish(fish);
    }
    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
        {
            fish = new Fish(fishTemplate);
 
            Vector3 origin = ray.GetPoint(0.5f);
            fish.pos = origin;
            
            shoal.AddFish(fish);
        }
        Vector3 target = ray.GetPoint(5.0f);
        Fish.outterTarget = target;
        shoal.Run();
    }


}
