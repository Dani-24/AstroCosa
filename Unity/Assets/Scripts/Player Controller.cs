using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float shootingCooldown = 1f;
    float shootingCont = 0;

    [SerializeField] float bulletSpeed = 10;

    InputAction moveAction;
    InputAction shootAction;

    [SerializeField] GameObject bulletPrefab;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");

        shootingCont = shootingCooldown;

        GameManagerScript.Instance.playerInstance = gameObject;
    }

    void Update()
    {
        // Movement
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 pos = transform.position;
        pos += speed * Time.deltaTime * new Vector3(moveValue.x, 0f, 0f);

        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        float minX = Camera.main.transform.position.x - camWidth / 2f;
        float maxX = Camera.main.transform.position.x + camWidth / 2f;

        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        pos.x = Mathf.Clamp(pos.x, minX + halfWidth, maxX - halfWidth);

        transform.position = pos;

        // Shooting
        if (shootingCont >= shootingCooldown && shootAction.IsPressed())
        {
            shootingCont = 0;

            GameObject newBullet = Instantiate(bulletPrefab, transform);
            newBullet.GetComponent<Bullet>().speed = bulletSpeed;
            newBullet.transform.parent = null;
        }
        else
        {
            shootingCont += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Receive DMG
        Debug.Log($"Player Collision DMG from {collision.gameObject.name}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Player received BULLET DMG from {collision.gameObject.name}");

        Destroy(collision.gameObject);
    }
}
