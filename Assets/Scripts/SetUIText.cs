using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetUIText : MonoBehaviour
{
    // Define an Action delegate event that takes a string parameter
    public Action<string> onTextUpdate;

    // Reference to the Text UI element
    [SerializeField]
    private TMPro.TextMeshProUGUI uiText;

    [SerializeField]
    private RawImage tutorialImage;

    [SerializeField]
    private InputActionReference tutorialOpenAction;

    // Duration for how long the text stays visible
    [SerializeField]
    private float textDisplayDuration = 3.5f;

    private void OnEnable()
    {
        tutorialOpenAction.action.Enable();

        // Subscribe to the event
        onTextUpdate += UpdateUIText;
    }

    private void OnDisable()
    {
        tutorialOpenAction.action.Disable();

        // Unsubscribe from the event
        onTextUpdate -= UpdateUIText;
    }

    private void Update()
    {
        bool isPressed = tutorialOpenAction.action.ReadValue<float>() > 0;

        tutorialImage.gameObject.SetActive(isPressed);
    }

    private void Start()
    {
        UpdateUIText("Click Tab to open/close Tutorial");
    }

    // Method to update the UI Text with the passed string
    private void UpdateUIText(string newText)
    {
        if (uiText != null)
        {
            // Set the text and enable the UI Text
            uiText.text = newText;
            uiText.gameObject.SetActive(true);

            // Start a coroutine to hide the text after a delay
            StartCoroutine(HideTextAfterDelay());
        }
        else
        {
            Debug.LogError("UI Text reference is missing!");
        }
    }

    // Coroutine to hide the text after the specified duration
    private IEnumerator HideTextAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Disable the UI Text after the delay
        uiText.gameObject.SetActive(false);
    }
}
