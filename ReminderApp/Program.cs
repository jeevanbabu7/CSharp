using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.IO;
using ReminderApplication;
namespace mySpace
{
    class PrivateClass
    {


        public string Name
        {
            get; set;
        }
    }
    class MyClass
    {
        static void Main(string[] args)
        {


            ReminderApp app = new ReminderApp();

        }
    }
    class J
    {



        static bool TryParse(string num, out int value)
        {
            int result = 0;
            value = default;
            for (int i = 0; i < num.Length; ++i)
            {
                if (num[i] < '0' || num[i] > '9')
                    return false;
                result = result * 10 + (num[i] - '0');
            }

            value = result;
            return true;
        }

    }
}