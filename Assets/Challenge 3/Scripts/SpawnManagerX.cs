using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    // Массив префабов объектов, которые будут создаваться
    public GameObject[] objectPrefabs;

    // Задержка перед первым созданием объекта
    private float spawnDelay = 2;

    // Интервал между созданием объектов
    private float spawnInterval = 1.5f;

    // Ссылка на скрипт PlayerControllerX
    private PlayerControllerX playerControllerScript;

    // Метод, вызываемый при старте
    void Start()
    {
        // Запускаем повторяющийся вызов метода SpawnObjects с задержкой и интервалом
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);

        // Находим объект "Player" и получаем его компонент PlayerControllerX
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Метод для создания объектов
    void SpawnObjects()
    {
        // Устанавливаем случайное место создания и случайный индекс объекта
        Vector3 spawnLocation = new Vector3(40, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // Если игра еще активна, создаем новый объект
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
