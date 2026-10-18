using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LLMUnity;

public class NPCController : MonoBehaviour
{
    public LLMAgent llmAgent;
    public TMP_InputField inputField;
    public TextMeshProUGUI chatLogText;
    public Button sendButton;
    public ScrollRect chatScrollRect;

    private string fullLog = "";

    void Start()
    {
        sendButton.onClick.AddListener(OnSendClicked);
        inputField.onSubmit.AddListener(delegate { OnSendClicked(); });
    }

    void OnSendClicked()
    {
        string userMessage = inputField.text.Trim();
        if (string.IsNullOrEmpty(userMessage)) return;

        fullLog += $"\n <color=#5B6CFF><b>You: </b></color> {userMessage}\n";
        fullLog += "<color=#FF9F1C><b>AI:</b></color> ";
        chatLogText.text = fullLog;

        inputField.text = "";
        inputField.interactable = false;
        sendButton.interactable = false;

        _ = llmAgent.Chat(userMessage, HandleReply, OnReplyComplete);
    }

    void HandleReply(string replySoFar)
    {
        chatLogText.text = fullLog + replySoFar;
        // Automatically scroll to the bottom
        // Forces Unity to recalculate the UI height based on the new text first
        Canvas.ForceUpdateCanvases();

        // 0f represents the absolute bottom of the scroll view
        chatScrollRect.verticalNormalizedPosition = 0f;
    }

    void OnReplyComplete()
    {
        fullLog = chatLogText.text + "\n";
        inputField.interactable = true;
        sendButton.interactable = true;
        inputField.ActivateInputField();
    }
}