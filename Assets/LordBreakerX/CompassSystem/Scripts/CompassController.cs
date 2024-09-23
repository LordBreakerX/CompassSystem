using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LordBreakerX.Utilities;
using UnityEngine.Events;

namespace LordBreakerX.CompassSystem
{
    public class CompassController : MonoBehaviour
    {
        public const float MAX_ALPHA = 255f;

        [SerializeField]
        private float _angleThreshold = 90f;

        [SerializeField]
        private Transform _playerTransform;

        [SerializeField]
        private RectTransform _compassContent;

        [SerializeField]
        private Image _compassIconPrefab;

        [Header("Icon Fading")]
        [SerializeField]
        [Min(0f)]
        private float _minAlpha;

        [SerializeField]
        [Min(0f)]
        private float _maxAlphaDistance;

        [Header("Icon Sizing")]
        [SerializeField]
        [Min(0f)]
        private float _minIconSize = 80f;

        [SerializeField]
        [Min(0f)]
        private float _maxIconSize = 100f;

        [SerializeField]
        [Min(0f)]
        private float _maxSizeDistance = 20f;

        [Header("Direction Icons")]
        [SerializeField]
        private DirectionIcon[] directions;

        [Header("Distance")]
        [SerializeField]
        [Min(0f)]
        private float _maxViewDistance = 20f;

        [Header("Events")]
        [SerializeField]
        public UnityEvent<CompassIcon> _onIconAdded = new UnityEvent<CompassIcon>();

        [SerializeField]
        public UnityEvent<CompassIcon> _onIconRemoved = new UnityEvent<CompassIcon>();

        private Dictionary<string, CompassIcon> _activeIcons = new Dictionary<string, CompassIcon>();

        private List<Image> _disabledIcons = new List<Image>();

        private float _leftPositionX;
        private float _rightPositionX;
        private float _devidedThreshold;

        public float MaxViewDistance { get => _maxViewDistance; }

        private void Awake()
        {
            for(int i = 0; i < directions.Length; i++)
            {
                Image image = Instantiate(_compassIconPrefab, _compassContent, false);
                image.sprite = directions[i].Icon;
                image.gameObject.name = directions[i].Icon.name;
                directions[i].IconImage = image;
            }

            _leftPositionX = (_compassContent.anchoredPosition.x - _compassContent.rect.width *  _compassContent.pivot.x) - _compassIconPrefab.rectTransform.rect.width / 2;
            _rightPositionX = (_compassContent.anchoredPosition.x + _compassContent.rect.width * (1 - _compassContent.pivot.x)) + _compassIconPrefab.rectTransform.rect.width / 2;
            _devidedThreshold = _angleThreshold / 2f;
        }

        // NOTE: this is done in the fixed update since the icons position updating runs more smoothly in fixed update
        private void FixedUpdate()
        {
            if (_activeIcons.Count > 0)
            {
                foreach (var iconPair in _activeIcons)
                {
                    PositionIcon(iconPair.Value.landmarkTransform.position, iconPair.Value.iconImage);
                }
            }

            if (directions.Length > 0)
            {
                foreach (var direction in directions)
                {
                    PositionIcon(new Vector3(direction.OffsetFromTarget.x, 0, direction.OffsetFromTarget.y), direction.IconImage);
                }
            }
        }

        private void Update()
        {
            if (_activeIcons.Count > 0)
            {
                foreach (var iconPair in _activeIcons)
                {
                    FadeIcon(iconPair.Value);
                    SizeIcon(iconPair.Value);
                }
            }

            CheckLandmarksInRange();
        }

        #region Main Compass Methods

        public void AddIcon(string landmarkName, CompassIcon compassIcon)
        {
            if (string.IsNullOrEmpty(landmarkName))
            {
                Debug.LogWarning("Can't add a Icon as the provided landmark name is either null or empty");
            }
            else if (compassIcon.icon == null)
            {
                Debug.LogWarning("Can't add a Icon as the provided compass icon struct has a null icon");
            }
            else if (_activeIcons.ContainsKey(landmarkName))
            {
                Debug.LogWarning($"Can't add a Icon as the compass already has a icon registered with the landmark name of {landmarkName}.");
            }
            else
            {
                Image iconImage;

                if (_disabledIcons.Count > 0)
                {
                    iconImage = _disabledIcons[0];
                    iconImage.gameObject.SetActive(true);
                    _disabledIcons.Remove(iconImage);
                }
                else
                {
                    iconImage = Instantiate(_compassIconPrefab, _compassContent, false);
                }

                iconImage.sprite = compassIcon.icon;
                iconImage.rectTransform.sizeDelta = new Vector2(_maxIconSize, _maxIconSize);
                compassIcon.iconImage = iconImage;
                _activeIcons.Add(landmarkName, compassIcon);
                _onIconAdded.Invoke(compassIcon);
            }
        }

