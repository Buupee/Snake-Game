using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class SnakeCntrl : MonoBehaviour
{
    // Настройки
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 20;
    public int Score = 0;
    // Ссылки(References)
    public GameObject BodyPrefab;
    public GameObject FoodPrefab; 
    public UnityEvent OnEat;
    public TextMeshProUGUI ScoreText;
    public GameObject EndText;
    // Списки
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    private void Awake() {
        EndText.SetActive(false) ;

    }



    // Start вызывается перед первым кадром обновления
    void Start() {
        SpawnFood(); 
        GrowSnake();
    }



    // FixedUpdate вызывается один раз за кадр
    void FixedUpdate() {

        // Движение вперед
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Поворот
        float steerDirection = Input.GetAxis("Horizontal"); 
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // Сохранение истории позиций
        PositionsHistory.Insert(0, transform.position);

        // Движение частей тела
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Движение части тела к точке по пути змеи
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Поворот части тела к точке по пути змеи
            body.transform.LookAt(point);

            index++;
        }
    }


    private void OnCollisionEnter(Collision collision){
        // Проверка на столкновение с Food
        if (collision.gameObject.tag == "Food"){
            Destroy(collision.gameObject);
            GrowSnake();
            SpawnFood();
            Score++;
            ScoreText.text="Score:"+Score.ToString();
            if (OnEat != null){
                OnEat.Invoke(); 
            }
        }
        
        // Проверка на столкновение с Body
        if (collision.gameObject.tag == "Body"){
            EndText.SetActive(true) ;
            Debug.Log("Dotron");
            MoveSpeed=0;
            BodySpeed=0;
            SteerSpeed=0;
        }
    }


    private void GrowSnake() {
        // Создание экземпляра части тела и
        // добавление её в список
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }


    // Генерация случайной позиции для еды
    private void SpawnFood(){
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-19f, 19f), 0.55f, UnityEngine.Random.Range(-19f, 19f));
        Instantiate(FoodPrefab, randomPosition, Quaternion.identity);
    }
}
