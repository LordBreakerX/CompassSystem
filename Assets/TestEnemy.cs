using LordBreakerX.CompassSystem;
using UnityEngine;
using UnityEngine.AI;

namespace LordBreakerX
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(NavMeshAgent))]
    public class TestEnemy : Landmark
    {
        private bool _isAgroed = false;
        private NavMeshAgent _agent;

        private Transform _player;

        private void Awake()
        {
            _landmarkName = _landmarkName + GetInstanceID();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isAgroed && _player != null)
            {
                _agent.SetDestination(_player.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _player == null)
            {
                _player = other.transform;
                _isAgroed = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && _player != null && _player == other.transform)
            {
                _player = null;
                _isAgroed = false;
            }
        }

        public override bool CanChangeIconSize()
        {
            return true;
        }

        public override bool CanFadeIcon()
        {
            return false;
        }

        public override bool CanIgnoreDistance()
        {
            return true;
        }

        public override bool CanShowIcon()
        {
            return _isAgroed || _agent.velocity.magnitude > 0.1f;
        }
    }
}
