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
        posOld = viewCam.transform.position;
        controller = GetComponent<FPSController>();
    }

    void Update()
    {
        Vector3 posNew = viewCam.transform.position;
        Plane plane = new Plane(portalCollider.transform.right, portalCollider.transform.position);
        float colliderDepth = portalCollider.size.x;

        if (!plane.SameSide(posOld, posNew))
        {
            float dstTravelled = (posNew - posOld).magnitude;
            Vector3 dir = (posNew - posOld) / dstTravelled;

            if (portalCollider.Raycast(new Ray(posOld - dir * colliderDepth, dir), out _, dstTravelled + colliderDepth))
            {
                Vector3 portalOffset = transform.position - portalCollider.transform.position;
                controller.Teleport(linkedPortal.position + portalOffset);

            }
        }
        posOld = viewCam.transform.position;
    }
}