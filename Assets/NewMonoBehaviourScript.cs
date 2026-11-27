using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LightBlinker : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Light targetLight; 
    [SerializeField] private Button startButton; 
    [SerializeField] private Button stopButton;  

    [Header("Настройки интервалов")]
    [SerializeField] private float initialInterval = 0.5f; 
    [SerializeField] private float thirdInterval = 1.5f;   
    [SerializeField] private float pauseBetween = 0.3f;   

    private Coroutine blinkCoroutine;

    private void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

       
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }

        
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartBlinking);
        }

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(StopBlinking);
        }

        
        UpdateButtonsState(isBlinking: false);
    }

    private void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(StartBlinking);
        }

        if (stopButton != null)
        {
            stopButton.onClick.RemoveListener(StopBlinking);
        }
    }

    public void StartBlinking()
    {
        
        if (blinkCoroutine != null) return;

        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    public void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }

        UpdateButtonsState(isBlinking: false);
    }

    private IEnumerator BlinkRoutine()
    {
        
        UpdateButtonsState(isBlinking: true);

        
        targetLight.enabled = true;
        yield return new WaitForSeconds(initialInterval);
        targetLight.enabled = false;
        yield return new WaitForSeconds(pauseBetween);

        
        targetLight.enabled = true;
        yield return new WaitForSeconds(initialInterval);
        targetLight.enabled = false;
        yield return new WaitForSeconds(pauseBetween);

        
        targetLight.enabled = true;
        yield return new WaitForSeconds(thirdInterval);
        targetLight.enabled = false;

        
        blinkCoroutine = null;
        UpdateButtonsState(isBlinking: false);
    }

    private void UpdateButtonsState(bool isBlinking)
    {
        if (startButton != null)
        {
            startButton.interactable = !isBlinking; 
        }

        if (stopButton != null)
        {
            stopButton.interactable = isBlinking; 
        }
    }
}