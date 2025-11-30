using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Text_Seperator_0 : MonoBehaviour
{
    //*-----------------------------------------------------------------------------------------//
    #region Inspector Tab

    [Header("Ayrıştırılmış Değerler ----------------------------------------------------")]
    [Space]
    [SerializeField] public int Cardiology = 0;
    [SerializeField] public int Orthopedics = 0;
    [SerializeField] public int Neurology = 0;
    [Space]
    [Header("Konuşmacı -----------------------------------------------------------------")]
    public string Speaker;


    [HideInInspector] public bool Is_Response_Parsed_0 = false;


    #endregion
    //*-----------------------------------------------------------------------------------------//
    #region Public Methods

    public void Parse_Response_0(string response)
    {
        if (string.IsNullOrEmpty(response) || response.Contains("API Hatası")) return;

        string[] lines = response.Split('\n');

        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            if (trimmed.StartsWith("Kardiyoloji:", System.StringComparison.OrdinalIgnoreCase))
            {
                string value = trimmed.Replace("Kardiyoloji:", "").Trim();
                if (int.TryParse(value, out int result))
                {
                    Cardiology = Mathf.Clamp(result, 0, 100);
                }
            }
            else if (trimmed.StartsWith("Ortopedi:", System.StringComparison.OrdinalIgnoreCase))
            {
                string value = trimmed.Replace("Ortopedi:", "").Trim();
                if (int.TryParse(value, out int result))
                {
                    Orthopedics = Mathf.Clamp(result, 0, 100);
                }
            }
            else if (trimmed.StartsWith("Nöroloji:", System.StringComparison.OrdinalIgnoreCase))
            {
                string value = trimmed.Replace("Nöroloji:", "").Trim();
                if (int.TryParse(value, out int result))
                {
                    Neurology = Mathf.Clamp(result, 0, 100);
                }
            }
        }

        if (Cardiology + Orthopedics + Neurology == 100)
        {
            Is_Response_Parsed_0 = true;
        }
    }

    public void Select_First_Speaker()
    {
    Speaker =
    (Cardiology >= Orthopedics && Cardiology >= Neurology) ? "Kardiyoloji" :
    (Orthopedics >= Cardiology && Orthopedics >= Neurology) ? "Ortopedi" :
    "Nöroloji";
    }

    #endregion
    //*-----------------------------------------------------------------------------------------//
}
