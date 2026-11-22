using UnityEngine;

public class Randomizer
{
    private const int MaxAttempts = 100;
    public static WinConditionDTO GetWinConditionDTO(int countOfNumbers)
    {
        int[] firstArray = new int[countOfNumbers];
        int[] secondArray = new int[countOfNumbers];
        int result = 0;

        for (int i = 0; i < countOfNumbers; i++)
        {
            firstArray[i] = Random.Range(0, 10);
            secondArray[i] = Random.Range(0, 10);
            result += firstArray[i] * secondArray[i];
        }

        for (int attempt = 0; attempt < MaxAttempts; attempt++)
        {
            Shuffle(firstArray);
            Shuffle(secondArray);

            if (PositionalSim(firstArray, secondArray) != result) break;
        }

        return new WinConditionDTO
        {
            firstArray = firstArray,
            secondArray = secondArray,
            result = result
        };
    }

    private static int PositionalSim(int[] a, int[] b)
    {
        int sum = 0;
        for (int i = 0; i < a.Length; i++) sum += a[i] * b[i];
        return sum;
    }

    private static void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int j = Random.Range(0, array.Length);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}