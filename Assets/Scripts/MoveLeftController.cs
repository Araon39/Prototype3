using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftController : MonoBehaviour
{
    // Ссылка на компонент PlayerController
    private PlayerController playerController;

    // Метод, вызываемый при старте
    void Start()
    {
        // Находим объект "Player" и получаем его компонент PlayerController
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Проверяем, что playerController существует и игра не окончена
        if (playerController && !playerController.isGameOver)
        {
            // Перемещаем объект влево с учетом скорости игрока и времени кадра
            transform.Translate(Vector3.left * playerController.GetSpeed() * Time.deltaTime);
        }
    }
}
