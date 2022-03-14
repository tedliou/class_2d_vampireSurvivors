using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private float _cooldown = 1f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldown);
        }
    }
}
