using UnityEngine;
using System.Collections;

namespace Utilities
{
    class RayCast
    {
        public static RaycastHit Raycast()
        {
            RaycastHit hit;

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            Physics.Raycast(ray, out hit, 100);
            Debug.DrawRay(ray.origin, ray.direction);

            return hit;
        }

        public static RaycastHit[] RaycastAll()
        {
            Transform camera = Camera.main.transform;

            Ray ray = new Ray(camera.position, camera.forward);

            Debug.DrawRay(ray.origin, ray.direction);

            return Physics.RaycastAll(ray, 100);
        }
    }

    class Timer
    {

    }
}