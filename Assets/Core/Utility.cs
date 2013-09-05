using UnityEngine;
using System.Collections;
using System;

namespace Utility {
	
	public struct Vector2Int {
		public int x;
		public int y;
		
		public Vector2Int (int x, int y) {
			this.x = x;
			this.y = y;
		}
	}
	
	public static class Math {
		
		public static T Clamp<T> (T min, T max, T target) where T : IComparable {
			if (min.CompareTo(max) > 0) return target;
			else if (min.CompareTo(target) > 0) target = min;
			else if (max.CompareTo(target) < 0) target = max;
			return target;
		}
		
		public static Vector3 Truncate (Vector3 vector, float max) {
			float ratio = max / vector.magnitude;
			ratio = ratio < 1.0f? ratio : 1.0f;
			return vector * ratio;
		}
	}
	
	
	public static class Helper {
		
		public static Vector3 GetMousePositionInWorld2D (float distFromCamera, float z) {
			Vector3 mousePosOnScreen = Input.mousePosition;
			Debug.Log("mouse on screen " + mousePosOnScreen.ToString());
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosOnScreen.x, mousePosOnScreen.y, distFromCamera));
			mousePos.z = z;
			return mousePos;
		}
	}
	
}