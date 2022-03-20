using System.Collections;
using UnityEngine;

// <summary>
/// This class manages the player ship
/// </summary>
public class ShipController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int rateOfFire = 2;
    [SerializeField] private Transform missileSpawnPoint;

    private Vector3 _startPos;
    private Vector3 _playerPos;
    private bool _canFire = true;
    private bool _canMove = true;

    private void Start()
    {
        _startPos = transform.position;
        GameManager.PInstance.onGameStart += GameStart;
        GameManager.PInstance.onGameEnd += GameEnd;
    }

    // <summary>
    /// Update is called every frame and it handles movement and firing of missiles
    /// </summary>
    private void Update()
    {
        if (_canMove)
        {
            Move();
        }

        if (Input.GetKey(KeyCode.Space) && _canFire)
        {
            StartCoroutine(Fire());
        }
    }

    // <summary>
    /// Checks collision with enemies
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        GameManager.PInstance.OnPlayerDestroyed();
    }

    // <summary>
    /// Resets the position on Game Start
    /// </summary>
    private void GameStart()
    {
        ResetPosition();
        _canMove = true;
        _canFire = true;
    }

    private void GameEnd()
    {
        _canMove = false;
        _canFire = false;
    }

    // <summary>
    /// Checks if the player can fire and fires the missile
    /// </summary>
    private IEnumerator Fire()
    {
        _canFire = false;
        var missileGo = MissilePooling.MissilePoolInstance.GetPooledPlayerMissile();
        if (missileGo != null)
        {
            missileGo.transform.position = missileSpawnPoint.position;
            missileGo.transform.rotation = Quaternion.identity;
            missileGo.SetActive(true);
        }

        yield return new WaitForSeconds(1f / rateOfFire);
        _canFire = true;
    }

    private void Move()
    {
        _playerPos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _playerPos.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = _playerPos;
    }

    private void ResetPosition()
    {
        transform.position = _startPos;
    }
}