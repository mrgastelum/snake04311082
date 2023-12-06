using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;

    private List<Transform> _snakeSpawn;
    public Transform snakePrefab;

    private Color snakeColor = Color.white; // Color inicial de la serpiente

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);

        _snakeSpawn = new List<Transform>();
        _snakeSpawn.Add(this.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0, moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0, -moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _snakeSpawn.Count - 1; i > 0; i--)
        {
            _snakeSpawn[i].position = _snakeSpawn[i - 1].position;
        }

        // Asigna el color a cada segmento de la serpiente
        foreach (var segment in _snakeSpawn)
        {
            segment.GetComponent<SpriteRenderer>().color = snakeColor;
        }
    }

    private void grow()
    {
        Transform snakeSpawn = Instantiate(this.snakePrefab);
        snakeSpawn.position = _snakeSpawn[_snakeSpawn.Count - 1].position;
        _snakeSpawn.Add(snakeSpawn);

        // Cambia el color cada vez que la serpiente crece
        snakeColor = new Color(Random.value, Random.value, Random.value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            grow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
