using TMPro;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject objInFirstRow;
    public GameObject objInSecondRow;
    public GameObject notFinalResultObject;
    public GameObject resultObject;

    [Header("Параметры размещения")]
    public bool enableAutoSpacing;
    public float spacing = 2f;              // Вертикальный отступ между объектами
    public float horizontalPadding = 1f;    // Отступ от краев для первого и второго рядов
    public float resultOffset = 3f;          // Дополнительное смещение для результата

    private FirstRowBtnScript[] firstRowObjs;
    private SecondRowBtnScript[] secondRowObjs;
    private NotFinalResultBtnScript notFinalResultBtnScript;
    private ResultBtnScript resultBtnScript;

    private Camera mainCamera;
    private float distanceFromCamera = 10f;

    private int size;
    private int result = 0;
    int[] arr1;
    int[] arr2;

    void CalculateResult()
    {
        for (int i = 0; i < size; i++)
        {
            result += arr1[i] * arr2[i];
        }
    }

    void InitializeRandomMassive(int[] arr)
    {
        for (int i = 0; i < size; i++)
        {
            arr[i] = Random.Range(0, 10);
        }
    }

    void Start()
    {
        if (enableAutoSpacing)
        {
            spacing = objInFirstRow.transform.localScale.y * 1.2f;
        }

        size = DataHolder.countOfObj;
        mainCamera = Camera.main;

        arr1 = new int[size];
        arr2 = new int[size];

        firstRowObjs = new FirstRowBtnScript[size];
        secondRowObjs = new SecondRowBtnScript[size];

        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
        if (distanceFromCamera < 5f) distanceFromCamera = 10f;

        // Заполняем массивы случайными числами
        InitializeRandomMassive(arr1);
        InitializeRandomMassive(arr2);

        // Вычисляем итоговый результат
        CalculateResult();

        // Сортируем массивы в случайном порядке
        ShuffleArray(arr1);
        ShuffleArray(arr2);


        // Располагаем объекты
        PlaceObjects();

        InitializeGameInfoManager();

    }

    void InitializeGameInfoManager()
    {
        GameInfoManager manager = GetComponent<GameInfoManager>();
        if (manager == null)
        {
            Debug.Log("Manager initialize Failed");
            return;
        }
        manager.Initialize(firstRowObjs, secondRowObjs, notFinalResultBtnScript, resultBtnScript);
        DataHolder.gameInfoManager = manager;
    }

    
    void PlaceObjects()
    {
        if (mainCamera == null) return;

        float cameraHeight = 2f * distanceFromCamera * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 centerPoint = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

        // Вычисляем границы всей видимой области
        float leftEdge = -cameraWidth / 2f;
        float rightEdge = cameraWidth / 2f;

        // Граница между 80% и 20% областями
        float splitPoint = leftEdge + cameraWidth * 0.8f;

        // Позиции для рядов с учетом отступов
        float firstRowX = leftEdge + horizontalPadding;           // Первый ряд - с отступом от левого края
        float secondRowX = splitPoint * 0.2f;        // Второй ряд
        float thirdRowX = splitPoint - horizontalPadding; // Третий ряд 
        float resultX = (splitPoint + rightEdge) / 2f + resultOffset; // Результат со смещением


        PlaceFirstRow(centerPoint, firstRowX);
        PlaceSecondRow(centerPoint, secondRowX);
        PlaceThirdRow(centerPoint, thirdRowX);
        PlaceResultRow(centerPoint, resultX);
    }

    void PlaceFirstRow(Vector3 centerPoint, float xPos)
    {
        if (arr1 == null || arr1.Length == 0 || objInFirstRow == null) return;

        for (int i = 0; i < arr1.Length; i++)
        {
            // Распределяем объекты равномерно по вертикали с центром в 0
            // Формула: позиция = (индекс - (длина-1)/2) * spacing
            float yPos = (i - (arr1.Length - 1) / 2f) * spacing;

            GameObject newObj = Instantiate(objInFirstRow,
                centerPoint + new Vector3(xPos, yPos, 0),
                Quaternion.identity);

            FirstRowBtnScript script = newObj.GetComponent<FirstRowBtnScript>();
            if (script != null)
            {
                script.Initialize(arr1[i]);
                firstRowObjs[i] = script;
            }
            else
            {
                Debug.Log("Error while initialize script");
            }
        }
    }

    void PlaceSecondRow(Vector3 centerPoint, float xPos)
    {
        if (arr2 == null || arr2.Length == 0 || objInSecondRow == null) return;

        for (int i = 0; i < arr2.Length; i++)
        {
            // Та же формула для второго ряда
            float yPos = (i - (arr2.Length - 1) / 2f) * spacing;

            GameObject newObj = Instantiate(objInSecondRow,
                centerPoint + new Vector3(xPos, yPos, 0),
                Quaternion.identity);

            SecondRowBtnScript script = newObj.GetComponent<SecondRowBtnScript>();
            if (script != null)
            {
                script.Initialize(arr2[i]);
                secondRowObjs[i] = script;
            }
            else
            {
                Debug.Log("Error while initializing script");
            }
        }
    }

    void PlaceThirdRow(Vector3 centerPoint, float xPos)
    {
        if (resultObject == null) return;

        GameObject newResultObj = Instantiate(notFinalResultObject,
            centerPoint + new Vector3(xPos, 0f, 0),
            Quaternion.identity);

        NotFinalResultBtnScript script = newResultObj.GetComponent<NotFinalResultBtnScript>();

        if (script != null)
        {
            script.Initialize(0);
            notFinalResultBtnScript = script;
        }
        else
        {
            Debug.Log($"Error while initializing script");
        }


            newResultObj.name = "NotFinalResultObject";
        //newResultObj.transform.parent = transform;
    }

    void PlaceResultRow(Vector3 centerPoint, float xPos)
    {
        if (resultObject == null) return;

        GameObject newResultObj = Instantiate(resultObject,
            centerPoint + new Vector3(xPos, 0f, 0),
            Quaternion.identity);

        ResultBtnScript script = newResultObj.GetComponent<ResultBtnScript>();

        if (script != null)
        {
            script.Initialize(result);
            resultBtnScript = script;
        }
        else
        {
            Debug.Log($"Error while initializing script");
        }

        newResultObj.name = "ResultObject";
        //newResultObj.transform.parent = transform;
    }

    void ShuffleArray<T>(T[] array)
    {
        System.Random random = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}