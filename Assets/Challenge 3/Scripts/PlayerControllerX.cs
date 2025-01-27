using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    // Флаг, указывающий, окончена ли игра
    public bool gameOver;

    // Сила подъема игрока
    public float floatForce;

    // Модификатор гравитации
    private float gravityModifier = 1.5f;

    // Максимальная высота, на которую может подняться игрок
    private float maxHeight = 14;

    // Компонент Rigidbody игрока
    private Rigidbody playerRb;

    // Эффекты частиц для взрыва и фейерверков
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    // Аудио компонент и звуки
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;

    // Метод, вызываемый при старте
    void Start()
    {
        // Увеличиваем гравитацию
        Physics.gravity *= gravityModifier;

        // Получаем компонент AudioSource
        playerAudio = GetComponent<AudioSource>();

        // Получаем компонент Rigidbody
        playerRb = GetComponent<Rigidbody>();

        // Применяем небольшую силу вверх в начале игры
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Пока нажата клавиша пробела и игрок не достиг максимальной высоты, поднимаем его вверх
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < maxHeight)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    // Метод, вызываемый при столкновении
    private void OnCollisionEnter(Collision other)
    {
        // Если игрок сталкивается с бомбой, взрываемся и устанавливаем gameOver в true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        // Если игрок сталкивается с землей, отскакиваем и проигрываем звук
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * floatForce / 5, ForceMode.Impulse);
            playerAudio.PlayOneShot(boingSound, 1.0f);
        }
        // Если игрок сталкивается с деньгами, запускаем фейерверк и проигрываем звук
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
