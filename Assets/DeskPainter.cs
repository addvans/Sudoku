using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DeskPainter : MonoBehaviour
{
    /// <summary>
    /// ������������ ����, ������ � ������� ����� ��� ������� numberIndex. ���� ���� numberIndex ������������� �������� �� 0.75f ��� ���������
    /// </summary>
    public static void HighlightNumber(TMP_Text[] texts, int numberIndex, Color newColor)
    {
        for (int j = numberIndex % 9; j < texts.Length; j += 9) // ��������� ������� ������ ����
            texts[j].GetComponentInParent<Image>().color = newColor;
        for (int j = numberIndex - (numberIndex % 9); j < numberIndex + (9 - numberIndex % 9); j++) // ��������� ������ �� ������ ����� �������
            texts[j].GetComponentInParent<Image>().color = newColor;
        int leftUpCornerRow = numberIndex / 9;
        int leftUpCornerRowCol = numberIndex % 9;
        leftUpCornerRow -= leftUpCornerRow % 3;
        leftUpCornerRowCol -= leftUpCornerRowCol % 3;
        for (int r = leftUpCornerRow; r < leftUpCornerRow + 3; r++) // ��������� ������� 3�3
            for (int c = leftUpCornerRowCol; c < leftUpCornerRowCol + 3; c++)
            {
                int blockIndex = r * 9 + c;
                texts[blockIndex].GetComponentInParent<Image>().color = newColor;
            }
        texts[numberIndex].GetComponentInParent<Image>().color *= 0.75f;
    }
    /// <summary>
    /// ���������� � newColor ����� � ������ ��� ��������� �� wrongNumbers
    /// </summary>
    public static void RecolorDuplicates(TMP_Text[] texts, HashSet<int> wrongNumbers, Color newColor)
    {
        foreach (int i in wrongNumbers)
            texts[i].color = newColor;
    }
    /// <summary>
    /// ���������� ����� ����� �������� ���� � newColor
    /// </summary>
    public static void RecolorNumbers(TMP_Text[] texts, Color newColor)
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].color = newColor;
    }
}
