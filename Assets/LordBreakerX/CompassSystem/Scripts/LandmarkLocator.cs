using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordBreakerX.CompassSystem
{
    public class LandmarkLocator : MonoBehaviour
    {
        [SerializeField]
        private CompassController _compass;

        private void Update()
        {
            CheckLandmarksInRange();
        }

        private void CheckLandmarksInRange()
        {
            if (Landmark.Landmarks.Count > 0)
            {
                foreach (Landmark landmark in Landmark.Landmarks)
                {
                    float distance = Vector3.Distance(transform.position, landmark.transform.position);

                    bool canMakeIconVisible = (distance <= _compass.MaxViewDistance || landmark.CanIgnoreDistance()) && landmark.CanShowIcon();
                    ChangeIconVisible(landmark, canMakeIconVisible);

                    if (_compass.HasLandmarkIcon(landmark.LandmarkName))
                    {
                        _compass.UpdateIconDistance(landmark.LandmarkName, distance);
                    }
                }
            }
        }

        private void ChangeIconVisible(Landmark landmark, bool visible)
        {
            if (visible && !_compass.HasLandmarkIcon(landmark.LandmarkName))
            {
                _compass.AddIcon(landmark.LandmarkName, landmark.GetCompassIcon());
            }
            else if (!visible && _compass.HasLandmarkIcon(landmark.LandmarkName))
            {
                _compass.RemoveIcon(landmark.LandmarkName);
            }
        }

        private void OnDrawGizmos()
        {
            if (_compass != null)
            {
                Gizmos.color = new Color(0, 0, 1, 0.3f);
                Gizmos.DrawWireSphere(transform.position, _compass.MaxViewDistance);
            }
        }
    }
}
