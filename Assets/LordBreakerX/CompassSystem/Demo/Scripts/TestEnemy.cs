using LordBreakerX.CompassSystem;
using UnityEngine;
using UnityEngine.AI;

namespace LordBreakerX
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(NavMeshAgent))]
    public class TestEnemy : Landmark
    {
        [SerializeField]
        private float _maxDistance = 10;

        private bool _isAgroed = false;
        private NavMeshAgent _agent;

        private Transform _player;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            print(CanShowIcon());

            if (SimpleFPSController.Instance == null) return;

            if (Vector3.Distance(SimpleFPSController.Instance.transform.position, transform.position) < _maxDistance)
            {
                _isAgroed = true;
                _agent.SetDestination(SimpleFPSController.Instance.transform.position);
            }
            else
            {
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

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(transform.position, _maxDistance / 2);
        }
    }
}
