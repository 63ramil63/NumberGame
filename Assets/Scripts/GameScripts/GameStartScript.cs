using TMPro;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject backGround;
    public GameObject objInFirstRow;
    public GameObject objInSecondRow;
    public GameObject notFinalResultObject;
    public GameObject resultObject;

    [Header("Параметры размещения")]
    public float spacing;              // Вертикальный отступ между объектами

    [Header("Настройки адаптации под размер")]
    public float availableHeightRatio;   // доля высоты экрана, занимаемая колонкой (0.9 = 90%)
    public float minObjectHeight;        // минимальный допустимый размер объекта
    public float maxObjectHeight;        // максимальный допустимый размер объекта
    public float desiredGap;              // желаемый зазор между объектами

    [Header("Настройки горизонтального размещения")]
    public float firstRowNormX;   // позиция первого ряда (0 = левый край, 1 = правый край)
    public float secondRowNormX;  // позиция второго ряда
    public float thirdRowNormX;   // позиция третьего ряда (нефинальный результат)
    public float resultNormX;      // позиция результата

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

    private void CalculateResult()
    {
        for (int i = 0; i < size; i++)
        {
            result += arr1[i] * arr2[i];
        }
    }

    private void InitializeRandomMassive(int[] arr)
    {
        for (int i = 0; i < size; i++)
        {
            arr[i] = Random.Range(0, 10);
        }
    }

    private void InitializeTransformForObj()
    {
        spacing = DataHolder.spacing;

        if (DataHolder.countOfObj < 4)
        {
            availableHeightRatio = 0.6f;
        } else
        {
            availableHeightRatio = DataHolder.availableHeightRatio;
        }
        minObjectHeight = DataHolder.minObjectHeight;
        maxObjectHeight = DataHolder.maxObjectHeight;
        desiredGap = DataHolder.desiredGap;

        firstRowNormX = DataHolder.firstRowNormX;
        secondRowNormX = DataHolder.secondRowNormX;
        thirdRowNormX = DataHolder.thirdRowNormX;
        resultNormX = DataHolder.resultNormX;
    }

    private void Start()
    {

        InitializeTransformForObj();

        DataHolder.isGameActive = true;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;

        size = DataHolder.countOfObj;
        mainCamera = Camera.main;

        arr1 = new int[size];
        arr2 = new int[size];

        firstRowObjs = new FirstRowBtnScript[size];
        secondRowObjs = new SecondRowBtnScript[size];

        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
        if (distanceFromCamera < 5f) distanceFromCamera = 10f;

        AdaptScaleToCount();

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

        if (DataHolder.backgrounds != null && DataHolder.backgrounds.Length > 0)
        {
            backGround.GetComponent<SpriteRenderer>().sprite = DataHolder.backgrounds[Random.Range(0, DataHolder.backgrounds.Length)];
        }

        CameraView.ScaleToFillCamera(backGround, Camera.main, false);
    }

    private void InitializeGameInfoManager()
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

    private float GetCameraHeight()
    {
        if (mainCamera.orthographic)
        {
            // Для ортографической камеры высота = 2 * orthographicSize
            return 2f * mainCamera.orthographicSize;
        }
        else
        {
            // Для перспективной камеры используем расстояние до плоскости объектов
            return 2f * distanceFromCamera * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }
    }

    private void AdaptScaleToCount()
    {
        if (size <= 0) return;

        float cameraHeight = GetCameraHeight();
        float availableHeight = cameraHeight * availableHeightRatio;

        float finalHeight;
        float finalGap;

        if (size == 1)
        {
            finalHeight = Mathf.Min(availableHeight, maxObjectHeight);
            finalGap = 0f;
        }
        else
        {
            // Оптимальный размер при желаемом зазоре
            float heightWithDesiredGap = (availableHeight - (size - 1) * desiredGap) / size;

            if (heightWithDesiredGap >= minObjectHeight && heightWithDesiredGap <= maxObjectHeight)
            {
                // Идеальный вариант
                finalHeight = heightWithDesiredGap;
                finalGap = desiredGap;
            }
            else if (heightWithDesiredGap < minObjectHeight)
            {
                // Пытаемся достичь minObjectHeight, уменьшая зазор
                float gapForMin = (availableHeight - size * minObjectHeight) / (size - 1);
                if (gapForMin >= 0)
                {
                    finalHeight = minObjectHeight;
                    finalGap = gapForMin;
                }
                else
                {
                    // Даже с нулевым зазором не влезает — берём максимально возможный размер
                    finalHeight = availableHeight / size;
                    finalGap = 0f;
                }
            }
            else // heightWithDesiredGap > maxObjectHeight
            {
                // Пытаемся достичь maxObjectHeight, возможно увеличивая зазор
                float gapForMax = (availableHeight - size * maxObjectHeight) / (size - 1);
                if (gapForMax >= 0)
                {
                    finalHeight = maxObjectHeight;
                    finalGap = gapForMax;
                }
                else
                {
                    // Даже с нулевым зазором не влезает — уменьшаем размер
                    finalHeight = availableHeight / size;
                    finalGap = 0f;
                }
            }

            // Убеждаемся, что finalHeight не превышает maxObjectHeight
            finalHeight = Mathf.Min(finalHeight, maxObjectHeight);
        }

        // Расстояние между центрами объектов
        spacing = finalHeight + finalGap;

        // Применяем масштаб ко всем префабам
        Vector3 newScale = Vector3.one * finalHeight;
        objInFirstRow.transform.localScale = newScale;
        objInSecondRow.transform.localScale = newScale;
        notFinalResultObject.transform.localScale = newScale;
        resultObject.transform.localScale = newScale;

        // Отладка: проверьте значения в консоли
        Debug.Log($"size: {size}, cameraHeight: {cameraHeight}, availableHeight: {availableHeight}, finalHeight: {finalHeight}, finalGap: {finalGap}");
    }

    private void PlaceObjects()
    {
        if (mainCamera == null) return;

        float cameraHeight = GetCameraHeight();
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Ширина объекта после масштабирования (предполагаем квадратную форму)
        float objWidth = objInFirstRow.transform.localScale.x;

        // Доступный диапазон для центра объекта (с учётом половины ширины)
        float leftBound = -cameraWidth / 2f + objWidth / 2f;
        float rightBound = cameraWidth / 2f - objWidth / 2f;

        // Преобразуем нормированные координаты в мировые
        float firstRowX = Mathf.Lerp(leftBound, rightBound, firstRowNormX);
        float secondRowX = Mathf.Lerp(leftBound, rightBound, secondRowNormX);
        float thirdRowX = Mathf.Lerp(leftBound, rightBound, thirdRowNormX);
        float resultX = Mathf.Lerp(leftBound, rightBound, resultNormX);

        Vector3 centerPoint = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

        PlaceFirstRow(centerPoint, firstRowX);
        PlaceSecondRow(centerPoint, secondRowX);
        PlaceThirdRow(centerPoint, thirdRowX);
        PlaceResultRow(centerPoint, resultX);
    }

    private void PlaceFirstRow(Vector3 centerPoint, float xPos)
    {
        if (arr1 == null || arr1.Length == 0 || objInFirstRow == null) return;

        for (int i = 0; i < arr1.Length; i++)
        {
            // Распределяем объекты равномерно по вертикали с центром в 0
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

    private void PlaceSecondRow(Vector3 centerPoint, float xPos)
    {
        if (arr2 == null || arr2.Length == 0 || objInSecondRow == null) return;

        for (int i = 0; i < arr2.Length; i++)
        {
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

    private void PlaceThirdRow(Vector3 centerPoint, float xPos)
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
    }

    private void PlaceResultRow(Vector3 centerPoint, float xPos)
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
    }

    private void ShuffleArray<T>(T[] array)
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