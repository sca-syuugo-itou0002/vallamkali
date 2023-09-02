using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    [SerializeField] private Text stateTex;
    [SerializeField] private Animator anim;
    private void Awake()
    {
        stateTex.text = null;
    }
    public void StateDisplay(SlotCont2.TIMING_STATE state)
    {
        stateTex.text = state.ToString();
        StartCoroutine(ResetText());
        anim.SetTrigger("Pop");
    }
    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(1.0f);
        stateTex.text = null;
    }
}
