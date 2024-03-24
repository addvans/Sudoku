using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

[System.Serializable]
public class SudokuDesk
{
    public string[] numbers;
}

public class SaveManager : MonoBehaviour
{
    static string savePath = Application.persistentDataPath + "/Desk.json";

    /// <summary>
    /// ��������� �������� �� ��������� ����� texts � .json ����
    /// </summary>
    public static void SaveDesk(TMP_Text[] texts)
    {
        SudokuDesk sudokuDesk = new SudokuDesk();
        sudokuDesk.numbers = new string[texts.Length];
        for (int i = 0; i < sudokuDesk.numbers.Length; i++)
            sudokuDesk.numbers[i] = texts[i].text;
        string json = JsonUtility.ToJson(sudokuDesk);
        File.WriteAllText(savePath, json);
    }
    /// <summary>
    /// ����� true, ���� ���� � ����������� ������� ��� ����������, � false, ���� �� ����������
    /// </summary>
    public static bool IsSaveFileExist()
    {
        return File.Exists(Application.persistentDataPath + "/Desk.json");
    }
    /// <summary>
    /// ��������� ������ � ����� �� .json-�����, ������ ����� SudokuDesk � ��������� �� ����
    /// </summary>
    public static SudokuDesk LoadDesk()
    {
            string fileText = File.ReadAllText(savePath);
            SudokuDesk sudokuDesk = new SudokuDesk();
            JsonUtility.FromJsonOverwrite(fileText, sudokuDesk);
            return sudokuDesk;
        
    }
}
