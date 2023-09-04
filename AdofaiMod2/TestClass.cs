using System;
using HarmonyLib;

namespace AdofaiMod2
{
    public class TestClass
    {
        public int a = 1;
        private string pri = "private";
        
        public void start()
        {
            Test(1);
        }
        public static void Test(int n)
        {
            Console.WriteLine("n: " + n);
        }
        public void ASDF()
        {
            Console.WriteLine("ASDF");
        }
    }

    public static class PatchTest
    {
        [HarmonyPatch(typeof(TestClass), "Test")]
        public static class TestReturn
        {
            public static bool Prefix(TestClass __instance, string __pri, int n)
            {
                __instance.ASDF();
                Console.WriteLine(__instance.a);
                Console.WriteLine(__pri);
                Console.WriteLine("n: " + n);
                return false;
            }
        }
    }
}