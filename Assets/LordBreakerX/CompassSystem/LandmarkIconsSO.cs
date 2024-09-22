using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordBreakerX.CompassSystem
{
    [CreateAssetMenu()]
    public class LandmarkIconsSO : ScriptableObject
    {
        [System.Serializable]
        public struct LandmarkIcon
        {
            public string iconName;
            public Sprite iconSprite;
        }

        [SerializeField]
        private LandmarkIcon[] _icons = new LandmarkIcon[0];

        public LandmarkIcon[] Icons { get => _icons; }

        public Sprite GetIcon(string iconName)
        {
            Sprite iconSprite = null;

            if (!string.IsNullOrEmpty(iconName))
            {
                foreach (LandmarkIcon icon in _icons)
                {
                    if (icon.iconName == iconName)
                    {
                        iconSprite = icon.iconSprite;
                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Provided icon name is invalid (Can't be null or empty)");
            }

            return iconSprite;
        }
            
        public Sprite GetIcon(int index)
        {
            Sprite icon = null;

            if (_icons != null && _icons.Length > 0)
            {
                if (index >= 0 && index < _icons.Length)
                {
                    icon = _icons[index].iconSprite;
                }
                else
                {
                    Debug.LogError($"provided index of {index} is outside of the range of the icons array (0 to {_icons.Length - 1})");
                }
            }
            else
            {
                Debug.LogError("there are no icons to get");
            }
            return icon;
        }

        public bool HasIcon(string iconName)
        {
            bool hasIcon = false;

            if (!string.IsNullOrEmpty(iconName))
            {
                foreach (LandmarkIcon icon in _icons)
                {
                    if (icon.iconName == iconName)
                    {
                        hasIcon = true;
                        break;
                    }
                }
            }
            return hasIcon;
        }

        public string[] GetIconsNames()
        {
            string[] names = new string[_icons.Length];
            for (int i = 0; i < names.Length; i++) names[i] = _icons[i].iconName;
            return names;
        }
    }
}
