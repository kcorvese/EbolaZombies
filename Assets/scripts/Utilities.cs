using UnityEngine;
using System.Collections;

namespace Utilities
{
    class RayCast
    {
        public static RaycastHit Raycast(Transform from, int dist)
        {
            RaycastHit hit;

            Ray ray = new Ray(from.position, from.forward);

            Physics.Raycast(ray, out hit, dist);
            Debug.Log(ray.direction);
            Debug.DrawLine(ray.origin, ray.origin + new Vector3(0, 100, 0));

            return hit;
        }

        public static RaycastHit Raycast(Transform from, Transform to, int dist)
        {
            RaycastHit hit;

            Vector3 v = (to.position - from.position);
            Vector3 dir = v / v.magnitude;

            Ray ray = new Ray(from.position, dir);

            Physics.Raycast(ray, out hit, dist);
            Debug.Log(ray.direction);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction);

            return hit;
        }

        public static RaycastHit[] RaycastAll(int dist)
        {
            Transform camera = Camera.main.transform;

            Ray ray = new Ray(camera.position, camera.forward);

            Debug.DrawRay(ray.origin, ray.direction);

            return Physics.RaycastAll(ray, dist);
        }
    }

    class Timer
    {
		public float end;
		public float period;
		private float startTime;
		
		private bool stopped = true;
		
		public Timer(float period)
		{
			this.period = period;
		}
		
		public void start()
		{
			stopped = false;
			this.startTime = Time.timeSinceLevelLoad;
			this.end = startTime + period;
		}
		
		public void reset()
		{
			startTime = 0;
			end = 0;
			startTime = Time.timeSinceLevelLoad;
			end = startTime + period;
		}
		
		public void stop()
		{
			stopped = true;
		}
		
		public float getStart()
		{
			return startTime;
		}
		
		public float getElapsed()
		{
			return Time.timeSinceLevelLoad - startTime;
		}
		
		public float getRemaining()
		{
			if (isRunning() && !stopped)
			{
				return end - Time.timeSinceLevelLoad;
			}
			return 0;
		}
		
		public bool isStopped()
		{
			return stopped;
		}
		
		private bool isRunning()
		{
			return Time.timeSinceLevelLoad < end;
		}
    }
}