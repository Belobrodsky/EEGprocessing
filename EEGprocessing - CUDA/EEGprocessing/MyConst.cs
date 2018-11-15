using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    class MyConst
    {
        /// <summary>
        /// Внесука я тут коменты с домашнего компа 28.12.16 и попробую-ка их отловить с рабочего подгрузив репозиторий
        /// </summary>
        /// <param name="f"></param>
        /// <param name="N"></param>
        /// <param name="g"></param>
        /// <param name="M"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport("CUFT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int cuftGetConvolution([MarshalAs(UnmanagedType.LPArray)] float[] f, int N,
            [MarshalAs(UnmanagedType.LPArray)] float[] g, int M, [MarshalAs(UnmanagedType.LPArray)] float[] c);

        public static int whatIsFitness = 3;

        public static float eps = (float)0.0001;

        public static float SIGNAL_NOIZE_RATE = (float)0.1;

        public static int BAND_FOR_FLIKKER_NOIZE = 10;  //окно интегрирования для фликкер шума

        public static float RED_LINE_TO_MAKE_MUTATION = (float)50; //процент ниже которого нужно делать мутацию

        public static float RED_LINE_TO_SKO_DIV_AVER = (float)0.2; //величина ниже которой считает плохой отсчет по фильтрам

        public static float confidenceprobability = (float)0.95;

        public static float STUDENTKVANTILE95_39 = (float)1.9600;

        public static int FILELENGTHLIMIT = 800; //специально для ЯАТ

        public static int COUNTOFCHANEL = 1; //кол-во каналов  в ЭЭГ

        public static int TOTALCOUNTOFCHANEL = 29;

        public static float ERRORVALUE = ((float)-777.7);  // метка значений дя исключений

        public static int COUNTOFGENERATION = 30;

        public static int FILTERINGENERATION = 135; 


        public static Color[] ColorArray = { Color.Red, Color.Green, Color.Blue, Color.Indigo, Color.Chartreuse, Color.Coral, Color.OrangeRed, Color.Pink, Color.Plum, Color.Peru, Color.Violet };

        /// <summary>
        /// Возвращает СПМ
        /// </summary>
        /// <param name="arrayfloat">Исходный массив</param>
        /// <returns>Массив из float - СПМ</returns>
        public static List<float> CsharpDFT(List<float> arrayfloat)
        {
            int N = arrayfloat.Count;

            List<float> array_re = new List<float>();
            List<float> array_im = new List<float>();
            List<float> total_result = new List<float>();

            float k = new float();
            float norm = new float();

            List<float> Temp_Array_Re = new List<float>();
            List<float> Temp_Array_Im = new List<float>();

            for (int i = 0; i < N - 1; i++)
            {
                array_re.Add(0);
                array_im.Add(0);
                Temp_Array_Re.Add(0);
                Temp_Array_Im.Add(0);
                total_result.Add(0);
            }

            double temp = new double();
            double temp_re = new double();
            double temp_im = new double();

            temp = 2 * Math.PI / N;
            k = ((float)temp);

            temp = 1 / Math.Sqrt(N);
            norm = ((float)temp);

            for (int t = 0; t < N - 1; t++)
            {
                Temp_Array_Re[t] = arrayfloat[t];
                Temp_Array_Im[t] = 0;
            }

            for (int t = 0; t < N - 1; t++)
            {
                temp_re = 0;
                temp_im = 0;
                for (int f = 0; f < N - 1; f++)
                {
                    temp_re = temp_re + Temp_Array_Re[f] * Math.Cos(k * f * t);
                    temp_im = temp_im + Temp_Array_Re[f] * Math.Sin(k * f * t);
                }
                array_re[t] = ((float)temp_re);
                array_im[t] = ((float)temp_im);

                //array_re[t] = norm * array_re[t];
                //array_im[t] = norm * array_im[t];
                total_result[t] = array_re[t] * array_re[t] + array_im[t] * array_im[t];
            }

            return total_result;
        }

        /// <summary>
        /// Возвращает true если нужно мутировать или false  если не нужно
        /// </summary>
        /// <param name="myonegeneration"> Принимает экземпляр класса FilterList</param>
        /// <returns>Делать или Не делать мутацию</returns>
        public static bool isNessMutation(FilterList myonegeneration)
        {
            //[17.02.2014 14:13:56] Федор Пантелеев: это отношение СВО к СРЕДНЕМУ
            //[17.02.2014 14:14:07] Belobrodsky Vladimir: ааа
            //[17.02.2014 14:14:26] Федор Пантелеев: если оно ниже определённого значения по определённому % отсчётов поколени фиьлтра то тогда мы добавляем мутации
            //[17.02.2014 14:14:31] Федор Пантелеев: если нет- не добавляем
            List<float> variation_coeff = new List<float>();

            for (int i = 0; i < myonegeneration[0].data.Count; i++)  //идем по длине фильтра
            {

                List<float> temparr = new List<float>(); //вспомогательный массив для находения среднего и СКО
                for (int j = 0; j < myonegeneration.Count; j++) // идем по всем фильтра в этом поколении
                {
                    temparr.Add(myonegeneration[j].data[i]);
                }

                variation_coeff.Add(((float)MyConst.MathSKO(temparr) / temparr.Average()));
                temparr.Clear();
                //variation_coeff.Add();           
            }

            int isBad = 0;
            for (int i = 0; i < variation_coeff.Count; i++)
            {
                if (Math.Abs(variation_coeff[i]) < MyConst.RED_LINE_TO_SKO_DIV_AVER) isBad++;
            }

            float varcoeff_pr = ((float)isBad / variation_coeff.Count) * 100;

            if (varcoeff_pr > MyConst.RED_LINE_TO_MAKE_MUTATION) { return true; } else return false;

        }


        /// <summary>
        /// Тупо возвращает СКО по массиву из флоат
        /// </summary>
        /// <param name="floatarray">Массив из флоат</param>
        /// <returns>СКО. СКО=КОРЕНЬ(дисперсия). Дисперсия=Сумма (среднее-текущее)/N-1.</returns>
        public static float MathSKO(List<float> floatarray)
        {
            float SKO = new float();
            float aver = floatarray.Average();
            //        да не два раза
            //[11.02.2014 23:33:34] Федор Пантелеев: Дисперсия=Сумма (среднее-текущее)/N-1
            //[11.02.2014 23:33:49] Федор Пантелеев: СКО=КОРЕНЬ(дисперсия)

            //[11.02.2014 23:34:05] Федор Пантелеев: Ошибка средненего= СКО/корень(n)
            //[11.02.2014 23:34:20] Федор Пантелеев: Корень из N
            //[11.02.2014 23:34:51] Федор Пантелеев: довинтервал СРЕднее+-СТЬЮДЕНТ*ошибку среднего
            for (int i = 0; i < floatarray.Count; i++)
            {
                SKO += (aver - floatarray[i]) * (aver - floatarray[i]);
            }
            SKO = SKO / (floatarray.Count - 1);
            SKO = (float)Math.Sqrt(SKO);
            return SKO;
        }

        public static float RocCountOfInvolve(ListOfEegFiles files, float maxvalue)
        {
            float sum = (float)0;

            foreach (OneFile file in files.MyFiles)
            {
                foreach (float myf in file.convolve_chanels[0])
                {
                    if (Math.Abs(myf) <= Math.Abs(maxvalue)) sum = sum+1;
                }                
            }

            return sum;
        }

       
       /// <summary>
        /// возвращает сколько элементов массива files имеют значения большее или РАВНО чем maxvalue
       /// </summary>
       /// <param name="files"></param>
       /// <param name="maxvalue"></param>
       /// <returns></returns>
        public static float RocCountOfInvolveArrayBig(List<float> files, float maxvalue)
        {
            float sum = (float)0;

          
                foreach (float myf in files)
                {
                    if (Math.Abs(myf) >= Math.Abs(maxvalue)) sum = sum + 1;
                }
              return sum;
        }


        
       /// <summary>
        /// возвращает сколько элементов массива files имеют значения СТРОГО меньшее чем maxvalue
       /// </summary>
       /// <param name="files"></param>
       /// <param name="maxvalue"></param>
       /// <returns></returns>
        public static float RocCountOfInvolveArraySmall(List<float> files, float maxvalue)
        {
            float sum = (float)0;


            foreach (float myf in files)
            {
                if (Math.Abs(myf) < Math.Abs(maxvalue)) sum = sum + 1;
            }
            return sum;
        }

    }
}
