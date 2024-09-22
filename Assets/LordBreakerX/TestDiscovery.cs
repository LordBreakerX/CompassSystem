using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordBreakerX.CompassSystem
{
    public class TestDiscovery : MonoBehaviour
    {
        public struct DiscoveryIcon
        {
            public CompassIcon compassIcon;
            public bool discovered;

            public DiscoveryIcon(CompassIcon compassIcon, bool discovered = false)
            {
                this.compassIcon = compassIcon;
                this.discovered = discovered;
            }
        }

        [SerializeField]
        [Min(0f)]
        private float _maxDiscoverDistance = 10f;

        [SerializeField]
        private CompassController _compassController;

        private List<DiscoveryIcon> _icons = new List<DiscoveryIcon>();

        private void OnEnable()
        {
            _compassController._onIconAdded.AddListener(OnIconAdded);
        }

        private void OnDisable()
        {
            _compassController._onIconAdded.RemoveListener(OnIconAdded);
        }

        public void OnIconAdded(CompassIcon compassIcon)
        {
            DiscoveryIcon discoveryIcon = new DiscoveryIcon(compassIcon);

            if (!_icons.Contains(discoveryIcon))
            {
                _icons.Add(discoveryIcon);
            }   
        }

        private void Update()
        {
            if (_icons.Count > 0)
            {
                for(int iconIndex = 0; iconIndex < _icons.Count; iconIndex++)
                {
                    DiscoveryIcon icon = _icons[iconIndex];

                    if (!icon.discovered && (Vector3.Distance(icon.compassIcon.landmarkTransform.position, transform.position) < _maxDiscoverDistance))
                    {
                        _icons[iconIndex] = new DiscoveryIcon(icon.compassIcon, true);
                        Debug.Log($"icon was discovered with the icon {icon.compassIcon.iconImage.sprite.name}");
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawSphere(transform.position, _maxDiscoverDistance / 2);
        }
    }
}
