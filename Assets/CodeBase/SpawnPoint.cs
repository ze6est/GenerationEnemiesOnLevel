using UnityEngine;

namespace Assets.CodeBase
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private EnemieType _enemieType;

        private Transform _target;

        public Transform Target => _target;
        public EnemieType EnemieType => _enemieType;

        private void OnValidate()
        {
            transform.position = _position;

            _target = GetComponentInChildren<Target>().transform;
        }
    }
}