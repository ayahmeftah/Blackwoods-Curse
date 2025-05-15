using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseTrigger : MonoBehaviour
{
    public ParticleSystem sparkleEffect;           // Drag the book’s particle system
    public Animator bookcaseAnimator;              // Drag the Animator on Bookcase_Door
    public HUD hud;                                // Drag the HUD (has txt field)

    private bool isPlayerNear = false;
    private bool hasPulled = false;

    void Start()
    {
        if (bookcaseAnimator != null)
        {
            bookcaseAnimator.enabled = false;      // Prevent auto-play
        }
    }

    void Update()
    {
        if (isPlayerNear && !hasPulled && Input.GetKeyDown(KeyCode.P))
        {
            if (PianoPuzzleManager.Instance != null && PianoPuzzleManager.Instance.IsPuzzleSolved)
            {
                hasPulled = true;

                if (hud != null)
                    hud.HideMessage();

                if (sparkleEffect != null)
                    sparkleEffect.Stop();

                if (bookcaseAnimator != null)
                {
                    bookcaseAnimator.enabled = true;
                    bookcaseAnimator.Rebind();         // Reset to Idle pose
                    bookcaseAnimator.Update(0f);       // Apply immediately
                    bookcaseAnimator.SetTrigger("Pull");
                    LevelManager.Instance.EnterBonusLevel();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPulled)
        {
            isPlayerNear = true;

            if (sparkleEffect != null && PianoPuzzleManager.Instance.IsPuzzleSolved)
                sparkleEffect.Play();

            if (hud != null && PianoPuzzleManager.Instance.IsPuzzleSolved)
                hud.txt.text = "Pull Book (P)";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (sparkleEffect != null)
                sparkleEffect.Stop();

            if (hud != null)
                hud.HideMessage();
        }
    }
}
