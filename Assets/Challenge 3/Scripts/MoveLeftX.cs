using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    // Скорость движения объекта влево
    public float speed;

    // Ссылка на скрипт PlayerControllerX
    private PlayerControllerX playerControllerScript;

    // Левая граница, за которую объект будет уничтожен
    private float leftBound = -40;

    // Метод, вызываемый при старте
    void Start()
    {
        // Находим объект "Player" и получаем его компонент PlayerControllerX
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Если игра не окончена, перемещаем объект влево
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // Если объект выходит за левую границу и не является фоном, уничтожаем его
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
