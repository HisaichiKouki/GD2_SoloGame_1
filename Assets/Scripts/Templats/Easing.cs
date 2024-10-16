using System;
using Unity.VisualScripting;
using UnityEngine;

public class Easing
{
    public static float Liner(float min, float max, float t)
    {
        return (1.0f - t) * min + max * t;
    }

    public static Vector2 Liner(Vector2 min, Vector2 max, float t)
    {
        return (1.0f - t) * min + max * t;
    }
    public static Vector3 Liner(Vector3 min, Vector3 max, float t)
    {
        return (1.0f - t) * min + max * t;
    }
 
    public static Vector2 SineInOut(float t, float totaltime, Vector2 min, Vector2 max)
    {
        max -= min;
        Vector2 result = Vector2.zero;
        result.x = -max.x / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min.x;
        result.y = -max.y / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min.y;
        return result;
    }

    public static float InSine(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * (1.0f - Mathf.Cos(t * Mathf.PI * 0.5f));
    }


    public static float OutSine(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float InOutSine(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * 0.5f * (1.0f - Mathf.Cos(t * Mathf.PI));
    }


    public static float InQuad(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * t * t;
    }

    public static float OutQuad(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * (-t * (t - 2.0f));
    }

    public static float InOutQuad(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * t * t;
        }
        else
        {
            t -= 1.0f;
            return min + deltaValue * (-0.5f * (t * (t - 2.0f) - 1.0f));
        }
    }


    public static float InCubic(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * t * t * t;
    }

    public static float OutCubic(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        t -= 1.0f;
        return min + deltaValue * (t * t * t + 1.0f);
    }

