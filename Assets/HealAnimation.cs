using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealAnimation : MonoBehaviour
{
    public Transform _graphics;
    private void Start()
    {
        _graphics.DOLocalMoveY(1f, 0.7f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
