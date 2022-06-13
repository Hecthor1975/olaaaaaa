using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
///  Base para componentes de: Makin' Stuff Look Good
/// </summary>
[ExecuteInEditMode]
public class BattleEffect : MonoBehaviour
{
    public Material TransitionMaterial;

    [Header("Initial Effect")]
    public bool displayInitialEffect = false;
    public float timePerEffect = 0.05f;
    public float timeToDisplayInitial = 1f;
    [Header("Second Effect")]
    public bool passToNextEffect = false;
    public bool displayEffect = false;
    public float timeToDisplay = 1;
    float actualTimeToDisplay;

    public UnityEvent onFinishedFirstAnim;
    public UnityEvent onFinishedSecondAnim;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }

    public void Update()
    {
        if (Application.isPlaying)
        {
            DisplayInitialMode();
            DisplayEffectCutoff();
        }

    }

    private void DisplayInitialMode()
    {
        if (displayInitialEffect == true && timeToDisplayInitial != 0)
        {
            actualTimeToDisplay += Time.deltaTime;
            TransitionMaterial.SetFloat("_Cutoff", 0); // Just in case
            TransitionMaterial.SetFloat("_AplhaChannetDefault", (actualTimeToDisplay % timePerEffect) / timePerEffect);

            if (actualTimeToDisplay >= timeToDisplayInitial)
            {
                displayInitialEffect = false;
                actualTimeToDisplay = 0;
                if (onFinishedFirstAnim != null) onFinishedFirstAnim.Invoke();
                if (passToNextEffect)
                {
                    displayEffect = true;
                }
            }
        }
    }

    private void DisplayEffectCutoff()
    {
        if (displayEffect == true && timeToDisplay != 0)
        {
            actualTimeToDisplay += Time.deltaTime;
            TransitionMaterial.SetFloat("_Cutoff", actualTimeToDisplay / timeToDisplay);

            if (actualTimeToDisplay >= timeToDisplay)
            {
                displayEffect = false;
                actualTimeToDisplay = 0;
                if (onFinishedSecondAnim != null) onFinishedSecondAnim.Invoke();
            }
        }
    }

    public void StartBattleEffect()
    {
        displayInitialEffect = true;
    }

    public void ResetValues()
    {
        TransitionMaterial.SetFloat("_Cutoff", 0);
        TransitionMaterial.SetFloat("_AplhaChannetDefault", 0);
    }
}
