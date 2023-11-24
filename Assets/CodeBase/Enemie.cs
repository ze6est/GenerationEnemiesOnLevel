using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class Enemie : MonoBehaviour
    {
        [SerializeField] private EnemieType _enemieType;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;

        private Coroutine _moveJob;

        public EnemieType EnemieType => _enemieType;

        public void Init(Transform target) =>
            _target = target;

        private void Start() =>
            _moveJob = StartCoroutine(MoveToTarget());

        private void OnDestroy()
        {
            if (_moveJob != null)
                StopCoroutine(_moveJob);
        }

        private IEnumerator MoveToTarget()
        {
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}