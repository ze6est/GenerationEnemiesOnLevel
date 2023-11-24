using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3[] _path;
        [SerializeField] private float _speed;

        private int _currentPoint;
        private Coroutine _moveToPathJob;

        private void OnValidate() =>
            transform.position = _position;

        private void Start() => 
            _moveToPathJob = StartCoroutine(MoveToPath());

        private void OnDestroy()
        {
            if (_moveToPathJob != null)
                StopCoroutine(_moveToPathJob);
        }

        private IEnumerator MoveToPath()
        {
            while (true)
            {
                Vector3 targetPosition = _path[_currentPoint];

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

                if(transform.position == targetPosition)
                {
                    _currentPoint++;

                    if(_currentPoint >= _path.Length)
                        _currentPoint = 0;
                }

                yield return null;
            }
        }
    }
}