        public void RemoveIcon(string landmarkName)
        {
            if (string.IsNullOrEmpty(landmarkName))
            {
                Debug.LogWarning("Can't remove a Icon as the provided landmark name is either null or empty");
            }
            else if (!_activeIcons.ContainsKey(landmarkName))
            {
                Debug.LogWarning($"Can't remove a Icon as the compass does not have a icon registered with the landmark name of {landmarkName}.");
            }
            else
            {
                _activeIcons[landmarkName].iconImage.gameObject.SetActive(false);
                _disabledIcons.Add(_activeIcons[landmarkName].iconImage);
                _onIconRemoved.Invoke(_activeIcons[landmarkName]);
                _activeIcons.Remove(landmarkName);
            }
        }

        #endregion

        private void CheckLandmarksInRange()
        {
            if (Landmark.Landmarks.Count > 0)
            {
                foreach (Landmark landmark in Landmark.Landmarks)
                {
                    float distance = Vector3.Distance(_playerTransform.position, landmark.transform.position);

                    bool canMakeIconVisible = (distance <= _maxViewDistance || landmark.CanIgnoreDistance()) && landmark.CanShowIcon();
                    ChangeIconVisible(landmark, canMakeIconVisible);

                    if (HasLandmarkIcon(landmark.LandmarkName))
                    {
                        UpdateIconDistance(landmark.LandmarkName, distance);
                    }
                }
            }
        }

        private void ChangeIconVisible(Landmark landmark, bool visible)
        {
            if (visible && !HasLandmarkIcon(landmark.LandmarkName))
            {
                AddIcon(landmark.LandmarkName, landmark.GetCompassIcon());
            }
            else if (!visible && HasLandmarkIcon(landmark.LandmarkName))
            {
                RemoveIcon(landmark.LandmarkName);
            }
        }

        public bool HasLandmarkIcon(string landmarkName)
        {
            return _activeIcons.ContainsKey(landmarkName);
        }

        public void UpdateIconDistance(string landmarkName, float distance)
        {
            if (_activeIcons.ContainsKey(landmarkName))
            {
                CompassIcon icon = _activeIcons[landmarkName];
                icon.UpdateDistance(distance);
                _activeIcons[landmarkName] = icon;
            }
        }

        private void FadeIcon(CompassIcon compassIcon)
        {
            if (compassIcon.CanFade())
            {
                float distance = Vector3.Distance(_playerTransform.position, compassIcon.landmarkTransform.position);

                float percentage = PercentageUtility.InvertedPercentageNormalized(distance, _maxAlphaDistance, _maxViewDistance);
                float currentAlpha = PercentageUtility.MapNormalizedPercentage(percentage, _minAlpha, MAX_ALPHA);
                compassIcon.iconImage.color = new Color(1, 1, 1, currentAlpha / MAX_ALPHA);
            }
        }

        private void SizeIcon(CompassIcon compassIcon)
        {
            if (compassIcon.CanChangeSize())
            {
                float percentage = PercentageUtility.InvertedPercentageNormalized(compassIcon.distanceFromLocator, _maxSizeDistance, _maxViewDistance);
                float currentSize = PercentageUtility.MapNormalizedPercentage(percentage, _minIconSize, _maxIconSize);
                compassIcon.iconImage.rectTransform.sizeDelta = new Vector2(currentSize, currentSize);
            }
        }

        private void PositionIcon(Vector3 iconPosition, Image iconImage)
        {
            Vector3 direction = iconPosition - _playerTransform.position;
            Vector3 planeDirectionFromPlayer = Vector3.ProjectOnPlane(direction, _playerTransform.up);
            float signedAngle = Vector3.SignedAngle(_playerTransform.forward, planeDirectionFromPlayer, _playerTransform.up);

            if (Mathf.Abs(signedAngle) <= _devidedThreshold)
            {
                iconImage.gameObject.SetActive(true);

                float percentage = PercentageUtility.PercentageNormalized(signedAngle + _devidedThreshold, 0f, _angleThreshold);
                float iconPositionX = PercentageUtility.MapNormalizedPercentage(percentage, _leftPositionX, _rightPositionX);

                iconImage.rectTransform.anchoredPosition = new Vector2(iconPositionX, iconImage.rectTransform.anchoredPosition.y);
            }
            else
            {
                iconImage.gameObject.SetActive(false);
            }
        }
    }
}