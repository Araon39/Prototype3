using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    // Начальная позиция фона
    private Vector3 startPos;

    // Ширина фона, после которой он будет повторяться
    private float repeatWidth;

    // Метод, вызываемый при старте
    private void Start()
    {
        // Устанавливаем начальную позицию фона
        startPos = transform.position;

        // Устанавливаем ширину фона, используя размер спрайта, и делим её на два для получения половины ширины
        repeatWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        // Если фон сместился влево на расстояние, равное его ширине, возвращаем его в начальную позицию
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}


