using System.Collections;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    private Coroutine _moveJob;
    private Vector3 _direction;

    public void Init(Vector3 direction) => 
        _direction = direction;

    private void Start() => 
        _moveJob = StartCoroutine(Move());

    private void OnDestroy()
    {
        if(_moveJob != null )
            StopCoroutine(_moveJob);
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(_direction * Time.deltaTime);
            yield return null;
        }        
    }
}