using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    
    private float _movement;
    private const float Speed = 15.0f;
    private bool _jointed = true;
    private Joint2D _joint2D;

    // Start is called before the first frame update
    private void Awake()
    {
        _joint2D = player.GetComponent<Joint2D>();
    }

    void Start()
    {
        player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _joint2D.enabled = false;
            _jointed = false;
        }
    }

    void FixedUpdate()
    {
        _movement = Input.GetAxis("Horizontal");
        var position = player.position;
        float moveX = (position.x + (_movement * Time.deltaTime * Speed));
        player.MovePosition(new Vector2(moveX, position.y));
    }
}
