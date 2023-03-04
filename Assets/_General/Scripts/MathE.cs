using UnityEngine;

namespace SterlingTools
{
	public static class MathE
	{
		/// <summary>
        /// Radians-to-RevolutionsPerMinute conversion constant.
        /// </summary>
        public const float Rad2Rpm = 9.54929659f;
        
        /// <summary>
        /// RevolutionsPerMinute-to-Radians conversion constant.
        /// </summary>
        public const float Rpm2Rad = 0.10471976f;

        /// <summary>
        /// MetersPerSecond-to-KilometersPerHour conversion constant.
        /// </summary>
        public const float Mps2Kph = 3.6f;

        /// <summary>
        /// KilometersPerHour-to-MetersPerSecond conversion constant.
        /// </summary>
        public const float Kph2Mps = 0.27777778f;
        
        /// <summary>
        /// Function for safe division in case of zero denominator.
        /// </summary>
        /// <returns>
        /// If denominator is close to zero, returns 0.
        /// </returns>
        public static float SafeDivide0(float numerator, float denominator)
        {
            if (Mathf.Approximately(denominator, 0f))
            {
                return 0;
            }
            
            return numerator / denominator;
        }
        
        /// <summary>
        /// Function for safe division in case of zero denominator.
        /// </summary>
        /// <returns>
        /// Returns numerator / denominator.
        /// </returns>
        public static float SafeDivide(float numerator, float denominator)
        {
            if (Mathf.Approximately(denominator, 0f))
            {
                return float.MaxValue * Mathf.Sign(numerator);
            }
            
            return numerator / denominator;
        }

        /// <summary>
        /// Get value sign
        /// </summary>
        /// <returns>
        /// Returns -1 if value is lesser than 0,
        /// 0 if value is equal to 0,
        /// 1 if value is greater than 0
        /// </returns>
        public static float Sign(float value)
        {
            if (value > 0)
            {
                return 1f;
            }
            if (value < 0)
            {
                return -1f;
            }
            
            return 0f;
        }
        
        /// <summary>
        /// Fixed Linear Interpolation with different increase and decrease speed
        /// </summary>
        /// <param name="target"> Target Value </param>
        /// <param name="current"> Current Value </param>
        /// <param name="increaseSpeed"> Interpolation speed if 'current' is less than 'target' </param>
        /// <param name="decreaseSpeed"> Interpolation speed if 'current' is higher than 'target' </param>
        /// <returns></returns>
        public static float FixedLerp(float target, float current, float increaseSpeed, float decreaseSpeed)
        {
            float diff = target - current;

            if (diff >= 0f)
            {
                current += increaseSpeed;
                return Mathf.Min(current, target);
            }
            
            current -= decreaseSpeed;
            return Mathf.Max(current, target);
        }

        /// <summary>
        /// Fixed Linear Interpolation
        /// </summary>
        /// <param name="target"> Target Value </param>
        /// <param name="current"> Current Value </param>
        /// <param name="speed"> Interpolation speed </param>
        /// <returns></returns>
        public static float FixedLerp(float target, float current, float speed)
        {
            return FixedLerp(target, current, speed, speed);
        }
	}
}
