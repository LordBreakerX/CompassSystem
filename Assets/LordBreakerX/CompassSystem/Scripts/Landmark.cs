using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordBreakerX.CompassSystem
{
    public class Landmark : MonoBehaviour
    {
        [SerializeField]
        protected string _landmarkName;

        [SerializeField]
        private LandmarkIconsSO _icons;

        [SerializeField]
        private int _selectedIndex;

        public string LandmarkName { get => _landmarkName; }
        public LandmarkIconsSO Icons { get => _icons; }
        public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; } }

        public static List<Landmark> Landmarks { get; private set; } = new List<Landmark>();

        private void OnEnable()
        {
            Landmarks.Add(this);
        }

        private void OnDisable()
        {
            if (Landmarks.Contains(this)) Landmarks.Remove(this);
        }

        public CompassIcon GetCompassIcon()
        {
            return new CompassIcon(this);
        }

        public virtual bool CanIgnoreDistance()
        {
            return false;
        }

        public virtual bool CanShowIcon()
        {
            return true;
        }

        public virtual bool CanFadeIcon()
        {
            return true;
        }

        public virtual bool CanChangeIconSize()
        {
            return true;
        }
    }

}
