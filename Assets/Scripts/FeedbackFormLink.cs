
using System;
using UnityEngine;

public class FeedbackFormLink : MonoBehaviour
{
    [Header("Microsoft Forms")]
    [TextArea(2, 4)]
    [SerializeField] private string feedbackFormUrl = "";

    public void OpenFeedbackForm()
    {
        string url = feedbackFormUrl.Trim();

        if (!Uri.TryCreate(
                url,
                UriKind.Absolute,
                out Uri parsedUrl) ||
            parsedUrl.Scheme != Uri.UriSchemeHttps)
        {
            Debug.LogWarning(
                "FeedbackFormLink: Please enter a valid " +
                "HTTPS Microsoft Forms response URL."
            );

            return;
        }

        Debug.Log("Opening playtesting feedback form.");

        Application.OpenURL(parsedUrl.AbsoluteUri);
    }
}
