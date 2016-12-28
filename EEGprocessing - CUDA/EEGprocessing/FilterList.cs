using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    public class FilterList : List<OneFilter>
    {

        private float _aver_fitness1;
        private float _aver_fitness2;
        private float _aver_fitness3;

        public float _maxfitness1;
        public float _maxfitness2;
        public float _maxfitness3;


        public float aver_fitness1{
        get {return this._aver_fitness1;}
        }

        public float aver_fitness2
        {
            get { return this._aver_fitness2; }
        }

        public float aver_fitness3
        {
            get { return this._aver_fitness3; }
        }


        public void CalcAverFitnes()
        {
            this._aver_fitness1=0;
            this._aver_fitness2=0;
            this._aver_fitness3=0;


            float max1 = 0;
            float max2 = 0;
            float max3 = 0;

            foreach (OneFilter item in this)
            {
                if (max1<= item.averOfDiffAver_for_rating)
                {
                    max1 = item.averOfDiffAver_for_rating;
                }

                if (max2 <= item.averOfMaxDiff_for_rating)
                {
                    max2 = item.averOfMaxDiff_for_rating;
                }

                if (max3<= item.averofABSMAX12_for_rating)
                {
                    max3 = item.averofABSMAX12_for_rating;

                }


                this._aver_fitness1 += item.averOfDiffAver_for_rating;
                this._aver_fitness2 += item.averOfMaxDiff_for_rating;
                this._aver_fitness3 += item.averofABSMAX12_for_rating;

            }

            this._maxfitness1 = max1;
            this._maxfitness2 = max2;
            this._maxfitness3 = max3;

            this._aver_fitness1 = this._aver_fitness1 / this.Count;
            this._aver_fitness2 = this._aver_fitness2 / this.Count;
            this._aver_fitness3 = this._aver_fitness3 / this.Count;

        
        }

        private float Average()
        {
            throw new NotImplementedException();
        }


        //короче генерируешь белый шум
        //[18.02.2014 11:39:32] Федор Пантелеев: из него интегрированием получаешь шумы типа 1/f  а дифференцированием- наоборот
        //[18.02.2014 11:39:54] Федор Пантелеев: чем болше окно интегрирования тем бвыше задираются низкие частоты
        //[18.02.2014 11:41:08] Belobrodsky Vladimir: то есть про дифференцировав белый шум что я получу?
        //[18.02.2014 11:41:20] Федор Пантелеев: синий и селёный шумы
        //[18.02.2014 11:42:05] Belobrodsky Vladimir: это тупа получится разница следубщей точки с текущей - так численно дифференцировать?
        //[18.02.2014 11:43:07] Федор Пантелеев: именно
        //[18.02.2014 11:43:12] Федор Пантелеев: или с определёным шагом
        //[18.02.2014 11:43:20] Федор Пантелеев: типа 1 и 5 5 и 10 и т.д.
        //[18.02.2014 11:44:32] Belobrodsky Vladimir: а фликкер - это каждая точка являеся суммой текущей и N послледующий, где N ширина окна ?
        //[18.02.2014 11:46:37] Федор Пантелеев: скорее предыдущих

        /// <summary>
        /// Каждому фильтры будет добавлен рандомный шум
        /// </summary>
        /// <param name="NoizeList">массив с индексами шумов.</param>
        public void AddRandomNoize(List<int> NoizeList)
        {
            Random r = new Random();
            Random r2 = new Random();
            for (int i = this.Count / 2; i < this.Count; i++) //Добавляем шумы ко второй половине фильтров
            {
                switch (r.Next(NoizeList.Count))
                {
                    case 0:
                        break;

                    case 1:
                        switch (NoizeList[1])
                        {
                            case 1: this.AddFlikkerNoize(i);
                                break;
                            case 2: this.AddBlueNoize(i);
                                break;
                            case 3:
                                int rtemp = i;
                                while (rtemp == i)
                                {
                                    rtemp = this.Count / 2 + r2.Next(this.Count / 2);
                                }
                                this.AddCrossingoverMutation(i, rtemp); //итак тут есть два различных фильтра
                                break;
                            default:
                                break;
                        }
           
                        break;
                    case 2:
                        switch (NoizeList[2])
                        {
                            case 1: this.AddFlikkerNoize(i);
                                break;
                            case 2: this.AddBlueNoize(i);
                                break;
                            case 3:
                                int rtemp = i;
                                while (rtemp == i)
                                {
                                    rtemp = this.Count / 2 + r2.Next(this.Count / 2);
                                }
                                this.AddCrossingoverMutation(i, rtemp); //итак тут есть два различных фильтра

                                break;

                            default:
                                break;
                        }
                       
                        break;
                    case 3:
                        switch (NoizeList[3])
                        {
                            case 1: this.AddFlikkerNoize(i);
                                break;
                            case 2: this.AddBlueNoize(i);
                                break;
                            case 3:
                                int rtemp = i;
                                while (rtemp == i)
                                {
                                    rtemp = this.Count / 2 + r2.Next(this.Count / 2);
                                }
                                this.AddCrossingoverMutation(i, rtemp); //итак тут есть два различных фильтра
                                break;
                            default:
                                break;
                        }
                  
                        break;
                    default:
                        break;
                }
             if (r.Next(2) == 1) this.AddShift(i);
            } // по всем нуждающимся фильтрам       
        }

        /// <summary>
        /// Добавляет фликкер шум к фильтру с индексом 
        /// </summary>
        /// <param name="filterIndex">индекс фильтра</param>
        private void AddFlikkerNoize(int filterIndex)
        {
            Random r = new Random();
            List<double> randarr = new List<double>();
            for (int j = 0; j < this[filterIndex].data.Count; j++) //Проходимся по длине каждого фильтра
            {
                randarr.Add(r.NextDouble());
                if (j > MyConst.BAND_FOR_FLIKKER_NOIZE)
                {
                    for (int k = j; k > j - MyConst.BAND_FOR_FLIKKER_NOIZE; k--)
                    {
                        this[filterIndex].data[j] += MyConst.SIGNAL_NOIZE_RATE * (float)randarr[k];
                    }
                } // пора добавлять шум
            }  //по длине фильтра
            this[filterIndex].Normalize(); //ну и конечно нормализация
            this[filterIndex].mutation = this[filterIndex].mutation + " FlikkerNoize |";
        }  //AddFlikkerNoize

        /// <summary>
        /// Добавим синий шум в фильтр по индексу
        /// </summary>
        /// <param name="filterIndex">индекс фильтр</param>
        private void AddBlueNoize(int filterIndex)
        {
            Random r = new Random();
            List<double> randarr = new List<double>();
            for (int j = 0; j < this[filterIndex].data.Count; j++) //Проходимся по длине каждого фильтра
            {
                randarr.Add(r.NextDouble());
                if (j > 0) this[filterIndex].data[j] += MyConst.SIGNAL_NOIZE_RATE * ((float)randarr[j] - (float)randarr[j - 1]);
            }  //по длине фильтра
            this[filterIndex].Normalize(); //ну и конечно нормализация
            this[filterIndex].mutation = this[filterIndex].mutation + " BlueNoize |";
        }  //AddBlueNoize

        /// <summary>
        /// Сдвигает фильтр на случайную величину
        /// </summary>
        /// <param name="filterIndex">индекс фильтра</param>
        private void AddShift(int filterIndex)
        {
            Random r = new Random();
            int shift = new int();
            shift = r.Next(this[filterIndex].data.Count); //номер позиции 
            List<float> temparr = new List<float>(); //динамический массив из флоат        
            for (int j = shift; j < this[filterIndex].data.Count; j++) //Проходимся по длине каждого фильтра
            {
                temparr.Add(this[filterIndex].data[j]);
            } //for j подлине фильтра
            for (int j = 0; j < shift; j++) //Проходимся по длине каждого фильтра
            {
                temparr.Add(this[filterIndex].data[j]);
            } //for j подлине фильтра
            this[filterIndex].data = temparr;
            this[filterIndex].mutation = this[filterIndex].mutation + " Shift |";
        } //addShift     


        /// <summary>
        /// Дабавляет мутацию типа кроссинговера, то есть нижние половины двух фильтров меняются 
        /// </summary>
        /// <param name="filterIndex">индекс 1 фильтра</param>
        /// <param name="filterIndex2">индекс 2 фильтра</param>
        private void AddCrossingoverMutation(int filterIndex, int filterIndex2)
        {
            Random r = new Random();
            List<float> temparr1 = new List<float>(); //динамический массив из флоат для нового фильтр1
            List<float> temparr2 = new List<float>(); //динамический массив из флоат для нового фтльтр2

            for (int j = 0; j < this[filterIndex].data.Count; j++) //Проходимся по длине каждого фильтра
            {
                if (j < this[filterIndex].data.Count / 2)
                {
                    temparr1.Add(this[filterIndex].data[j]);
                    temparr2.Add(this[filterIndex2].data[j]);
                }
                else
                {
                    temparr1.Add(this[filterIndex2].data[j]);
                    temparr2.Add(this[filterIndex].data[j]);
                }
            } //for j подлине фильтра            
            this[filterIndex].data = temparr1;
            this[filterIndex2].data = temparr2;

            this[filterIndex].Normalize(); //ну и конечно нормализация
            this[filterIndex2].Normalize(); //ну и конечно нормализация

            this[filterIndex].mutation = this[filterIndex].mutation + " Crosover vs " + this[filterIndex2].ID.ToString() + "|";
            this[filterIndex2].mutation = this[filterIndex2].mutation + " Crosover vs " + this[filterIndex].ID.ToString() + "|";
        
        } //добавление кроссинговера
    } //public class FilterList
} //namespace
