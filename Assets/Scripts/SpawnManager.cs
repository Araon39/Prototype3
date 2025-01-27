using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Префаб препятствия, который будет создаваться
    [SerializeField] private GameObject obstaclePrefab;

    // Позиция, в которой будет создаваться препятствие
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);

    // Минимальная задержка перед созданием следующего препятствия
    [SerializeField] private float spawnDelayMin = 0;

    // Максимальная задержка перед созданием следующего препятствия
    [SerializeField] private float spawnDelayMax = 0;

    // Ссылка на компонент PlayerController
    private PlayerController playerController;

    // Метод, вызываемый при старте
    void Start()
    {
        // Находим объект "Player" и получаем его компонент PlayerController
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // Начинаем процесс создания препятствий
        SpawnObstacle();
    }

    // Метод для создания препятствия
    void SpawnObstacle()
    {
        // Проверяем, что префаб препятствия и playerController существуют, и игра не окончена
        if (obstaclePrefab && playerController && !playerController.isGameOver)
        {
            // Создаем препятствие в заданной позиции с ротацией префаба
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);

            // Планируем следующий вызов метода SpawnObstacle через случайное время в диапазоне от spawnDelayMin до spawnDelayMax
            Invoke("SpawnObstacle", Random.Range(spawnDelayMin, spawnDelayMax));
        }
    }
}
