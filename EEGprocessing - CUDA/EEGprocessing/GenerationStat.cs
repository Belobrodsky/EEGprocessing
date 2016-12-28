using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    public class GenerationStat
    {
        private float averTime {set;get;}
        private float dispersionTime { set; get; }
        private float skoTime { set; get; }
        private float averDevTime { set; get; }
        private float absoluteDevTime { set; get; }
        private int N {set;get;}


        public void CalcAbsoluteDevTime(List<int> data) {

            int N = data.Count;

            float myaverTime = (float)data.Average();
            this.averTime = myaverTime;
            float sum = new float();

            for (int i = 0; i < N; i++)
            {
                sum+=(float)Math.Pow((myaverTime-data[i]),2);
            }
            sum = sum / (N - 1);
            this.dispersionTime = sum; //Дисперсия

            sum = (float)Math.Sqrt(sum);
                        
            this.skoTime = sum;  //корень квадратный из дисперсии

            this.averDevTime = sum / ((float)Math.Sqrt(N));


            this.absoluteDevTime = (float)MyConst.STUDENTKVANTILE95_39 * this.averDevTime;
            
                    
            this.N = N;
        }

        public static void PrintStatToFile(string filename, GenerationStat[] ListGenerationStat)
        {
            StreamWriter myw = new StreamWriter(filename,false, Encoding.GetEncoding("Windows-1251"));

            myw.Write("Номер Поколения;");
            myw.Write("Среднее значение критической точки;");
            myw.Write("Дисперсия;");
            myw.Write("СКО;");
            myw.Write("Коэфф. Стьюдента;");
            myw.Write("Ошибка среднего;");
            myw.Write("Начало Дов. Интервала;");
            myw.Write("Конец  Дов. Интервала;");
            myw.Write("Доверительная вероятность;");
            myw.WriteLine("Кол-во измерений;");

            int i = 0;

            foreach (GenerationStat OneStat in ListGenerationStat)
            {
                
                myw.Write(i + ";");
                myw.Write(OneStat.averTime + ";");
                myw.Write(OneStat.dispersionTime + ";");
                myw.Write(OneStat.skoTime + ";");
                myw.Write(MyConst.STUDENTKVANTILE95_39 + ";");
                myw.Write(OneStat.averDevTime + ";");
                myw.Write(OneStat.averTime - OneStat.absoluteDevTime + ";");
                myw.Write(OneStat.averTime + OneStat.absoluteDevTime + ";");
                myw.Write(MyConst.confidenceprobability + ";");
                myw.WriteLine(OneStat.N + ";");
                i++;
            }
            myw.Close();
        
        
        }
    
    }
}