    public static float InOutCubic(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * t * t * t;
        }
        else
        {
            t -= 2.0f;
            return min + deltaValue * 0.5f * (t * t * t + 2.0f);
        }
    }


    public static float InQuart(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * t * t * t * t;
    }

    public static float OutQuart(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        t -= 1.0f;
        return min + deltaValue * (1.0f - t * t * t * t);
    }

    public static float InOutQuart(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * t * t * t * t;
        }
        else
        {
            t -= 2.0f;
            return min + deltaValue * (-0.5f * (t * t * t * t - 2.0f));
        }
    }


    public static float InQuint(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * t * t * t * t * t;
    }

    public static float OutQuint(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        t -= 1.0f;
        return min + deltaValue * (1.0f + t * t * t * t * t);
    }

    public static float InOutQuint(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * t * t * t * t * t;
        }
        else
        {
            t -= 2.0f;
            return min + deltaValue * 0.5f * (t * t * t * t * t + 2.0f);
        }
    }


    public static float InExpo(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
    }

    public static float OutExpo(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * (-Mathf.Pow(2.0f, -10.0f * t) + 1.0f);
    }

    public static float InOutExpo(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
        }
        else
        {
            t -= 1.0f;
            return min + deltaValue * 0.5f * (-Mathf.Pow(2.0f, -10.0f * t) + 2.0f);
        }
    }


    public static float InCirc(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * (1.0f - Mathf.Sqrt(1.0f - t * t));
    }

    public static float OutCirc(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        t -= 1.0f;
        return min + deltaValue * Mathf.Sqrt(1.0f - t * t);
    }

    public static float InOutCirc(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime * 0.5f;
        float deltaValue = max - min;

        if (t < 1.0f)
        {
            return min + deltaValue * 0.5f * (1.0f - Mathf.Sqrt(1.0f - t * t));
        }
        else
        {
            t -= 2.0f;
            return min + deltaValue * 0.5f * (Mathf.Sqrt(1.0f - t * t) + 1.0f);
        }
    }

    

    public static float OutQuintZero(float t, float totaltime, float min, float max, float backRaito)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return min;
        float backPoint = totaltime * backRaito / 100.0f;


        if (t < backPoint)
        {
            return OutQuint(t, backPoint, min, max);
        }
        else
        {
            return InQuint(t - backPoint, totaltime - backPoint, max, min);
        }
    }

   
    public static float easeInBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }

    public static float easeOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
    public static float easeInBack(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;

        return Liner(min, max, easeInBack(t));
    }
    public static float easeOutBack(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;

        return Liner(min, max, easeOutBack(t));
    }

    public static float InBackZero(float t, float totaltime, float min, float max, float backRaito)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return min;
        //全体に戻る割合を掛けて切り返しポイントを求める
        float backPoint = totaltime * backRaito;
        // t /= totaltime;

        if (t < backPoint)
        {
            return easeInBack(t, backPoint, min, max);
        }
        else
        {
            return easeOutBack(t - backPoint, totaltime - backPoint, max, min);
        }
    }
    public static Vector2 easeInBack(float t, float totaltime, Vector2 min, Vector2 max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;

        return Liner(min, max, easeInBack(t));
    }
    public static Vector3 easeInBack(float t, float totaltime, Vector3 min, Vector3 max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;

        return Liner(min, max, easeInBack(t));
    }
    public static Vector3 easeOutBack(float t, float totaltime, Vector3 min, Vector3 max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;

        return Liner(min, max, easeOutBack(t));
    }
    /// <summary>
	/// 弾性のある動き。振幅と周期のみ
	/// </summary>
	/// <param name="t">今の時間</param>
	/// <param name="totaltime">トータルの時間</param>
	/// <param name="amplitude">振幅。デフォルトは1.0f</param>
	/// <param name="period">周期デフォルトは0.3f</param>
	/// <returns></returns>
    public static float InElasticAmplitude(float t, float totaltime, float amplitude, float period)
    {
        if (t <= 0.0f) return 0.0f;
        if (t >= totaltime) return 0.0f;

        float s = period / (2.0f * Mathf.PI) * Mathf.Asin(1.0f);
        t /= totaltime;

        return -amplitude * Mathf.Pow(2.0f, 10.0f * (t - 1.0f)) * Mathf.Sin((t - 1.0f - s) * (2.0f * Mathf.PI) / period);
    }

    public static float OutElasticAmplitude(float t, float totaltime, float amplitude, float period)
    {
        if (t <= 0.0f) return 0.0f;
        if (t >= totaltime) return 0.0f;

        float s = period / (2.0f * Mathf.PI) * Mathf.Asin(1.0f);
        t /= totaltime;

        return amplitude * Mathf.Pow(2.0f, -10.0f * t) * Mathf.Sin((t - s) * (2.0f * Mathf.PI) / period);
    }

    public static float InOutElasticAmplitude(float t, float totaltime, float amplitude, float period)
    {
        if (t <= 0.0f) return 0.0f;
        if (t >= totaltime) return 0.0f;

        //float s = period / (2.0f * Mathf.PI) * Mathf.Asin(1.0f);
        t /= totaltime;
        float backPoint = 0.5f;


        if (t < backPoint)
        {
            Debug.Log("no");
            return OutElasticAmplitude(t, totaltime, amplitude, period);
        }
        else
        {
            Debug.Log("back");
            return InElasticAmplitude(t - backPoint, totaltime - backPoint, amplitude, period);
        }
        //if (t < 1.0f)
        //{
        //    return -0.5f * amplitude * Mathf.Pow(2.0f, 10.0f * (t - 1.0f)) * Mathf.Sin((t - 1.0f - s) * (2.0f * Mathf.PI) / period);
        //}
        //else
        //{
        //    return amplitude * Mathf.Pow(2.0f, -10.0f * (t - 1.0f)) * Mathf.Sin((t - 1.0f - s) * (2.0f * Mathf.PI) / period) * 0.5f + 1.0f;
        //}
    }

    public static Vector3 EaseAmplitudeScale(Vector3 initScale, float easeT, float easeTime, float ampritude, float period)
    {
        Vector3 newPos;
        newPos = initScale;
        newPos.x = initScale.x + -Easing.OutElasticAmplitude(easeT, easeTime, ampritude, period);
        newPos.y = initScale.y + Easing.OutElasticAmplitude(easeT, easeTime, ampritude, period);
        return newPos;
    }
}