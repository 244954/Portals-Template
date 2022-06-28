using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    RenderTexture renderTexture;
    public Portal linkedPortal;
    public MeshRenderer portalMesh;
    Camera playerCam;
    Camera portalCam;

    void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetCameraTexture(RenderTexture targetTexture)
    {
        portalCam.targetTexture = targetTexture;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateTexture();
        Vector3 playerOffsetToLinkedPortal = playerCam.transform.position - linkedPortal.transform.position;

        portalCam.transform.position = transform.position + playerOffsetToLinkedPortal;
        portalCam.transform.rotation = playerCam.transform.rotation;
    }

    void UpdateTexture()
    {
        if(renderTexture == null)
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalMesh.material.SetTexture("_MainTex", renderTexture);
            linkedPortal.SetCameraTexture(renderTexture);
        }
    }
}
