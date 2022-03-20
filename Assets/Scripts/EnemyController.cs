using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float rateOfFire = 1;

    private float _valueX, _valueY;

    private bool _canFire = true;

    private float _maxLimit, _lowerLimit, _direction, _amplitude;


    private void OnEnable()
    {
        _amplitude = Random.Range(1f, 2f);
        _maxLimit = 2.5f;
        _lowerLimit = -2.5f;
        _direction = 0.01f;
        _canFire = true;
    }

    private void Update()
    {
        if (_canFire) StartCoroutine(Fire());
        Move();
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    // <summary>
    /// Moves the player in s sinusoidal manner
    /// </summary>
    private void Move()
    {
        var enemyTransform = transform;
        enemyTransform.position =
            enemyTransform.position + new Vector3(Vector3.left.x * speed * Time.deltaTime, _direction, 0);
        if (transform.position.y >= _maxLimit) _direction = -0.01f * _amplitude;
        if (transform.position.y <= _lowerLimit) _direction = 0.01f * _amplitude;
    }

    // <summary>
    /// Checks if the enemy can fire and fires the missiles
    /// </summary>
    private IEnumerator Fire()
    {
        _canFire = false;
        var missileGo = MissilePooling.MissilePoolInstance.GetPooledEnemyMissile();
        if (missileGo != null)
        {
            missileGo.transform.position = bulletSpawn.position;
            missileGo.transform.rotation = Quaternion.identity;
            missileGo.SetActive(true);
        }

        yield return new WaitForSeconds(1f / rateOfFire);
        _canFire = true;
    }
}