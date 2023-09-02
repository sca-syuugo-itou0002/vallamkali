using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMove : MonoBehaviour
{
	//�X���C�_�[���C���X�y�N�^�[����R�t���B
	[SerializeField]
	Slider powerMeterSlider;

	[SerializeField]
	Slider powerMeterSlider01;
	//�p���[���[�^�[�̃X�s�[�h�̔{���B
	[SerializeField]
	float powerMeterSpeedRate = 0.01f;

	//�p���[���[�^�[���~�߂����̒l�B
	//0�`1�܂ł̊Ԃ̒l������̂ŁA�g�p���鎞��Mathf.Lerp(powerMin, powerMax, powerMeterValue)�݂����Ȋ����ŁB
	float powerMeterValue = 0;

	//�p���[���[�^�[�R���[�`���̌o�ߎ��ԁB
	float powerMeterElapsedTime;

	//�R���[�`���̊Ǘ��p�B
	Coroutine powerMeter;


    private void Update()
    {
		#if false
        if (Input.GetMouseButtonDown(0))
        {
			StartPowerMeter();
        }
		#endif
		
	}
    //�_��̃p���[���[�^�[���J�n���������ɌĂԁB
    void StartPowerMeter()
	{
		
		powerMeter = StartCoroutine("PowerMeter");
	}


	//�_��
	IEnumerator PowerMeter()
	{
		
		powerMeterElapsedTime = 0;

		while (true)
		{
			powerMeterElapsedTime += Time.deltaTime * powerMeterSpeedRate;

			//�Ō�܂ōs���Ɖ������[�vVer�B
			powerMeterSlider.value = Mathf.PingPong(powerMeterElapsedTime, 10.0f);
			//�Ō�܂ōs���ƍŏ����烋�[�vVer�B
			//			powerMeterSlider.value = powerMeterElapsedTime % 1.0f;
			
			//��ʂ��^�b�v����ƒ�~���ăp���[���m��B
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
