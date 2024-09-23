using UnityEngine;
using UnityEngine.UI;

namespace LordBreakerX.CompassSystem
{
    public struct CompassIcon
    {
        public Image iconImage;
        public Sprite icon;
        public Transform landmarkTransform;
        public float distanceFromLocator;

        private Landmark _landmark;

        public CompassIcon(Landmark landmark)
        {
            _landmark = landmark;
            icon = _landmark.Icons.GetIcon(_landmark.SelectedIndex);
            landmarkTransform = _landmark.transform;
            iconImage = null;
            distanceFromLocator = 0;
        }

        public void UpdateDistance(float distance)
        {
            distanceFromLocator = distance;
        }

        public bool CanFade()
        {
            return _landmark.CanFadeIcon();
        }

        public bool CanChangeSize()
        {
            return _landmark.CanChangeIconSize();
        }
    }
}
