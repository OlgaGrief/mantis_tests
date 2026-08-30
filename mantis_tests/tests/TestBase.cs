using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Text;

namespace mantis_tests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager app;

        [TestFixtureSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random(); //делаем генератор случайных чисел. Чтобы не создавались одинаковые псевдослучайные числа, необходимо объявить генератор вне класса
        public static string GenerateRandomString(int max)
        {
            //делаем случайное число в диапазоне от 0 до указанного мах
            int l = Convert.ToInt32(rnd.NextDouble() * max); //этот метод сгенерирует число от 0 до 1, умножаем на мах и преобразуем из дробного в целое
            // Создаём объект, в котором будем собирать строку
            StringBuilder builder = new StringBuilder(); //генерируем случайные символы и делаем строку
            // Генерируем l случайных символов
            for (int i = 0; i < l; i++) //цикл для генерации случайных символов при помощи генератора rnd
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65))); //Все символы с кодом < 32 непечатные.
                                                                                             //Сгенерированное от 0 до 1число, *223 и конвертируем в целое число,
                                                                                             //а потом в символ число 
                                                                                             //изменили 223 на 65 - символы лат алфавита, цифры и спецсимволы
            }
            return builder.ToString();
        }
    }
}
