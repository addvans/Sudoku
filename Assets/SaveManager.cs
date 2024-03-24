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
    /// Переводит значения из текстовых полей texts в .json файл
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
    /// Выдаёт true, если файл с сохранёнными данными уже существует, и false, если не существует
    /// </summary>
    public static bool IsSaveFileExist()
    {
        return File.Exists(Application.persistentDataPath + "/Desk.json");
    }
    /// <summary>
    /// Загружает данные о полях из .json-файла, создаёт класс SudokuDesk и добавляет их туда
    /// </summary>
    public static SudokuDesk LoadDesk()
    {
            string fileText = File.ReadAllText(savePath);
            SudokuDesk sudokuDesk = new SudokuDesk();
            JsonUtility.FromJsonOverwrite(fileText, sudokuDesk);
            return sudokuDesk;
        
    }
}
