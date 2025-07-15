using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    // 게임오브젝트를 위한 FindChild
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false) // 직계 자식만 검색
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else // 모든 하위 오브젝트 검색
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    #region TRANSFORM_COROUTINE

    public static IEnumerator Co_SlrpLocalRotation(GameObject gameObject, Quaternion startLocalRotation, Quaternion targetLocalRotation, float lerpTime)
    {
        float accTime = 0f;
        float lerpTimeInv = 1f / lerpTime;
        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime * lerpTimeInv, 1f);

            gameObject.transform.localRotation = Quaternion.Slerp(startLocalRotation, targetLocalRotation, ratio);

            if (ratio >= 1f)
            {
                yield break;
            }

            yield return null;
        }
    }

    public static IEnumerator Co_MoveParabolic(GameObject gameObject, Transform targetTransform, float lerpTime)
    // 이후 범용 함수가 있다면 그것으로 교체
    {
        float accTime = 0f;
        float maxTime = lerpTime;

        Vector3 startPos = gameObject.transform.position;
        Vector3 additionalForce = new Vector3(0f, 2f, 0);

        while (gameObject != null)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / maxTime, 1f);

            Vector3 curAdditonalForce = additionalForce * Mathf.Sin(ratio * Mathf.Deg2Rad * 180f);
            Vector3 targetPos = targetTransform.position + curAdditonalForce;
            gameObject.transform.position = Vector3.Lerp(startPos, targetPos, ratio);

            if (ratio >= 1f - Mathf.Epsilon)
            {
                yield break;
            }

            yield return null;
        }
    }

    public static IEnumerator Co_RingingZ(GameObject gameObject, float maxAngle, float time, int numCycle)
    {
        float accTime = 0f;
        float timeInv = 1f / time;

        Vector3 axis = gameObject.transform.forward;
        Quaternion startRotation = gameObject.transform.rotation;

        while (numCycle > 0)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Clamp(accTime * timeInv, 0f, 1f);

            if (accTime >= time)
            {
                accTime -= time;
                --numCycle;
            }

            float factor = ratio * 4f;
            float curAngle = 0f;

            if (factor < 1f)
            {
                curAngle = ratio * 4f * maxAngle;
            }

            else if (factor < 2f)
            {
                curAngle = ((ratio * 4f) - 2f) * -1f  * maxAngle;
            }

            else if (factor < 3f)
            {
                curAngle = ((ratio * 4f) - 2f) * -1f * maxAngle;
            }

            else
            {
                curAngle = ((ratio * 4f) - 4f)  * maxAngle;
            }

            Quaternion curAddRotation = Quaternion.AngleAxis(curAngle, axis);
            gameObject.transform.rotation = curAddRotation * startRotation;

            yield return null;
        }

        yield break;
    }

    //  Sin 그래프를 따라 시작 크기 => 최대 크기 => 목표 크기 순으로 적용
    public static IEnumerator Co_LerpLocalScaleSin(GameObject gameObject, Vector3 startScale, Vector3 maxScale, Vector3 targetScale, float time)
    {
        float accTime = 0f;

        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / time, 1f);

            float sinValue = Mathf.Sin(ratio * 180f * Mathf.Deg2Rad);
            Vector3 curScale = Vector3.zero;
            if (ratio < 0.5f)
            {
                curScale = Vector3.Lerp(startScale, maxScale, sinValue);
            }
            else
            {
                curScale = Vector3.Lerp(targetScale, maxScale, sinValue);
            }

            gameObject.transform.localScale = curScale;

            if (ratio >= 1f - Mathf.Epsilon)
            {
                yield break;
            }

            yield return null;
        }
    }

    public static IEnumerator Co_LerpLocalScale(GameObject gameObject, Vector3 startScale, Vector3 targetScale, float time)
    {
        float accTime = 0f;

        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / time, 1f);
            Vector3 curScale = Vector3.Lerp(startScale, targetScale, ratio);
            gameObject.transform.localScale = curScale;

            if (ratio >= 1f - Mathf.Epsilon)
            {
                yield break;
            }

            yield return null;
        }
    }

    #endregion
}
