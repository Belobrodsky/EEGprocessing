using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    public class ListOfFilters
    {
        private FilterList[] _generationList; //каждый элемент представляет собой список из фильтров
        private GenerationStat[] _generationStatistic;

        public ListOfFilters()
        {
            this._generationList = new FilterList[MyConst.COUNTOFGENERATION];
            this._generationStatistic = new GenerationStat[MyConst.COUNTOFGENERATION];

            //ИНИЦИАЛИЗИРУЕМ МЕСТО ПОД ХРАНЕНИЕ ФИЛЬТРОВ ВСЕХ ПОКОЛЕНИЙ
            for (int i = 0; i < MyConst.COUNTOFGENERATION; i++)
            {
                this._generationList[i] = new FilterList();
                this._generationStatistic[i] = new GenerationStat();
            }
        }


        /// <summary>
        /// Метод простой не умной генерации на основе случайных чисел
        /// </summary>
        /// <param name="nextgeneration">номер следующего поколения</param>
        public void CreateNextGenerationSimple(int nextgeneration, List<int> NoizeList)
        {
            //ИТАК НАЧНЕМ
            Random myRandomizer = new Random();
            int startId;
            startId = 1000 * nextgeneration + 1;
            int f, m;
            float myaver = new float();
            // цикл по кол-ву элементов в этом НОВОМ поколении
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                OneFilter myNewfilter = new OneFilter();
                OneFilter myFatherFilter = new OneFilter();
                OneFilter myMotherFilter = new OneFilter();
                f = 0;
                m = 0;
                while (f == m)
                {
                    f = myRandomizer.Next(MyConst.FILTERINGENERATION);
                    m = myRandomizer.Next(MyConst.FILTERINGENERATION);
                }
                myNewfilter.ID = startId;
                startId++;
                myFatherFilter = this.getMyOneFilterByGenerByIndex(nextgeneration - 1, f);
                myMotherFilter = this.getMyOneFilterByGenerByIndex(nextgeneration - 1, m);
                //пока механизм генерации очень прост                
                for (int j = 0; j < myFatherFilter.countOfFilterData; j++)
                {
                    myaver = (myFatherFilter.data[j] + myMotherFilter.data[j]) / 2;
                    myNewfilter.data.Add(myaver);
                }
                myNewfilter.countOfFilterData = myFatherFilter.countOfFilterData;
                myNewfilter.fatherID = myFatherFilter.ID;
                myNewfilter.motherID = myMotherFilter.ID;
                myNewfilter.Normalize();
                //  myNewfilter.CalcFurier();
                this.generationList[nextgeneration].Add(myNewfilter); // и самая главная на сегодня строчка
            } //цикл по кол-ву новых фильтров в поколении        

            int popitka = 0;
            while ((MyConst.isNessMutation(this._generationList[nextgeneration])) && (popitka < 1))
            {
                this._generationList[nextgeneration].AddRandomNoize(NoizeList);
                popitka++;
            }
        }



        /// <summary>
        /// Основная функция генерация
        /// </summary>
        /// <param name="nextgeneration"> номер будующего поколения </param>
        /// <param name="rate_skolko_ost_starih">Сколько процентов оставить старых от полного числа фильтра в этих поколениях</param>
        public void CreateNextGeneration(int nextgeneration, float rate_skolko_ost_starih, List<int> myNoize, int kolvovpokolenii)
        {
            //тут предполагаем что в в элементе 
            //generationlist[nextgeneration-1] уже сидят фильтры
            // и они упорядочены по убыванитю нужного нам критерия
            // добавим их сначала в наше новое поколение
            OneFilter tempFilter = new OneFilter();
            //for (int i = 0; i < rate_skolko_ost_starih * MyConst.FILTERINGENERATION[nextgeneration - 1]; i++)
            for (int i = 0; i < rate_skolko_ost_starih * kolvovpokolenii; i++)
            {
                tempFilter = this.getMyOneFilterByGenerByIndex(nextgeneration - 1, i);
                this.generationList[nextgeneration].Add(tempFilter);
            }
            Random myRandomizer = new Random();
            int startId;
            startId = 1000 * nextgeneration + 1;
            int f, m;
            float myaver = new float();
            //for (int i = 0; i < (1 - rate_skolko_ost_starih) * MyConst.FILTERINGENERATION[nextgeneration]; i++)
            for (int i = 0; i < (1 - rate_skolko_ost_starih) * kolvovpokolenii; i++)

            {
                OneFilter myNewfilter = new OneFilter();
                OneFilter myFatherFilter = new OneFilter();
                OneFilter myMotherFilter = new OneFilter();

                f = 0;
                m = 0;

                while (f == m)
                {
                    //f = myRandomizer.Next(MyConst.FILTERINGENERATION[nextgeneration - 1] / 2); //наполовину потому что фильтры отсортированы по крутизне в порядке убывания и нужно только от лучших потомки
                    //m = myRandomizer.Next(MyConst.FILTERINGENERATION[nextgeneration - 1] / 2);
                    f = myRandomizer.Next((int)Math.Round(kolvovpokolenii * rate_skolko_ost_starih)); //наполовину потому что фильтры отсортированы по крутизне в порядке убывания и нужно только от лучших потомки
                    m = myRandomizer.Next((int)Math.Round(kolvovpokolenii * rate_skolko_ost_starih));
                }
                myNewfilter.ID = startId;
                startId++;
                myFatherFilter = this.getMyOneFilterByGenerByIndex(nextgeneration - 1, f);
                myMotherFilter = this.getMyOneFilterByGenerByIndex(nextgeneration - 1, m);
                //пока механизм генерации очень прост                
                for (int j = 0; j < myFatherFilter.countOfFilterData; j++)
                {
                    myaver = (myFatherFilter.data[j] + myMotherFilter.data[j]) / 2;
                    myNewfilter.data.Add(myaver);
                }
                myNewfilter.countOfFilterData = myFatherFilter.countOfFilterData;
                myNewfilter.fatherID = myFatherFilter.ID;
                myNewfilter.motherID = myMotherFilter.ID;
                myNewfilter.Normalize();
                //   myNewfilter.CalcFurier();
                this.generationList[nextgeneration].Add(myNewfilter); // и самая главная на сегодня строчка
            }
            //на этом этапе новое поколение сформировано полностью 
            //теперь проверим его и при необходимости запустим мутацию


            int popitka = 0;
            while ((MyConst.isNessMutation(this._generationList[nextgeneration])) && (popitka < 1))
            {
                this._generationList[nextgeneration].AddRandomNoize(myNoize);
                popitka++;
            }
        }  //CreateNextGeneration




        /// <summary>
        /// Это функция напечатает ОДНО поколение фильтров из массива generationList
        /// Нужнов всего лишь указать номер поколения
        /// </summary>
        /// <param name="numOfGeneration">Укажите номер поколения фильтров, какой хотите напечатать</param>
        /// <param name="filename">Укажаите Имя файла в который хотите напечатать это поколение фильтров</param>
        public void PrintOneGeneration(int numOfGeneration, string filename)
        {

            string tempDir = @"Сами фильтры/";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }


            StreamWriter myWriter = new StreamWriter(tempDir + filename);
            myWriter.WriteLine("Generathion:;#" + numOfGeneration.ToString());
            //==========================================

            myWriter.Write("ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].ID);
                myWriter.Write(";");
            }

            //для фурье
            myWriter.Write("FURIER ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].ID);
                myWriter.Write(";");
            }//для Фурье


            myWriter.WriteLine();
            //==========================================
            //==========================================
            myWriter.Write("father ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].fatherID);
                myWriter.Write(";");
            }

            //для фурье
            myWriter.Write("father ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].fatherID);
                myWriter.Write(";");
            }
            //конец для фурье

            myWriter.WriteLine();

            //==========================================
            //==========================================
            myWriter.Write("mother ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].motherID);
                myWriter.Write(";");
            }

            //для Фурье
            myWriter.Write("mother ID = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].motherID);
                myWriter.Write(";");
            }
            //конец Фурье

            myWriter.WriteLine();


            //для мутации
            //==========================================
            myWriter.Write("Mutation = ");
            myWriter.Write(";");
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                myWriter.Write(this._generationList[numOfGeneration][i].mutation);
                myWriter.Write(";");
            }

            //для Фурье
            myWriter.Write("Mutation = ");
            myWriter.Write(";");
            //for (int i = 0; i < MyConst.FILTERINGENERATION[numOfGeneration]; i++)
            //{
            //    myWriter.Write(this._generationList[numOfGeneration][i].mutation);
            //    myWriter.Write(";");
            //}
            //конец Фурье

            myWriter.WriteLine();



            for (int j = 0; j < this._generationList[numOfGeneration][0].data.Count; j++)
            {
                myWriter.Write(";");

                for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
                {
                myWriter.Write(this._generationList[numOfGeneration][i].data[j]);
                myWriter.Write(";");
            }
            myWriter.Write(";");
            // фурье
            for (int i = 0; i < MyConst.FILTERINGENERATION; i++)
            {
                if (j < (this._generationList[numOfGeneration][i].furier.Count)) myWriter.Write(this._generationList[numOfGeneration][i].furier[j]);
                myWriter.Write(";");
            }
            //   фурье

            myWriter.WriteLine();
        }
        myWriter.Close();
        }

        /// <summary>
        /// Возвращает экземпляр класса MyOneFilter у которого поколение=gener и имеет порядковый номер в массиве index        /// </summary>
        /// <param name="gener">Номер поколения</param>
        /// <param name="index">Порядковый номер в массиве</param>
        /// <returns>Экземпляр класса MyOneFilter</returns>
        public OneFilter getMyOneFilterByGenerByIndex(int gener, int index)
        {
            return this._generationList[gener][index];
        }
        /// <summary>
        /// Из всего класса выберем конкретный фильтра по Id
        /// </summary>
        /// <param name="Id">Задайте конкретное значение Id у фильтра</param>
        /// <returns> Экземпляр класса MyOneFilter</returns>
        public OneFilter getMyOneFilterById(int Id)
        {
            int gener = 0;
            int index = 0;

            foreach (FilterList ListOfmyFiltes in this._generationList)
            {
                index = 0;
                foreach (OneFilter myfilter in ListOfmyFiltes)
                {
                    if (myfilter.ID == Id)
                    {
                        return this.getMyOneFilterByGenerByIndex(gener, index);
                    }
                    index++;
                }
                gener++;
            } //по всем поколениям
            return this.getMyOneFilterByGenerByIndex(0, 0);
        }
        public FilterList[] generationList
        {
            get { return this._generationList; }
            set { this._generationList = value; }
        }

        public GenerationStat[] generationStatistic
        {
            get { return this._generationStatistic; }
            set { this._generationStatistic = value; }
        }

    }
}
