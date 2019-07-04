using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Input;

public class HololensTarget : MonoBehaviour
{

    public Camera cam;
    public GameObject fishTemplate;
    public Fish fish;
    public Shoal shoal;

    private readonly Queue<Action> dispatchQueue = new Queue<Action>();


    // Hand Interaction
    private GestureRecognizer recognizer;
    protected bool tapExecuted = false;





    // Start is called before the first frame update
    void Start()
    {
        shoal = new Shoal();
        fish = new Fish(fishTemplate);
        Vector3 target = new Vector3(0.0f,0.0f,5.0f);
        Vector3 origin = new Vector3(0.0f, -0.5f,0.85f);
        fish.pos = origin;
        Fish.outterTarget = target;
        shoal.AddFish(fish);

        recognizer = new GestureRecognizer();
        recognizer.StartCapturingGestures();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += HandleTap;
    }


    // Update is called once per frame
    void Update()
    {
        shoal.Run();
        lock (dispatchQueue)
        {
            if (dispatchQueue.Count > 0)
            {
                dispatchQueue.Dequeue()();
            }
        }
    }


    private void HandleTap(TappedEventArgs tapEvent)
    {
        // Construct a Ray using forward direction of the HoloLens.
        Ray HeadRay = new Ray(tapEvent.headPose.position, tapEvent.headPose.forward);
        Vector3 target = HeadRay.GetPoint(3.0f);
        Vector3 origin = HeadRay.GetPoint(0.5f);
        Fish.outterTarget = target;

        QueueOnUpdate(() =>
        {
            Debug.Log("Adding fish to shoal...");
            fish = new Fish(fishTemplate);
            fish.pos = origin;
            shoal.AddFish(fish);
        });

    }

    protected void QueueOnUpdate(Action updateAction)
    {
        lock (dispatchQueue)
        {
            dispatchQueue.Enqueue(updateAction);
        }
    }
}
