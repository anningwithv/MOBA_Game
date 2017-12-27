using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCheck
{
	public static float GetDistance(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(GetDistancePower(x1, y1, x2, y2));
	}

	public static float GetDistancePower(float x1, float y1, float x2, float y2)
	{
		return Mathf.Pow(x1 - x2, 2) + Mathf.Pow(y1 - y2, 2);
	}

	public static float GetDistance(Transform from, Transform to)
	{
		return GetDistance(from.position.x, to.position.x, from.position.z, to.position.z);
	}

	public static Vector3 GetVetor(Vector3 from, Vector3 to)
	{
		return new Vector3(to.x - from.x, from.y, to.z - from.z);
	}

	/// 
	/// 判定攻击目标是否在攻击者扇形攻击范围类
	/// 
	/// 攻击者
	/// 被击者
	/// 扇形半径
	/// 扇形角度
	/// 
	public static bool IsInSector(Transform source, Transform target, float radius, float angle)
	{
		float STDistance = GetDistance(source, target);

		Vector3 _tVector = GetVetor(source.transform.position, target.transform.position);
		Vector3 _sVector = (source.position + (source.rotation * Vector3.forward) * radius);
		float targetAngle = Vector3.Angle(_tVector, _sVector);

		if (STDistance < radius && targetAngle < angle / 2 && targetAngle > -(angle / 2))
			return true;
		
		return false;
	}
}
