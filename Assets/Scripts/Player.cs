using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private ContactFilter2D _contactFilter2D;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private float raycastSizeY = 0.2f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        Vector3 originBox = new Vector3(transform.position.x, transform.position.y - raycastSizeY / 2, transform.position.z);
        Vector2 boxSize = new Vector2(_boxCollider2D.size.x, raycastSizeY);

        Physics2D.BoxCast(originBox, boxSize, 0f, Vector2.zero, _contactFilter2D, results);

        if(results.Count > 0)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 newPosition = new Vector3(-_speed * Time.deltaTime, 0f, transform.position.z);
            transform.Translate(newPosition);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 newPosition = new Vector3( _speed * Time.deltaTime, 0f, transform.position.z);
            transform.Translate(newPosition);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - raycastSizeY/2, transform.position.z), new Vector2(_boxCollider2D.size.x, raycastSizeY));
    }
}
