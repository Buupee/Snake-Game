using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0, 1)]
    public float BonesDistance = 0.5f;
    [Range(0, 2)]
    public float HeadToFirstBoneDistance = 1.0f; 
    public GameObject BonePrefab;
    public GameObject FoodPrefab; 

    [Range(0, 4)]
    public float Speed = 0.5f;
    public float SpeedBoost= 2f;
    private Transform _transform;

    public UnityEvent OnEat;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        SpawnFood(); 
    }

    private void Update()
    {
        float currentSpeed = Speed;

        // Увеличиваем скорость при удержании Shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= SpeedBoost;
        }

        MoveSnake(_transform.position + _transform.forward * currentSpeed * Time.deltaTime);

        float angle = Input.GetAxis("Horizontal") * 2.0f;
        _transform.Rotate(0, angle, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float headSqrDistance = HeadToFirstBoneDistance * HeadToFirstBoneDistance;
        float tailSqrDistance = BonesDistance * BonesDistance;

        Vector3 previousPosition = _transform.position;

        for (int i = 0; i < Tails.Count; i++)
        {
            var bone = Tails[i];
            float requiredDistance = (i == 0) ? headSqrDistance : tailSqrDistance;

            if ((bone.position - previousPosition).sqrMagnitude > requiredDistance)
            {
                var tempPosition = bone.position;

                if (i == 0)
                {
                    bone.position = Vector3.Lerp(bone.position, previousPosition - _transform.forward * HeadToFirstBoneDistance, 0.5f);
                }
                else
                {
                    bone.position = Vector3.Lerp(bone.position, previousPosition, 0.5f);
                }

                previousPosition = tempPosition;
            }
            else
            {
                break;
            }
        }

        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверка на столкновение с Food
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);

            // Создаем новый сегмент хвоста
            var bone = Instantiate(BonePrefab);
            if (Tails.Count > 0)
            {
                bone.transform.position = Tails[Tails.Count - 1].position;
            }
            else
            {
                bone.transform.position = _transform.position - _transform.forward * BonesDistance;
            }
            Tails.Add(bone.transform);

            SpawnFood();
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
        }

        // Проверка на столкновение с Body
        if (collision.gameObject.tag == "Body")
        {
            Debug.Log("Голова дотронулась до сегмента тела!");
        }
    }

    private void SpawnFood()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-19f, 19f), 0.55f, Random.Range(-19f, 19f));
        Instantiate(FoodPrefab, randomPosition, Quaternion.identity);
    }
}
