using UnityEngine;
using UnityEngine.UI;

namespace LordBreakerX.CompassSystem
{
    [System.Serializable]
    public struct DirectionIcon
    {
        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private Vector2 _offsetFromOrigin;

        public Sprite Icon { get => _icon; }
        public Vector2 OffsetFromTarget { get => _offsetFromOrigin; }

        public Image IconImage { get; set; }
    }
}
