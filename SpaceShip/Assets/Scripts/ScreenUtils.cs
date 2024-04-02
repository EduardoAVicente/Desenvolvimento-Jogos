using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ScreenUtils
    {
        public static float ScreenTop => Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
        public static float ScreenBottom => Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        public static float ScreenLeft => Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        public static float ScreenRight => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
    }

}
