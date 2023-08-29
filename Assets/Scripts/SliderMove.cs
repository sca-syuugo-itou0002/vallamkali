using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMove : MonoBehaviour
{
	//スライダーをインスペクターから紐付け。
	[SerializeField]
	Slider powerMeterSlider;


	//パワーメーターのスピードの倍率。
	float powerMeterSpeedRate = 0.01f;

	//パワーメーターを止めた時の値。
	//0～1までの間の値が入るので、使用する時はMathf.Lerp(powerMin, powerMax, powerMeterValue)みたいな感じで。
	float powerMeterValue = 0;

	//パワーメーターコルーチンの経過時間。
	float powerMeterElapsedTime;

	//コルーチンの管理用。
	Coroutine powerMeter;


    private void Update()
    {
		Debug.Log(Input.GetMouseButtonDown(0));
        if (Input.GetMouseButtonDown(0))
        {
			StartPowerMeter();
        }
	}
    //棒状のパワーメーターを開始したい時に呼ぶ。
    void StartPowerMeter()
	{
		Debug.Log("A");
		powerMeter = StartCoroutine("PowerMeter");
	}


	//棒状
	IEnumerator PowerMeter()
	{
		Debug.Log("B");
		powerMeterElapsedTime = 0;

		while (true)
		{
			powerMeterElapsedTime += Time.deltaTime * powerMeterSpeedRate;

			//最後まで行くと往復ループVer。
			powerMeterSlider.value = Mathf.PingPong(powerMeterElapsedTime, 1.0f);
			//最後まで行くと最初からループVer。
			//			powerMeterSlider.value = powerMeterElapsedTime % 1.0f;
			Debug.Log("C");
			//画面をタップすると停止してパワーを確定。
			if (Input.GetMouseButtonUp(0))
			{
				powerMeterValue = powerMeterSlider.value;
				yield break;
			}

			yield return null;
		}
	}
	public float GetPowerMeterValue()
	{
		return powerMeterValue;
	}
}
