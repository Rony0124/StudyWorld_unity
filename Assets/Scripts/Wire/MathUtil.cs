using UnityEngine;

namespace Wire
{
   public static class MathUtil
   {
      public static Vector3 CatmullRomSplineInterp(Vector3 p_mi1, Vector3 p_0, Vector3 p_1, Vector3 p_2, float t)
      {
         Vector3 a4 = p_0;
         Vector3 a3 = (p_1 - p_mi1) / 2.0f;
         Vector3 a1 = (p_2 - p_0) / 2.0f - 2.0f*p_1 + a3 + 2.0f*a4;
         Vector3 a2 = 3.0f*p_1 - (p_2 - p_0) / 2.0f - 2.0f*a3 - 3.0f*a4;

         return a1*t*t*t + a2*t*t + a3*t + a4;
      }
   }
}
