using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeChangeColor : MonoBehaviour
{
    public Transform camTrans;
    public bool useMouse;

    Color _lerpColor = new Color(1,0.6f,0.6f,1);
    Color _originalColor = new Color(1, 1, 1, 1);
    public float _fadeSpeed;

    int layerMask = 1 << 31;
    float maxDist = 50.0f;
    int state = 0;

    int _colorID;
    SkinnedMeshRenderer _meshRenderer;

    // Hand Interaction
    InteractionSourceState[] interactionSourceStates;

    // Manager Class
    private BannerManager bannerManager;

    private void Awake()
    {
        bannerManager = BannerManager.Instance;
        bannerManager.setInBanner(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _colorID = Shader.PropertyToID("_color");
        InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;

    }

    private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
    {
        switch (state)
        {
            case 1:
                state = -1;
                bannerManager.setInBanner(false);
                break;
            case -1:
                state = -1;
                bannerManager.setInBanner(false);
                break;
            default:
                state = 0;
                bannerManager.setInBanner(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useMouse)
        {
            RayCastBannerMouse();
        }
        else
        {
            RayCastBanner();
        }

    }

    void RayCastBannerMouse()
    {

        Vector3 origin = camTrans.position;
        Vector3 direction = camTrans.forward;



        RaycastHit hit;
        switch (state)
        {
            case 1:
                //enter
                if (_meshRenderer.material.GetColor(_colorID) != _lerpColor)
                {
                    _meshRenderer.material.SetColor(_colorID, Color.Lerp(_meshRenderer.material.GetColor(_colorID), _lerpColor, _fadeSpeed));
                }
                if (!Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                {
                    state = -1;
                    bannerManager.setInBanner(false);
                }
                break;

            case -1:
                //exit
                if (_meshRenderer.material.GetColor(_colorID) != _originalColor)
                {
                    _meshRenderer.material.SetColor(_colorID, Color.Lerp(_meshRenderer.material.GetColor(_colorID), _originalColor, _fadeSpeed));
                }
                if (Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                {
                    _meshRenderer = hit.transform.gameObject.GetComponent<SkinnedMeshRenderer>();
                    state = 1;
                    bannerManager.setCurrentBanner(hit.transform.tag);
                    bannerManager.setInBanner(true);

                }
                break;
            default:
                if (Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                {
                    _meshRenderer = hit.transform.gameObject.GetComponent<SkinnedMeshRenderer>();
                    bannerManager.setCurrentBanner(hit.transform.tag);
                    bannerManager.setInBanner(true);
                    state = 1;
                }
                break;
        }
    }

    void RayCastBanner()
    {
        interactionSourceStates = InteractionManager.GetCurrentReading();


        if (state == -1 && _meshRenderer != null)
        {
            if (_meshRenderer.material.GetColor(_colorID) != _originalColor)
            {
                _meshRenderer.material.SetColor(_colorID, Color.Lerp(_meshRenderer.material.GetColor(_colorID), _originalColor, _fadeSpeed));
            }

        }

        foreach (InteractionSourceState sourceState in interactionSourceStates)
        {
            var headPose = sourceState.headPose;
            Vector3 origin = headPose.position;
            Vector3 direction = headPose.forward;

            RaycastHit hit;

            switch (state)
            {
                case 1:
                    //enter
                    if (_meshRenderer.material.GetColor(_colorID) != _lerpColor)
                    {
                        _meshRenderer.material.SetColor(_colorID, Color.Lerp(_meshRenderer.material.GetColor(_colorID), _lerpColor, _fadeSpeed));
                    }
                    if (!Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                    {
                        state = -1;
                        bannerManager.setInBanner(false);
                    }
                    break;

                case -1:
                    //exit
                    if (_meshRenderer.material.GetColor(_colorID) != _originalColor)
                    {
                        _meshRenderer.material.SetColor(_colorID, Color.Lerp(_meshRenderer.material.GetColor(_colorID), _originalColor, _fadeSpeed));
                    }
                    if (Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                    {
                        _meshRenderer = hit.transform.gameObject.GetComponent<SkinnedMeshRenderer>();
                        state = 1;
                        bannerManager.setCurrentBanner(hit.transform.tag);

                        bannerManager.setInBanner(true);

                    }
                    break;
                default:
                    if (Physics.Raycast(origin, direction, out hit, maxDist, layerMask))
                    {
                        _meshRenderer = hit.transform.gameObject.GetComponent<SkinnedMeshRenderer>();
                        bannerManager.setCurrentBanner(hit.transform.tag);
                        bannerManager.setInBanner(true);
                        state = 1;
                    }
                    break;
            }


        }
    }
}
