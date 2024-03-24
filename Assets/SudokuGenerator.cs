using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SudokuGenerator : MonoBehaviour
{
     /// <summary>
     /// Заполняет всё поле цифрами, подходящими для судоку
     /// </summary>
    public static void RenumberDesk(TMP_Text[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            int rowMult = i / 9 * 3;
            int blockMult = i / 27;
            int totalnumber = (i + rowMult + blockMult) % 9 + 1;
            texts[i].text = $"{totalnumber}";
        }
    }
    /// <summary>
    /// Перемешивает игровое поле с помощью всех доступных методов
    /// </summary>
    public static void ShuffleDesk(TMP_Text[] texts, int difficulty)
    {
        RenumberDesk(texts);
        Transpose(texts);
        for (int i = 0; i < 10; i++)
        {
            int first = 0;
            int second = 0;
            while (first == second)
            {
                first = Random.Range(1, 4);
                second = Random.Range(1, 4);
            }
            switch (Random.Range(1, 4))
            {
                case 1:
                    SwapRowsAreas(texts, first, second);
                    break;
                case 2:
                    SwapColumnsAreas(texts, first, second);
                    break;
                case 3:
                    Transpose(texts);
                    break;
            }
        }
        SetInvisible(texts, difficulty);
        DeskPainter.RecolorNumbers(texts, Color.black);
    }
    /// <summary>
    /// Удаляет amount значений из игрового поля. Двигается с начала и с конца игрового поля
    /// </summary>
    static void SetInvisible(TMP_Text[] texts, int amount)
    {
        int maxStep = 81 / amount;
        int randStep;
        int count = 0;
        for (int i = 0; i < texts.Length; i += randStep)
        {

            if (count % 2 == 0)
                if (texts[i].text.Length > 0)
                    texts[i].text = string.Empty;
                else count--;
            else
                if (texts[texts.Length - i].text.Length > 0)
                texts[texts.Length - i].text = string.Empty;
            else
                count--;
            randStep = Random.Range(1, maxStep + 1);
            count++;
            if (count >= amount)
                break;
        }
    }
    /// <summary>
    /// Транспонирует игровое поле - значения строк в столбцы и наоборот
    /// </summary>
    static void Transpose(TMP_Text[] texts)
    {
        string[] numbers = new string[texts.Length];
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = texts[i].text;
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                texts[j * 9 + i].text = numbers[i * 9 + j];
    }
    /// <summary>
    /// Меняет тройки строк местами. first и second - номера троек с нумерацией сверху поля
    /// </summary>
    static void SwapRowsAreas(TMP_Text[] texts, int first, int second) // оба номера - от 1 до 3
    {
        if (first > second)
        {
            int temp = first;
            first = second;
            second = temp;
        }
        for (int j = 27 * (first - 1); j < first * 27; j++)
        {
            string temp = texts[j].text;
            int indexToSwap = j + 27 * (second - first);
            texts[j].text = texts[indexToSwap].text;
            texts[indexToSwap].text = temp;
        }

    }
    /// <summary>
    /// Меняет тройки столбцов местами. first и second - номера троек с нумерацией с левой стороны поля
    /// </summary>
    static void SwapColumnsAreas(TMP_Text[] texts, int first, int second) // оба номера - от 1 до 3
    {
        if (first > second)
        {
            int temp = first;
            first = second;
            second = temp;
        }

        for (int i = (first - 1) * 3; i < (first - 1) * 3 + 3; i++)
        {
            for (int j = i; j < texts.Length; j += 9)
            {
                string temp = texts[j].text;
                int indexToSwap = j + 3 * (second - first);
                texts[j].text = texts[indexToSwap].text;
                texts[indexToSwap].text = temp;
            }
        }


    }
}
