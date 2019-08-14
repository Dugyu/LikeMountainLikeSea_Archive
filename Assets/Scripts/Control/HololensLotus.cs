using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
public class HololensLotus : MonoBehaviour
{
    // GameObjects Templates
    public GameObject lotusTemplate;

    // Manager Class
    private EnvManager LotusManager;
    private BannerManager bannerManager;


    int lotusTimer;
    int lotusLoop;
    Vector3 wenyaoBannerOrigin = new Vector3(1.6f,1.08f,0.93f);
    Vector3 boBannerOrigin = new Vector3(1.2f,1.08f,1.58f);

    // Hand Interaction
    private GestureRecognizer recognizer;
    protected bool tapExecuted = false;

    // Tasks
    private readonly Queue<Action> dispatchQueue = new Queue<Action>();

    private void Awake()
    {
        LotusManager = EnvManager.Instance;
        bannerManager = BannerManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.StartCapturingGestures();
        recognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate | GestureSettings.Tap);
        recognizer.Tapped += HandleTap;
        recognizer.ManipulationUpdated += OnManupulationUpdated;
        recognizer.ManipulationStarted += OnManipulationStarted;
        recognizer.ManipulationCompleted += OnManipulationCompleted;
        recognizer.ManipulationCanceled += OnManipulationCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        lock (dispatchQueue)
        {
            if (dispatchQueue.Count > 0)
            {
                dispatchQueue.Dequeue()();
            }
        }


        if (bannerManager.InBanner && bannerManager.CurrentBanner != null)
        {
            string currentBanner = bannerManager.CurrentBanner;

            switch (currentBanner)
            {
                case "wenyao":
                    WenyaoUpdate();
                    break;
                case "bo":
                    BoUpdate();
                    break;
                default:
                    break;
            }
        }


    }

    private void WenyaoUpdate()
    {
        if(lotusTimer % 64 == 0 && lotusLoop < 10)
        {
            Lotus lotusInAir = new Lotus(lotusTemplate, UnityEngine.Random.insideUnitSphere + wenyaoBannerOrigin);
            LotusManager.AddLotus(lotusInAir);
            lotusLoop++;
        }


        if (lotusTimer == 130)
        {
            lotusTimer = 0;

        }
        lotusTimer++;
    }

    private void BoUpdate()
    {
        if (lotusTimer % 64 == 0 && lotusLoop < 10)
        {
            Lotus lotusInAir = new Lotus(lotusTemplate, UnityEngine.Random.insideUnitSphere + boBannerOrigin);
            LotusManager.AddLotus(lotusInAir);
            lotusLoop++;
        }
        if (lotusTimer == 130)
        {
            lotusTimer = 0;

        }
        lotusTimer++;
    }


    private void HandleTap(TappedEventArgs tapEvent)
    {

        // Construct a Ray using forward direction of the HoloLens.
        Ray HeadRay = new Ray(tapEvent.headPose.position, tapEvent.headPose.forward);
        Vector3 PointInAir = HeadRay.GetPoint(2.0f);

        Lotus lotusInAir = new Lotus(lotusTemplate, PointInAir);

        LotusManager.AddLotus(lotusInAir);

    }


    private void OnManupulationUpdated(ManipulationUpdatedEventArgs obj)
    {
        throw new NotImplementedException();
    }
    private void OnManipulationStarted(ManipulationStartedEventArgs obj)
    {
        throw new NotImplementedException();
    }

    private void OnManipulationCanceled(ManipulationCanceledEventArgs obj)
    {
        throw new NotImplementedException();
    }

    private void OnManipulationCompleted(ManipulationCompletedEventArgs obj)
    {
        throw new NotImplementedException();
    }


    protected void QueueOnUpdate(Action updateAction)
    {
        lock (dispatchQueue)
        {
            dispatchQueue.Enqueue(updateAction);
        }
    }
}
