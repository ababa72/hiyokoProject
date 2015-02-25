using UnityEngine;
using System.Collections;

public interface  ITapBehaviour
{
	// タップされたときの処理
	void TapDown (ref RaycastHit hit);

	// タップを離したときの処理
	void TapUp (ref RaycastHit hit);
}
