using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector3 posOld;

    public BoxCollider portalCollider;
    public Transform linkedPortal;
    FPSController controller;
    Camera viewCam;

    void Start()
    {
        viewCam = Camera.main;
        posOld = GetCamNearClipCentre();
        controller = GetComponent<FPSController>();
    }

    Vector3 GetCamNearClipCentre()
    {
        return viewCam.transform.position + viewCam.transform.right * viewCam.nearClipPlane;
    }

    void Update()
    {
        Vector3 posNew = GetCamNearClipCentre();
        Plane plane = new Plane(portalCollider.transform.right, portalCollider.transform.position);
        float colliderDepth = portalCollider.size.z;

        if (!plane.SameSide(posOld, posNew))
        {
            float dstTravelled = (posNew - posOld).magnitude;
            Vector3 dir = (posNew - posOld) / dstTravelled;

            if (portalCollider.Raycast(new Ray(posOld - dir * colliderDepth, dir), out _, dstTravelled + colliderDepth))
            {
                Debug.Log("Went through portal!");
                Vector3 portalOffset = transform.position - portalCollider.transform.position;
                controller.Teleport(linkedPortal.position + portalOffset);
            }
            else
            {
                Debug.Log("Went passed portal");
            }
        }
        posOld = GetCamNearClipCentre();
    }
}