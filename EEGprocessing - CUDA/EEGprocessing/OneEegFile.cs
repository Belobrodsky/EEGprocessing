using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{

    public struct OneChanelStatistic
    {
        public float convolve_average;
        public float convolve_max;
        public int convolve_max_time;
    }

    public class OneFile
    {
        private string _filename;
        private int _countOfData;
        private bool _alreadyPrint; //для мечати максимальных значений 
        private bool _alreadyCheck; //для предотвращения повторного вызова процедуры подсчета свертки при контрольнй проверки

        private List<float>[] _chanels;
        private List<float>[] _convolve_chanels;
        private int _convolve_filter_id;
        private OneChanelStatistic[] _oneChanelStatistic;

        public bool alreadyPrint {
            set { this._alreadyPrint = value; }
            get { return this._alreadyPrint; }       
        }
        public bool alreadyCheck
        {
            set { this._alreadyCheck = value; }
            get { return this._alreadyCheck; }
        }


        /// <summary>
        /// Конструктор сожрет только число каналов ЭЭГ файла
        /// </summary>
        /// <param name="countofchanels">Число ЭЭГ каналов файла ВСЕГДА РАВЕН 1 БОЛЬШЕ НЕ АКТУАЛЬНО ЗАГРУЖАТЬ</param>
        public OneFile(int countofchanels)
        {
            this._filename = "";
            this._countOfData = 0;
            this._chanels = new List<float>[countofchanels];
            this._convolve_chanels = new List<float>[countofchanels];
            this._oneChanelStatistic = new OneChanelStatistic[countofchanels];
            this._alreadyPrint = false;
            this._alreadyCheck = false;

            for (int i = 0; i < countofchanels; i++)
            {
                this._chanels[i] = new List<float>();
                this._convolve_chanels[i] = new List<float>();
                this._oneChanelStatistic[i].convolve_average = new float();
                this._oneChanelStatistic[i].convolve_max = new float();
                this._oneChanelStatistic[i].convolve_max_time = new int();
            }
        }  //конструктор КОНЕЦ



        public OneChanelStatistic[] MyOneChanelStatostic
        {
            get { return this._oneChanelStatistic; }
        }
        public int convolve_filter_id
        {
            set { this._convolve_filter_id = value; }
            get { return this._convolve_filter_id; }

        }


        /// <summary>
        ///С помощью этой процедуры парситься реальный файл в float и результаты 
        ///заносятся в массив-массивов  float
        /// </summary>
        /// <param name="filename">Путь к считываемого файлу</param>
        public void LoadDataFromFile(string filename, int NumOfChanel)
        {
            this._filename = System.IO.Path.GetFileName(filename);

            StreamReader myReader = new StreamReader(filename);
            string line = "";
            float[] value = new float[MyConst.TOTALCOUNTOFCHANEL];
            int i = 0;

            while (line != null)// (i < MyConst.FILELENGTHLIMIT)//(line!=null)
            {
                line = myReader.ReadLine();
                if (line != null)
                {
                    try
                    {
                        value = line.Split('\t').Select(n => float.Parse(n)).ToArray();
                    }
                    catch (Exception)
                    {
                        value[NumOfChanel] = MyConst.ERRORVALUE;
                    }

                    for (int k = 0; k < MyConst.COUNTOFCHANEL; k++)
                    {
                        if (value[NumOfChanel] != MyConst.ERRORVALUE)
                        {
                            this._chanels[k].Add(value[NumOfChanel]);
                            i++;
                        }
                    }
                }// if line не равно null
            }//while по всем строкам файла 
            this._countOfData = i;
            myReader.Close();
        }


        /// <summary>
        ///этот метод пробегается по всем каналов данного ЭЭГ файла и в массив _convolve_chanels
        ///записывает результат посчитанной свертки
        ///свертка считается ТУТ
        /// </summary>
        /// <param name="filter">СЮДА НУЖНО ПОМЕСТИТЬ ФИЛЬТР В ВВИДЕ List float</param>
        public void CalcConvolution(List<float> filter)
        {
            float sum = new float();
            float xx = new float();
            float hh = new float();

            for (int j = 0; j < MyConst.COUNTOFCHANEL; j++) //пробежимся по все каналам
            {
                this._convolve_chanels[j].Clear();
                for (int k = 0; k < this._countOfData; k++) //пробежимся по длине всего сигнала
                {
                    sum = 0;
                    for (int l = 0; l < filter.Count; l++) //по длине всего фильтра
                    {
                        if (k + l < this._countOfData)
                        {
                            xx = this._chanels[j][k + l];
                            hh = filter[l];
                        }
                        else { xx = 0; hh = 0; }
                        sum = sum + xx * hh;
                    }
                    this._convolve_chanels[j].Add(sum);
                } //for k
                this._oneChanelStatistic[j].convolve_average = this._convolve_chanels[j].Average();///sumaver / this._countOfData;
                this._oneChanelStatistic[j].convolve_max = this._convolve_chanels[j].Max();
                this._oneChanelStatistic[j].convolve_max_time = this._convolve_chanels[j].IndexOf(this._oneChanelStatistic[j].convolve_max);
            } //count of chanel
        }

        /// <summary>
        /// Свертка одного канала в фильтре
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="chnumber"></param>
        public void CalcConvolutionOneChanel(List<float> filter, int chnumber, int filterID,bool CUDA)
        {
            if (!CUDA)
            {
                float sum = new float();
                float xx = new float();
                float hh = new float();
                this._convolve_chanels[chnumber].Clear();

                for (int k = 0; k < this._countOfData; k++) //пробежимся по длине всего сигнала
                {
                    sum = 0;
                    for (int l = 0; l < filter.Count; l++) //по длине всего фильтра
                    {
                        if (k + l < this._countOfData)
                        {
                            xx = this._chanels[chnumber][k + l];
                            hh = filter[l];
                        }
                        else { xx = 0; hh = 0; }
                        sum = sum + xx * hh;
                    }
                    this._convolve_chanels[chnumber].Add(sum);
                } //for k  
            }

            this._convolve_filter_id = filterID;

            //теперь УДАЛИМ НАЧАЛО И КОНЕЦ МАССИВА СВЕРТКИ, ЧТОБЫ УБРАТЬ КРАЕВЫЕ ЭФФЕКТЫ
            int fiterLength = filter.Count;

            //удаляю начало и конец массива сверток равный длине фильтров, чтобы удалить краевые эффекты
            this._convolve_chanels[chnumber].RemoveRange(0, fiterLength); //ОБРЕЖЕМ НАЧАЛО СВЕРТКИ НА ДЛИНУ ФИЛЬТРА
            this._convolve_chanels[chnumber].RemoveRange(this._convolve_chanels[chnumber].Count - fiterLength, fiterLength); //ОБРЕЖЕМ КОНЕЦ СВЕРТКИ НА ДЛИНУ ФИЛЬТРА


           this._oneChanelStatistic[chnumber].convolve_average = this._convolve_chanels[chnumber].Average();///sumaver / this._countOfData;
            this._oneChanelStatistic[chnumber].convolve_max = this._convolve_chanels[chnumber].Max();
            this._oneChanelStatistic[chnumber].convolve_max_time = this._convolve_chanels[chnumber].IndexOf(this._oneChanelStatistic[chnumber].convolve_max);

        }
        /// <summary>
        /// Печатает один канал его свертку и фильтра
        /// </summary>
        /// <param name="filter">массив значений фильтра</param>
        /// <param name="chnumber">внутренний номер канала - ВСЕГДА 0</param>
        /// <param name="absolutechanel">Абсолютный номер канала</param>
        public void PrintOneChanel(List<float> filter, int chnumber, int absolutechanel,string stateNumber)
        {

            string tempDir = @"СВЕРТКИ/" + stateNumber.ToString() + "/";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);                
            }
                       

            string myPath = tempDir + System.IO.Path.GetFileName(this._filename) + "_conv_vs_filter" + this._convolve_filter_id.ToString() + ".csv";

     StreamWriter mywriteConvolve = new StreamWriter(@myPath, false, Encoding.GetEncoding("Windows-1251"));
    // StreamWriter mywrite = new StreamWriter("свертки\\" + System.IO.Path.GetFileName(this._filename) + "_conv_vs_filter" + this._convolve_filter_id.ToString() + ".csv",      
     mywriteConvolve.WriteLine("Результат свертки " + absolutechanel.ToString() + " файла" + this._filename + "с фильтром id =" + this._convolve_filter_id.ToString());
     mywriteConvolve.WriteLine("ФИЛЬТР;ИСХОДНЫЙ КАНАЛ " + absolutechanel.ToString() + "; Результат свертки");
      
    
     for (int i = 0; i < this._chanels[chnumber].Count; i++)
         {
             if (i < filter.Count) { mywriteConvolve.Write(filter[i].ToString() + ";"); } else { mywriteConvolve.Write(";"); }
             mywriteConvolve.Write(this._chanels[chnumber][i].ToString() + ";");
             if (i < this._convolve_chanels[chnumber].Count) { mywriteConvolve.Write(this._convolve_chanels[chnumber][i].ToString() + ";"); } else { mywriteConvolve.Write(";"); }
             mywriteConvolve.WriteLine();
         }
     mywriteConvolve.Close();
        }

        public List<float>[] chanels
        {
            get { return this._chanels; }
        }

        public List<float>[] convolve_chanels
        {
            get { return this._convolve_chanels; }
            set { this._convolve_chanels = value; }
        }

        public string filename
        {
            set { this._filename = value; }
            get { return this._filename; }
        }

        public int countOfData
        {
            set { this._countOfData = value; }
            get { return this._countOfData; }
        }
    } // Конец класса myOneFile
}