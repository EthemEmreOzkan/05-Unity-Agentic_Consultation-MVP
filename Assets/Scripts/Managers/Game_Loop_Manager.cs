using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Loop_Manager : MonoBehaviour
{
    //*-----------------------------------------------------------------------------------------//
    #region Inspector Tab

    [Header("Gemini Ayarları -----------------------------------------------------------")]
    [Space] 
    [SerializeField] private Gemini_Api_Handler Gemini_Api_Handler;
    [SerializeField] private Prompt_List_SO Prompt_List_SO;
    [Space]
    [Header("Text_Seperators -----------------------------------------------------------")]
    [Space]
    [SerializeField] private Text_Seperator_0 Text_Seperator_0;
    [Space]
    [Space]
    [Header("Test ----------------------------------------------------------------------")]
    [Space]
    [TextArea(3,10)]
    [SerializeField] private string Test_Scenario;
    [Header("Placeholders ------------------------------------------------------")]
    [Space]
    [SerializeField] private string Cardiology_Trait;
    [SerializeField] private string Orthopedics_Trait;
    [SerializeField] private string Neurology_Trait;

    //* [BRANCH] - [TRAIT]-Bu koddan 
    //* [DOMINANCE]-Text_Seperator0'dan 
    //* [CURRENT_TURN]-Bu koddan 
    //* [PATIENT_DATA] - [SUMMARY] - [PREV_SPEAKER]
    //* [PREV_RESPONSE] - [TARGET_BRANCH]

    [Space]
    [Header("Turn_Settings -----------------------------------------------------")]
    [SerializeField] private int Max_Turn = 10;
    [SerializeField] private int Current_Turn = 1;

    private string prompt;

    #endregion
    //*-----------------------------------------------------------------------------------------//
    #region Unity LifeCycle

    void Start()
    {
        prompt = Prompt_List_SO.Prompt_List[0].Paragraph + "\n" + Test_Scenario;
        Gemini_Api_Handler.Send_Prompt(prompt);
    }

    void Update()
    {
        if(!Gemini_Api_Handler.Is_Request_In_Progress && Gemini_Api_Handler.Is_Response_Received && !Text_Seperator_0.Is_Response_Parsed_0)
        {
            Text_Seperator_0.Parse_Response_0(Gemini_Api_Handler.Last_Response);
            if(Text_Seperator_0.Is_Response_Parsed_0)
                Text_Seperator_0.Select_First_Speaker();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Speaker_Talk(Text_Seperator_0.Speaker);
            Gemini_Api_Handler.Send_Prompt(prompt);
        }
    }
    
    #endregion
    //*-----------------------------------------------------------------------------------------//
    #region Methods
    
    void Speaker_Talk(string Speaker)
    {
        if(Speaker != null && Speaker == "Kardiyoloji")
        {
            prompt = Prompt_List_SO.Prompt_List[1].Paragraph
            .Replace("[BRANCH]", "Kardiyoloji")
            .Replace("[TRAIT]", Cardiology_Trait)
            .Replace("[DOMINANCE]", Text_Seperator_0.Cardiology.ToString())
            .Replace("[CURRENT_TURN]", Current_Turn.ToString())
            .Replace("[PATIENT_DATA]", Test_Scenario)
            .Replace("[SUMMARY]", "YOK (İlk Tur)")
            .Replace("[PREV_SPEAKER]", "YOK (İlk Tur)")
            .Replace("[PREV_RESPONSE]", "YOK (İlk Tur)")
            ;
            Debug.Log($"<color=cyan><b>--- OLUŞTURULAN PROMPT ---</b></color>\n{prompt}");
        }
        else if(Speaker != null && Speaker == "Ortopedi")
        {
            
        }
        else if(Speaker != null && Speaker == "Nöroloji")
        {
            
        }
        else
        {
            Debug.LogWarning("SPEAKER ATANMADI");
        }
    }

    #endregion
    //*-----------------------------------------------------------------------------------------//
}
