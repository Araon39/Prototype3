using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Модификатор гравитации
    [SerializeField] private float _gravityModifier = 1;

    // Интервал увеличения скорости
    [SerializeField] private float _speedIncreaseInterval = 1;

    // Величина увеличения скорости
    [SerializeField] private float _speedIncreaseAmount = 1;

    // Экземпляр GameManager для реализации паттерна Singleton
    public static GameManager instance;

    // Свойства для доступа к приватным полям
    public static float gravityModifier { get { return instance._gravityModifier; } }
    public static float speedIncreaseInterval { get { return instance._speedIncreaseInterval; } }
    public static float speedIncreaseAmount { get { return instance._speedIncreaseAmount; } }

    // Ссылка на компонент PlayerController
    private static PlayerController playerController;

    // Метод, вызываемый при инициализации объекта
    void Awake()
    {
        // Реализация паттерна Singleton
        if (!instance)
        {
            instance = this;
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();

            // Начинаем увеличивать скорость
            IncreaseSpeed();
        }
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Устанавливаем гравитацию в зависимости от модификатора гравитации
        Physics.gravity = Vector3.down * gravityModifier * 10;
    }

    // Метод для увеличения скорости игрока
    void IncreaseSpeed()
    {
        // Проверяем, что playerController существует
        if (playerController)
        {
            // Увеличиваем скорость игрока
            playerController.IncreaseSpeed(speedIncreaseAmount);
        }

        // Планируем следующий вызов метода IncreaseSpeed через заданный интервал
        Invoke("IncreaseSpeed", speedIncreaseInterval);
    }
}
