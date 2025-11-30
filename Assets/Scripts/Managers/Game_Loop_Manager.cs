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
    }

    #endregion
}
