using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DeskPainter : MonoBehaviour
{
    /// <summary>
    /// Подсвечивает поле, строку и столбец слота под номером numberIndex. Цвет поля numberIndex дополнительно умножает на 0.75f для выделения
    /// </summary>
    public static void HighlightNumber(TMP_Text[] texts, int numberIndex, Color newColor)
    {
        for (int j = numberIndex % 9; j < texts.Length; j += 9) // проверяем столбец сверху вниз
            texts[j].GetComponentInParent<Image>().color = newColor;
        for (int j = numberIndex - (numberIndex % 9); j < numberIndex + (9 - numberIndex % 9); j++) // проверяем строку от номера слева направо
            texts[j].GetComponentInParent<Image>().color = newColor;
        int leftUpCornerRow = numberIndex / 9;
        int leftUpCornerRowCol = numberIndex % 9;
        leftUpCornerRow -= leftUpCornerRow % 3;
        leftUpCornerRowCol -= leftUpCornerRowCol % 3;
        for (int r = leftUpCornerRow; r < leftUpCornerRow + 3; r++) // проверяем квадрат 3х3
            for (int c = leftUpCornerRowCol; c < leftUpCornerRowCol + 3; c++)
            {
                int blockIndex = r * 9 + c;
                texts[blockIndex].GetComponentInParent<Image>().color = newColor;
            }
        texts[numberIndex].GetComponentInParent<Image>().color *= 0.75f;
    }
    /// <summary>
    /// Окрашивает в newColor цифры в слотах под индексами из wrongNumbers
    /// </summary>
    public static void RecolorDuplicates(TMP_Text[] texts, HashSet<int> wrongNumbers, Color newColor)
    {
        foreach (int i in wrongNumbers)
            texts[i].color = newColor;
    }
    /// <summary>
    /// Окрашивает цифры всего игрового поля в newColor
    /// </summary>
    public static void RecolorNumbers(TMP_Text[] texts, Color newColor)
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].color = newColor;
    }
}
