using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{

    /*
     31.03.2014 этот класс нужен для ранжирования Осей Ф-ОС
     * Для каждого конкретно файла СС в массив будет добавляться пару Ф-ОС со значением фитнесс функции
     * Потом входе обработки эти пару будут сортироваться по значению фитнесс функции
     * браться лучшие к примеру 25%
     * а дальше считаться веса....
     */
    class Para_ID
    {
        private int _filterId;
        private string _OCfilename;


        public Para_ID()
        {
            this._filterId = new int();
            this._OCfilename = "";

        }


        public int filterId
        {
            get { return this._filterId; }
            set { this._filterId = value; }
        }

        public string OCfilename
        {
            get { return this._OCfilename; }
            set { this._OCfilename = value; }
        }
    }

    class paraId_value : IComparable
    {
        private Para_ID _para;
        private float _paravalue;



        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            paraId_value other = obj as paraId_value;
            if (other != null)
                return this._paravalue.CompareTo(other._paravalue);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public paraId_value()
        {
            this._para = new Para_ID();
            this._paravalue = new float();
        }


        public float paravalue
        {
            set { this._paravalue = value; }
            get { return this._paravalue; }

        }

        public Para_ID para
        {
            set { this._para = value; }
            get { return this._para; }
        }
    }
    /// <summary>
    /// Класс нужен для того чтобы для каждого СС файла хранить всевозможные пары ИХ фитнессами
    /// </summary>
    class OneSsStatistic
    {
        private string _SsFilename;

        private float _minFitness;
        private List<paraId_value> _ListIdPariMaxvalue;

        public OneSsStatistic()
        {
            this._ListIdPariMaxvalue = new List<paraId_value>();
            this._SsFilename = "";
        }

        public string SsFilename
        {
            get { return this._SsFilename; }
            set { this._SsFilename = value; }
        }

        public float minFitness
        {
            get { return this._minFitness; }
            set { this._minFitness = value; }
        }



        public List<paraId_value> ListIdPariMaxvalue
        {
            get { return this._ListIdPariMaxvalue; }
            set { this._ListIdPariMaxvalue = value; }
        }
    }

    class ListOfSsStatistic : List<OneSsStatistic>
    {
        //самое синимальное значение фитнесс функции среди всех СС файлов после отсечки
        private float _AbsoluteMinimum;


        public void copeFromAnother(ListOfSsStatistic source)
        {
            foreach (OneSsStatistic item in source)
            {
                OneSsStatistic item2 = new OneSsStatistic();
                item2.SsFilename = item.SsFilename;

                foreach (paraId_value newitems in item.ListIdPariMaxvalue)
                {
                    item2.ListIdPariMaxvalue.Add(newitems);
                }


              //  item2.ListIdPariMaxvalue = item.ListIdPariMaxvalue;


                
                this.Add(item2);
            }
        
        }

        public float AbsoluteMinimum
        {
            get { return this._AbsoluteMinimum; }
        }

        /// <summary>
        /// Вносим в массив все пара СС ОС фильтра и значение фитнесса при данных парах
        /// </summary>
        /// <param name="ssfilename"></param>
        /// <param name="osfilename"></param>
        /// <param name="filterid"></param>
        /// <param name="maxvalue"></param>
        public void AddOrInsert(string ssfilename, string osfilename, int filterid, float maxvalue)
        {

            //была ли такая пара у 
            bool SSinArray = false;
            foreach (OneSsStatistic myOneSS in this)
            {

                if (myOneSS.SsFilename == ssfilename)
                {

                    SSinArray = true;

                    paraId_value tempparaID = new paraId_value();
                    tempparaID.paravalue = maxvalue;
                    tempparaID.para.filterId = filterid;
                    tempparaID.para.OCfilename = osfilename;

                    myOneSS.ListIdPariMaxvalue.Add(tempparaID);


                } //if myOneSS.filename  ==

            } //конец форич

            if (!SSinArray)
            {
                OneSsStatistic temp = new OneSsStatistic();
                temp.SsFilename = ssfilename;


                paraId_value tempparaID = new paraId_value();
                tempparaID.paravalue = maxvalue;
                tempparaID.para.filterId = filterid;
                tempparaID.para.OCfilename = osfilename;
                temp.ListIdPariMaxvalue.Add(tempparaID);

                this.Add(temp);
            }
        } //AddOrInsert


        /// <summary>
        /// отсортируем по возрастанию фитнесс функции пары для каждого СС
        /// </summary>
        public void mySort()
        {
            foreach (OneSsStatistic myOneSS in this)
            {
                myOneSS.ListIdPariMaxvalue.Sort();
                myOneSS.ListIdPariMaxvalue.Reverse();
            } //конец форич
        }

        /// <summary>
        /// ВНИМАНИЕ. ТОЛЬКО НЕ ЗАБУДЬТЕ СОРТИРОВКУ ПЕРЕД ЭТИМ . ОТСЕЧЕМ ХУДЖИЕ  
        /// </summary>
        /// <param name="otsechka"> СКОЛЬКО ПРОЦЕНТОВ ОСТАВИТЬ ЛУЧШИХ</param>
        public void Otsechka(float otsechka)
        {
            foreach (OneSsStatistic myOneSS in this)
            {
                int t = (int)(otsechka * myOneSS.ListIdPariMaxvalue.Count);
                myOneSS.ListIdPariMaxvalue.RemoveRange(t, myOneSS.ListIdPariMaxvalue.Count-t); //ОБРЕЖЕМ НАЧАЛО СВЕРТКИ НА ДЛИНУ ФИЛЬТРАM
                myOneSS.minFitness = myOneSS.ListIdPariMaxvalue[t - 1].paravalue; //для нахождения минимального значение после отсечки
            } //конец форич
            
            
            //найдем самый минимум отсечки
            float min = this[0].minFitness;
            foreach (OneSsStatistic myOneSS in this)
            {
                if (myOneSS.minFitness < min) { min = myOneSS.minFitness; }
            } //конец форич


            // а вот здесь как раз и будет храниться этот самый минимум отсечки
            this._AbsoluteMinimum = min;
        }
        
        /// <summary>
        /// Метод печатающий для каждого СС файла всевозможные индивидуальные комбинации Ф-ОС  с фитнесс значением пары для текущего СС файла 
        /// </summary>
        /// <param name="ff">Величина ОТСЕЧКИ - чисто для информации в имени печатающего файла</param>
        public void PrintTotal_F_OS(float ff)
        {
            string tempDir = @"Фил-ОС статистика/";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }


            string myPath = tempDir + "1_Total_F_OC_for_CC_" + (ff*100).ToString()+ ".csv";

            StreamWriter mywr = new StreamWriter(@myPath, false, Encoding.GetEncoding("Windows-1251"));



            mywr.Write("Имя СС файла;");
            mywr.Write("Фильтер ID;");
            mywr.Write("Имя ОС файла;");
            mywr.WriteLine("Фитнесс значение;");


            foreach (OneSsStatistic myOneSS in this)
            {

                mywr.WriteLine(myOneSS.SsFilename + ";");

                foreach (paraId_value item in myOneSS.ListIdPariMaxvalue)
                {

                    mywr.Write(item.para.filterId + ";");
                    mywr.Write(item.para.OCfilename + ";");
                    mywr.WriteLine(item.paravalue + ";");


                }

 

            } //конец форич

            mywr.Close();


        }  //PrintTotal_F_OS()




    }  //Class ListOfSsStatistic



} //namespace
