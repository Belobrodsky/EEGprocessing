using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    //    [8:29:30] Belobrodsky Vladimir: [15 марта 2014 г. 1:19] Федор Пантелеев: 

    //<<< т.е. например пара Ф-ОС жила 6 поколений и была 5 6 7 8 9 10
    //среднее место 7.5
    //и веди учет числа поколений жизни
    /// <summary>
    ///  Класс где одна запись пары Ф_О
    /// </summary>

    class TotalOneFiltervsMain : IComparable
    {
        private int _filterID;
        private string _mainfilename;
        private int _skolkoZhili;
        private List<int> _mesto;
        private List<int> _gener;

        public  TotalOneFiltervsMain()
        {
            this._filterID = new int();
            this._mainfilename = "";
            this._skolkoZhili = new int();
            this._mesto = new List<int>();
            this._gener = new List<int>();
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            TotalOneFiltervsMain other = obj as TotalOneFiltervsMain;
            if (other != null)
                return this._skolkoZhili.CompareTo(other._skolkoZhili);
            else
                throw new ArgumentException("Object is not a Temperature");
        }



        public int filterID
        {
            set { this._filterID = value; }
            get { return this._filterID; }
        }
        public string mainfilename
        {
            set { this._mainfilename = value; }
            get { return this._mainfilename; }
        }

        public int skolkoZhili
        {
            set { this._skolkoZhili = value; }
            get { return this._skolkoZhili; }
        }

        public List<int> mesto
        {
            set { this._mesto = value; }
            get { return this._mesto; }
        }

        public List<int> gener
        {
            set { this._gener = value; }
            get { return this._gener; }
        }


    }
    /// <summary>
    /// Класс где абсолютно будет накапливать все индивидуальные пары Ф-О.
    /// Массим классов TotalOneFiltervsMain
    /// </summary>
    class TotalFOStatistic 
    {
        private List<TotalOneFiltervsMain> _ListofTotalOneFiltervsMain;


        public void printTotalFOStatistic()
        {

            //Для того чтобы в одном файле лжело по поколения среднее значение фитнесс функции
            StreamWriter myFitnesswriter = new StreamWriter("TotalFOStatistic.csv", false, Encoding.GetEncoding("Windows-1251"));
            myFitnesswriter.WriteLine("Порядковый номер пару; FILTER_ID; FILENAME; Среднее место; Сколько поколений жила пара; Какие места занимала; В каких поколениях ");

            foreach (TotalOneFiltervsMain item in this._ListofTotalOneFiltervsMain)
            {

                myFitnesswriter.Write(this._ListofTotalOneFiltervsMain.IndexOf(item) + ";" + item.filterID + ";" + item.mainfilename + ";" + item.mesto.Average() + ";" + item.skolkoZhili + ";");

                foreach (int place in item.mesto)
                {
                    myFitnesswriter.Write(place + ",");   
                }
                myFitnesswriter.Write(";");


                foreach (int gener in item.gener)
                {
                    myFitnesswriter.Write(gener + ",");
                }

                myFitnesswriter.Write(";");

                myFitnesswriter.WriteLine();
                
            }

            myFitnesswriter.Close();
        
        }


        public List<TotalOneFiltervsMain> ListofTotalOneFiltervsMain
        {
            get { return this._ListofTotalOneFiltervsMain; }
            //set { this._ListofTotalOneFiltervsMain = value; }
        }


        public TotalFOStatistic()
        {
            this._ListofTotalOneFiltervsMain = new List<TotalOneFiltervsMain>();
        }

        public void AddOrInsert(List<OneFilterVsMain> ListOfOneFilterVsMain)
        {
            //есть ли такая пара ужу в списке



            //проверка есть ли такая пару уже в списке

          
            foreach (OneFilterVsMain newItems in ListOfOneFilterVsMain)
            {
              
                bool alreadyIn =false;
                foreach (TotalOneFiltervsMain itemdatabase in this._ListofTotalOneFiltervsMain)
                {

                    if ((newItems.filterID == itemdatabase.filterID) && (newItems.mainfilename == itemdatabase.mainfilename))
                    {

                        itemdatabase.skolkoZhili += 1;
                        itemdatabase.mesto.Add(ListOfOneFilterVsMain.IndexOf(newItems));
                        itemdatabase.gener.Add(newItems.currgeneration);
                        alreadyIn = true;
                    }

                }
                

                if (!alreadyIn) //значит нужно добаваить в itemdatabase;
                {
                    TotalOneFiltervsMain temp = new TotalOneFiltervsMain();

                    temp.filterID = newItems.filterID;
                    temp.mainfilename = newItems.mainfilename;
                    temp.mesto.Add(ListOfOneFilterVsMain.IndexOf(newItems));
                    temp.gener.Add(newItems.currgeneration);
                    temp.skolkoZhili += 1;

                    this._ListofTotalOneFiltervsMain.Add(temp);

                }

               
            }

        }




    }
}
