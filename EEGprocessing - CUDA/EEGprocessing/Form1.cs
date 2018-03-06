using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EEGprocessing
{
    public partial class Form1 : Form
    {
        private ListOfEegFiles mainstate; //= new MyMainStateData();

       // private ListOfEegFiles secondstate;  //ПОКА СОМНЕНИЯ ГДЕ ЭТО ВООБЩЕ ИСПОЛЬЗУЕТСЯ... ВОЗМОЖНО МОЖНО СТЕРЕТЬ

        private ListOfEegFiles controlCheck;

        int currentchanel = 0; //так как у нас загружен всего лишь один канал для проверки


        private ListOfFilters allfilters;// = new MyFilter();
        private List<ListOfEegFiles> allOtherStates;

        private ListOfEegFiles ROCmainstate; //= new MyMainStateData();
        private ListOfEegFiles ROCsecondstate;
        private List<float> ROCfilter;



        private ListOfSsStatistic mySSStatistic; //Препроцессорный класс для весов конечного поколения
        



        public List<int> myNoize;

        // int filterId;
        //public int NumOffilterFromForm2;
        // Guid idgenerator;
        public Form1()
        {
            //СРАЗУ ИНИЦИАЛИЗИРУЕМ ВСЕ СВОИ ОБЪЕКТЫ, ЧТОБЫ ОНИ БЫЛИ ДОСТУПНЫ ВЕЗДЕ
            this.mainstate = new ListOfEegFiles();
           // this.secondstate = new ListOfEegFiles(); //промежуточная переменная для каждого состояния
            this.controlCheck = new ListOfEegFiles();

            this.allOtherStates = new List<ListOfEegFiles>();
            this.myNoize = new List<int>();

            this.mySSStatistic = new ListOfSsStatistic();
            //this.myWeight = new ListofPara_F_O_Weight();
            InitializeComponent();
        }

        private void загрузитьОсновныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

          //  MessageBox.Show("Вычисления закончены");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    // Итак раз мы идем по всем файлам, то для каждого вайла заводим
                    //новый экземпляр класса MyOneFile
                    // OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    //теперь парсим этот файл, внося нужную информацию
                    // в теперь уже РЕАДЬНЫЙ класс FILE
                    myOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);  //второй стообец указывает какой по счету столбец, начиная с 1 будет закружен для анализа из ЭЭГ файла
                    //MessageBox.Show(myOnefilesignal.chanels[0].Count.ToString());
                    
                    mainstate.MyFiles.Add(myOnefilesignal); //Добавили в наш класс все сигналы
                } //foreach по всем файлам
            } // if openFiledialog

            // totalListOfHumanState.ListOfState.Add(mainstate);


            //foreach (OneFile item in mainstate.MyFiles)
            //{
            //    for (int i = 0; i < item.chanels[0].Count; i++)
            //    {
            //        listBox1.Items.Add(item.chanels[0][i]);
            //    }
            //}
            //listBox1.Items.Add(mainstate.MyFiles.Count);
        } // конец онклик загрузки основных //Загрузка дополнительных

        private void Load0genOfFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //сколько всего поколений---задается пользователем
            MyConst.COUNTOFGENERATION = (int)numericUpDown3.Value;
            //в этом массиве лежать число фильтров в каждом конкретно поколении. ОНО У НАС ОДИНАКОВОЕ, но можно заказать разные
            //for (int i = 0; i < MyConst.FILTERINGENERATION.Length; i++)
           // {
            MyConst.FILTERINGENERATION = (int)numericUpDown4.Value;
           // }


            this.allfilters = new ListOfFilters();
            int currentgeneration = 0;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //пройдемся по всем файлам
                int IDgenerator0 = 1;

                foreach (string filename in openFileDialog1.FileNames)
                {
                    //для каждого конуретного файла с фильтром давайте 
                    //создадим экземпляр класса стримридер
                    OneFilter newfilter = new OneFilter();
                    newfilter.ID = IDgenerator0; //присвоили айдишники фильтрам 
                    IDgenerator0++;
                    //так как у нулевого поколения предков нет 
                    //то предкам присвоим -1;
                    newfilter.fatherID = -1;
                    newfilter.motherID = -1;

                    Exception ex1 = newfilter.LoadFilterFromFile(filename);
                    if (ex1 != null)
                    {
                        MessageBox.Show(ex1.Message);
                    }

                    else
                    {
                        newfilter.Normalize();
                        allfilters.generationList[currentgeneration].Add(newfilter);
                    }

                   // MessageBox.Show(newfilter.data.Count.ToString());

                }  //foreach по всем файлам
            } //опенфайл диалог загрузки о поколения фильтров

            CalcAndCreate.Enabled = true;
        }

        private void print0genToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                allfilters.PrintOneGeneration(0, saveFileDialog1.FileName);
            }
        }


        //ЭТО БЫЛО ЗАКОМЕНТИРОВАНО ДЛЯ ЭКСПЕРИМЕНТА. НЕПОНЯТНО ДЛЯ ЧЕГО БЫЛ СОЗДАН НИЖНИЙ КУСОК

        ////кнопка загрузки первых дополнительных состояний
        //private void firstOtherToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        foreach (string filename in openFileDialog1.FileNames)
        //        {
        //            // Итак раз мы идем по всем файлам, то для каждого вайла заводим
        //            //новый экземпляр класса MyOneFile
        //            OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
        //            //теперь парсим этот файл, внося нужную информацию
        //            // в теперь уже РЕАЛЬНЫЙ класс FILE
        //            myOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);
        //            secondstate.MyFiles.Add(myOnefilesignal); //Добавили в наш экземпляр класса все сигналы              
        //        } //foreach по всем файлам
        //    } // if openFiledialog

        //    // totalListOfHumanState.ListOfState.Add(secondstate);
        //}


        ////сгенерируем первое поколение фильтров, для этого даже написан специальный метод
        //private void Create1GenerFrom0_Click(object sender, EventArgs e)
        //{

        //    //  Create2generatFrom1.Enabled = true;
        //    CalcAndCreate.Enabled = true;



        //    // MessageBox.Show(allfilters.generationList[1].Count.ToString());
        //    // MessageBox.Show(allfilters.generationList[1][0].ID.ToString());
        //}


        //private void Print1GenFilter_Click(object sender, EventArgs e)
        //{
        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        allfilters.PrintOneGeneration(1, saveFileDialog1.FileName);
        //    } //if save dialog
        //}



        /// <summary>
        /// Функция прорисовки фурье образов на крафике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawFilterById_Click(object sender, EventArgs e)
        {

            int fId = new int();
            fId = (int)numericUpDown1.Value;

            OneFilter currFilter = new OneFilter();
            currFilter = allfilters.getMyOneFilterById(fId);

            //listBox1.Items.Add("Фильтр Id = " + fId.ToString() + " от " + currFilter.fatherID.ToString() + " и " + currFilter.motherID.ToString());

            //Legend newLegent = new Legend();
            //newLegent.Name = "Filter ID = " + fId.ToString() + " от " + currFilter.fatherID.ToString() + " и " + currFilter.motherID.ToString();
            //newLegent.Title = newLegent.Name;
            //chart1.Legends.Add(newLegent);

            Series newSeries = new Series(); // ряд для свертки
            newSeries.Color = MyConst.ColorArray[chart1.Series.Count];
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.LegendText = "Filter ID = " + fId.ToString() + " от " + currFilter.fatherID.ToString() + " и " + currFilter.motherID.ToString();
            chart1.Series.Add(newSeries);

            for (int i = 0; i < currFilter.countOfFilterData; i++)
            {
                chart1.Series[chart1.Series.Count - 1].Points.AddXY(i, currFilter.data[i]);
            }

            List<float> furierArray = new List<float>();

            furierArray = MyConst.CsharpDFT(currFilter.data);


            //ДОБАВИМ НОВУЮ ОБЛАСТЬ ДЛЯ ФУРЬЕ ОБРАЗОВ
            Series newSeriesFur = new Series();
            newSeriesFur.ChartType = SeriesChartType.Column;
            newSeriesFur.Color = MyConst.ColorArray[chart2.Series.Count];

            chart2.Series.Add(newSeriesFur); //новый ряд данных для Фурье

            ChartArea newChartArea = new ChartArea();
            chart2.ChartAreas.Add(newChartArea);// новый область для Фурье

            chart2.Series[chart2.Series.Count - 1].ChartArea = chart2.ChartAreas[chart2.ChartAreas.Count - 1].Name;

            for (int i = 0; i < currFilter.countOfFilterData - 1; i++)
            {
                chart2.Series[chart2.Series.Count - 1].Points.AddXY(i, furierArray[i]);
            }

            //КОНЕЦ ОБЛАСТИ ДЛЯ ФУРЬЕ ОБРАЗОВ
        }

        /// <summary>
        /// Кнопка очистки графиков от фурье образов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
        }



        /// <summary>
        /// Добавляем сигналы второго дополнительного состояния
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToCompare_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                ListOfEegFiles oneOtherState = new ListOfEegFiles();

                foreach (string filename in openFileDialog1.FileNames)
                {
                    // Итак раз мы идем по всем файлам, то для каждого вайла заводим
                    //новый экземпляр класса MyOneFile
                    OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    //теперь парсим этот файл, внося нужную информацию
                    // в теперь уже РЕАЛЬНЫЙ класс FILE
                    myOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);
                   // MessageBox.Show(myOnefilesignal.chanels[0].Count.ToString());
                    oneOtherState.MyFiles.Add(myOnefilesignal); //Добавили в наш экземпляр класса все сигналы              
                } //foreach по всем файлам

                allOtherStates.Add(oneOtherState);
                //  MessageBox.Show(allOtherStates[allOtherStates.Count-1].MyFiles[0].chanels[0].Count.ToString());
            } // if openFiledialog

        }






        private void CalcAndCreate_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked) MyConst.whatIsFitness = 1;
            if (radioButton2.Checked) MyConst.whatIsFitness = 2;
            if (radioButton3.Checked) MyConst.whatIsFitness = 3;

            myNoize.Add(0); //если ничего не делать, но мутация нужна
            if (ChBoxFlikker.Checked) { myNoize.Add(1); }
            if (chBoxBlue.Checked) { myNoize.Add(2); }
            if (chBoxCrossingover.Checked) { myNoize.Add(3); }


            //сгенерируем 1 - ое поколение фильтров
            allfilters.CreateNextGenerationSimple(1, myNoize);

            //  int currentgeneration = 1;

            int currentchanelabsolute = (int)numericUpDown2.Value;



            //Для того чтобы в одном файле лежало по поколениям среднее значение фитнесс функции
            StreamWriter myFitnesswriter = new StreamWriter("GenerationFiltnessAverage.csv", false, Encoding.GetEncoding("Windows-1251"));
            myFitnesswriter.WriteLine("Номер поколения; Средний фитнесс1; Средний фитнесс2; Средний фитнесс3; Макс. фитнес 1; Макс. фитнес 2; Макс. фитнес 3");


            //в этом файле будут писаться пары Ф-ОС1 с фитнесс значениями
            StreamWriter myFO = new StreamWriter("myFO.csv", false, Encoding.GetEncoding("Windows-1251"));

            //т.е. например пара Ф-ОС жила 6 поколений и была 5 6 7 8 9 10
            //среднее место 7.5
            //и веди учет числа поколений жизни
            TotalFOStatistic myTotalFOStatistic = new TotalFOStatistic(); //Старая практически не актуальная 

            bool dosro_exit;
            //на каждой итерации этого цикла будет происходить работа всех фильтров, их ранжирование на основе результатов работ, и гненерация следующего поколения
            for (int currentgeneration = 1; currentgeneration < MyConst.COUNTOFGENERATION; currentgeneration++)
            {

                ///Определим критерий досрочного выхожа из генетического алгоритма
                /// 
                dosro_exit = true; //пускай по умолчанию подразумевается что сходится



                for (int i = 0; i < allfilters.generationList[currentgeneration][0].data.Count; i++)
                {

                    for (int j = 1; j < allfilters.generationList[currentgeneration].Count; j++)
                    {

                        if (Math.Abs(allfilters.generationList[currentgeneration][j-1].data[i]- allfilters.generationList[currentgeneration][j].data[i])>MyConst.eps)
                        {
                            dosro_exit = false; // ан нет не хрена
                        }

                    }

                }


                if (dosro_exit)
                {
                    MessageBox.Show("Сходимость достигнута");
                    break;
                }

                //foreach (OneFilter currfilter in allfilters.generationList[currentgeneration])
                //{
                //}



                /// Экземляр класса в котором каждое поколение будет хранится фторая фитнесс функция Ф-ОС1
                List<OneFilterVsMain> ListOfFilterVsMain = new List<OneFilterVsMain>();//для второй фитнесс функции ФО


                //встпомогательный временный массив для текущей максимальной и средней разницы разницы между каналами
                List<float> curr_diff_array = new List<float>();
                float maxvalue = new float(); //вспомогательная временная переменная

                List<int> criticalTime = new List<int>();


                string tempDirresult = @"Результаты по поколениям/";

                if (!Directory.Exists(tempDirresult))
                {
                    Directory.CreateDirectory(tempDirresult);
                }


                StreamWriter myWriterTotal = new StreamWriter(tempDirresult + "total_result_for_ch_" + currentchanelabsolute.ToString() + "_pokolenie_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));
                myWriterTotal.WriteLine("Результаты всех фильтров " + currentgeneration.ToString() + "поколения созданных на основе " + (currentgeneration - 1).ToString() + " -го для канала №=" + currentchanelabsolute.ToString());

                myWriterTotal.Write("#Поколения фильтра;");//0
                myWriterTotal.Write("ID Фильтра;");//1
                myWriterTotal.Write("ID Отца;");//1
                myWriterTotal.Write("ID Матери;");//1
                myWriterTotal.Write("Среднее значение фильтра;");//1
                myWriterTotal.Write("Норма фильтра;");//1
                myWriterTotal.Write("Состояние:;");//2
                myWriterTotal.Write("Имя файла;");//2
                myWriterTotal.Write("Состояние:;");//2
                myWriterTotal.Write("Имя файла;");//3
                myWriterTotal.Write("Среднее значение свертки основного;");//4
                myWriterTotal.Write("Среднее значение свертки дополнительного;");//5
                myWriterTotal.Write("МАХ свертки основного;");//6
                myWriterTotal.Write("Точка Max основного;");//7
                myWriterTotal.Write("Max свертки дополнительного;");//8
                myWriterTotal.Write("Точка Max дополнительного;");//9
                myWriterTotal.Write("Среднее значение разницы сверток;");
                myWriterTotal.Write("Мах разница сверток;");//11
                myWriterTotal.Write("Точка в которой мах разница сверток;");//12
                myWriterTotal.Write("ABS(ABS(МАКС) -ABS(МАКС));");//12
                myWriterTotal.WriteLine();

                /*  Будем искать ээфективность фильтра */
                //конечно же в самом начале надо пробежаться по ВСЕМ фильтрам
                //одного конкретного поколения, эффективность которого проверяется

                int aaa;

                string tempDirMax1 = @"Максимальные значениея сверток/1/";

                if (!Directory.Exists(tempDirMax1))
                {
                    Directory.CreateDirectory(tempDirMax1);
                }

                string tempDirMax2 = @"Максимальные значениея сверток/2/";

                if (!Directory.Exists(tempDirMax2))
                {
                    Directory.CreateDirectory(tempDirMax2);
                }

                StreamWriter myWriterMaxConvolvemain = new StreamWriter(tempDirMax1 + "Main_Max_conv_for_ch_" + currentchanelabsolute.ToString() + "_pokolenie_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));
                StreamWriter myWriterMaxConvolve = new StreamWriter(tempDirMax2 + "Other_Max_conv_for_ch_" + currentchanelabsolute.ToString() + "_pokolenie_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));


                ///По всем в природе фильтрам в текущем поколении
                foreach (OneFilter currfilter in allfilters.generationList[currentgeneration])
                {
                    List<float> temp_aver_for_rating = new List<float>(); // эти три массива живут в области видимости одного фильтра в него заносятся результаты каждой пары файлов
                    List<float> temp_max_for_rating = new List<float>();
                    List<float> temp_ABSMAX12_for_rating = new List<float>(); //для третьего критерия

                    foreach (OneFile currmainfile in mainstate.MyFiles)
                    {

                        //ДЛя второй фитнесс функции Ф-ОС1
                        OneFilterVsMain tempOneFilterVsMain = new OneFilterVsMain();
                        tempOneFilterVsMain.filterID = currfilter.ID;
                        tempOneFilterVsMain.mainfilename = currmainfile.filename;
                        tempOneFilterVsMain.currgeneration = currentgeneration;


                        /*Подсчет свертки с текущим файлом основного
                         Либо по КУДЕ либо без КУДЫ
                         * НАПОМНЮ ЧТО СЕЙЧАС РЕЗУЛЬТАТ СВЕРТКИ ОБРЕЗАЕТСЯ С НАЧАЛА И КОНЦА НА ДЛИНУ ФИЛЬТРА
                         */
                        if (chBoxCUDA.Checked)
                        {
                            float[] c = new float[currmainfile.chanels[currentchanel].Count];
                            aaa = MyConst.cuftGetConvolution(currmainfile.chanels[currentchanel].ToArray(), currmainfile.chanels[currentchanel].Count, currfilter.data.ToArray(), currfilter.data.Count, c);
                            currmainfile.convolve_chanels[currentchanel] = c.ToList<float>();
                        }
                        currmainfile.CalcConvolutionOneChanel(currfilter.data, currentchanel, currfilter.ID, chBoxCUDA.Checked);




                        // Печатаем в одтедльный файл текущее максимальное значение " @"Максимальные значениея сверток/1/Main_Max_conv_for_ch_"
                        if (!currmainfile.alreadyPrint) { myWriterMaxConvolvemain.WriteLine(currmainfile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";"); currmainfile.alreadyPrint = true; }//8


                        //Если стоит птичка печатаем свертки в файл  @"СВЕРТКИ/" + stateNumber.ToString() + "/";
                        if (chBoxPrintConv.Checked) currmainfile.PrintOneChanel(currfilter.data, currentchanel, currentchanelabsolute, "1");

                        int temp = 0;
                        int aaaa; //вспомогательная переменная для КУДЫ

                        foreach (ListOfEegFiles currListOfEegFiles in allOtherStates)
                        {
                            temp++;
                            foreach (OneFile currOneOtherFile in currListOfEegFiles.MyFiles)
                            {

                                /*считаю свертки текущим фильтром с файлом дополнительного состояния*/
                                if (chBoxCUDA.Checked)
                                {
                                    float[] cc = new float[currOneOtherFile.chanels[currentchanel].Count];
                                    aaaa = MyConst.cuftGetConvolution(currOneOtherFile.chanels[currentchanel].ToArray(), currOneOtherFile.chanels[currentchanel].Count, currfilter.data.ToArray(), currfilter.data.Count, cc);
                                    currOneOtherFile.convolve_chanels[currentchanel] = cc.ToList<float>();
                                }
                                currOneOtherFile.CalcConvolutionOneChanel(currfilter.data, currentchanel, currfilter.ID, chBoxCUDA.Checked);


                                //Печать в файл Максимальное значение свертки дополнительного состояния  " @"Максимальные значениея сверток/2/Other_Max_conv_for_ch_"
                                if (!currOneOtherFile.alreadyPrint) { myWriterMaxConvolve.WriteLine(currOneOtherFile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";"); currOneOtherFile.alreadyPrint = true; }//8

                                /*
                                 печать значение свертки в файл @"СВЕРТКИ/" + stateNumber.ToString() + "/";
                                 */
                                if (chBoxPrintConv.Checked) currOneOtherFile.PrintOneChanel(currfilter.data, currentchanel, currentchanelabsolute, "2");



                                // Ниже идет печать в total_result_for_ch_
                                myWriterTotal.Write(currentgeneration.ToString() + ";");//0
                                myWriterTotal.Write(currfilter.ID.ToString() + ";");//1

                                myWriterTotal.Write(currfilter.fatherID.ToString() + ";");//1
                                myWriterTotal.Write(currfilter.motherID.ToString() + ";");//1


                                myWriterTotal.Write(currfilter.data.Average().ToString() + ";");//1

                                myWriterTotal.Write(currfilter.norm + ";");//1


                                myWriterTotal.Write("ОС-1:;");//1
                                myWriterTotal.Write(System.IO.Path.GetFileName(currmainfile.filename) + ";");//2

                                myWriterTotal.Write(temp.ToString() + " дополнительное:;");//1
                                myWriterTotal.Write(System.IO.Path.GetFileName(currOneOtherFile.filename) + ";");//3

                                //тут надо заменить без учета границ
                                myWriterTotal.Write(currmainfile.MyOneChanelStatostic[currentchanel].convolve_average.ToString() + ";");//4
                                myWriterTotal.Write(currOneOtherFile.MyOneChanelStatostic[currentchanel].convolve_average.ToString() + ";");//5

                                myWriterTotal.Write(currmainfile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";");//6
                                myWriterTotal.Write(currmainfile.MyOneChanelStatostic[currentchanel].convolve_max_time.ToString() + ";");//7

                                myWriterTotal.Write(currOneOtherFile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";");//8
                                myWriterTotal.Write(currOneOtherFile.MyOneChanelStatostic[currentchanel].convolve_max_time.ToString() + ";");//9

                                curr_diff_array.Clear();

                                for (int k = 0; k < currmainfile.convolve_chanels[currentchanel].Count; k++)
                                {
                                    curr_diff_array.Add(Math.Abs(currmainfile.convolve_chanels[currentchanel][k] - currOneOtherFile.convolve_chanels[currentchanel][k]));
                                }

                                myWriterTotal.Write(curr_diff_array.Average().ToString() + ";");//10


                                //критерий №1
                                temp_aver_for_rating.Add(curr_diff_array.Average()); //массив где хранятся средние разницы между свертками

                                maxvalue = curr_diff_array.Max();
                                //максимальная разница между свертками
                                temp_max_for_rating.Add(maxvalue); //критерий №2
                                //tempOneFilterVsMain.fitnessArray.Add(maxvalue);

                                //критерий №3
                                float kriteri3 = Math.Abs(Math.Abs(currmainfile.convolve_chanels[currentchanel].Max()) - Math.Abs(currOneOtherFile.convolve_chanels[currentchanel].Max()));
                                temp_ABSMAX12_for_rating.Add(kriteri3);


                                if (currentgeneration == MyConst.COUNTOFGENERATION - 1)
                                {
                                    mySSStatistic.AddOrInsert(currOneOtherFile.filename, currmainfile.filename, currfilter.ID, kriteri3);
                                }


                                //Второй критерий будет
                                tempOneFilterVsMain.fitnessArray.Add(kriteri3);

                                //массив где хранятся максимальные разницы сверток


                                myWriterTotal.Write(maxvalue.ToString() + ";");//11

                                myWriterTotal.Write(curr_diff_array.IndexOf(maxvalue) + ";");//12

                                myWriterTotal.Write(kriteri3);//13

                                criticalTime.Add(curr_diff_array.IndexOf(maxvalue)); //массив где хранятся временя в которых максимальные отличия

                                myWriterTotal.WriteLine();


                            } //форич по всем файлам каждого конкретно дополнительного состояния состояния



                        } //форич по числу всех дополнительных состояниям

                        tempOneFilterVsMain.CalcFiltess();
                        myFO.WriteLine(tempOneFilterVsMain.filterID + ";" + tempOneFilterVsMain.mainfilename + ";" + tempOneFilterVsMain.fitnessArray.Average());

                        ListOfFilterVsMain.Add(tempOneFilterVsMain);

                    }//foreach по всем файлам Основного состояниям

                    //посчитали свертку для каждого файла первого дополнительного

                    //======================обнулим созданные свертки для лбеспечения возмодности использоваь адд арай листа=========================== 
                    for (int i = 0; i < MyConst.COUNTOFCHANEL; i++)
                    {

                        // StreamWriter myWriterMaxConvolvemain = new StreamWriter("Main_Max_conv_for_ch_" + currentchanelabsolute.ToString() + "_pokolenie_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));
                        foreach (OneFile myfile in mainstate.MyFiles)
                        {
                            //   myWriterMaxConvolvemain.Write(myfile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";");//8
                            myfile.convolve_chanels[i].Clear();
                            myfile.alreadyPrint = false;
                        }//foreach по всем файлам
                        //   myWriterMaxConvolvemain.Close();

                        //============================

                        foreach (ListOfEegFiles currListOfEegFiles in allOtherStates)
                        {
                            // StreamWriter myWriterMaxConvolve = new StreamWriter("Other_Max_conv_for_ch_" + currentchanelabsolute.ToString() + "_pokolenie_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));
                            foreach (OneFile currOneOtherFile in currListOfEegFiles.MyFiles)
                            {
                                //      myWriterMaxConvolve.Write(currOneOtherFile.MyOneChanelStatostic[currentchanel].convolve_max.ToString() + ";");//8
                                currOneOtherFile.convolve_chanels[i].Clear();
                                currOneOtherFile.alreadyPrint = false;
                            }
                            //  myWriterMaxConvolve.Close();
                        }

                        //=============================

                    }  // for i обнуления сверток для обеспечения возможности использовать новые фильтры


                    currfilter.averOfDiffAver_for_rating = temp_aver_for_rating.Average(); //в совойства фидьтра заносим среднее значение среди средних разниц
                    currfilter.averOfMaxDiff_for_rating = temp_max_for_rating.Average(); // в свойства фильтра заномис среднее значение среди максимальных разниц
                    currfilter.averofABSMAX12_for_rating = temp_ABSMAX12_for_rating.Average(); //в свойство фильтра заносим среднее значение среди значений типа abs(MAX1-MAX2)    

                } // по всем фильтрам в природе, принадлжежащим ОДНОМУ поколению
                //*****************************************************************========================**********************************************

                myWriterMaxConvolvemain.Close();
                myWriterMaxConvolve.Close();
                myWriterTotal.Close();
                //=====================================================================================================================================


                //Эта мега крутая штука называется LINQ
                //var sortedFilters =
                //from d in allfilters.generationList[currentgeneration]
                //orderby d.averOfDiffAver_for_rating descending
                //select d;



                ListOfFilterVsMain.Sort();
                ListOfFilterVsMain.Reverse();
                myTotalFOStatistic.AddOrInsert(ListOfFilterVsMain);

                allfilters.generationList[currentgeneration].Sort();
                allfilters.generationList[currentgeneration].Reverse();

                string tempDirSort = @"Отсортированные по поколениям/";

                if (!Directory.Exists(tempDirSort))
                {
                    Directory.CreateDirectory(tempDirSort);
                }


                StreamWriter myOtsortirovPoPokol = new StreamWriter(tempDirSort + "отсортированные_фильтры_gen_" + currentgeneration.ToString() + ".csv", false, Encoding.GetEncoding("Windows-1251"));

                myOtsortirovPoPokol.WriteLine(";;Фитнесс1;Фитнесс2;Фитнесс3");
                myOtsortirovPoPokol.Write("Фильтр Id;");
                myOtsortirovPoPokol.Write("Была ли мутация?;");


                myOtsortirovPoPokol.Write("Среднее среди СРЕДНИХ разниц;");
                myOtsortirovPoPokol.Write("Среднее среди MAX разниц;");
                myOtsortirovPoPokol.WriteLine("Среднее среди ABS(ABS(МАКС) -ABS(МАКС))");

                foreach (OneFilter sortFilter in allfilters.generationList[currentgeneration])
                {
                    myOtsortirovPoPokol.Write(sortFilter.ID + ";");
                    myOtsortirovPoPokol.Write(sortFilter.mutation + ";");
                    myOtsortirovPoPokol.Write(sortFilter.averOfDiffAver_for_rating.ToString() + ";");
                    myOtsortirovPoPokol.Write(sortFilter.averOfMaxDiff_for_rating.ToString() + ";");
                    myOtsortirovPoPokol.WriteLine(sortFilter.averofABSMAX12_for_rating.ToString());
                }
                myOtsortirovPoPokol.Close();


                if (currentgeneration < MyConst.COUNTOFGENERATION - 1) { allfilters.CreateNextGeneration(currentgeneration + 1, (float)(numericUpDown5.Value/100), myNoize,(int)numericUpDown4.Value); }

                allfilters.PrintOneGeneration(currentgeneration, "filter_" + currentgeneration.ToString() + ".csv");

                //посчитаем статистику для текущего поколения
                allfilters.generationStatistic[currentgeneration].CalcAbsoluteDevTime(criticalTime);
                criticalTime.Clear();

                allfilters.generationList[currentgeneration].CalcAverFitnes();
                myFitnesswriter.WriteLine(currentgeneration + ";" + allfilters.generationList[currentgeneration].aver_fitness1 + ";" + allfilters.generationList[currentgeneration].aver_fitness2 + ";" + allfilters.generationList[currentgeneration].aver_fitness3 + ";" + allfilters.generationList[currentgeneration]._maxfitness1 + ";" + allfilters.generationList[currentgeneration]._maxfitness2 + ";" + allfilters.generationList[currentgeneration]._maxfitness3);


            }  // for currentgeneration ПО ВСЕМ ПОКОЛЕНИЯМ
            myFO.Close();
            myFitnesswriter.Close();


            //Ну и тут после окончания все поколений модно вывести на печать результаты myTotalStatisticVsMain
            myTotalFOStatistic.ListofTotalOneFiltervsMain.Sort();
            myTotalFOStatistic.ListofTotalOneFiltervsMain.Reverse();
            myTotalFOStatistic.printTotalFOStatistic();


            //ну и выведем результат статистики по времени для каждого поколения
            GenerationStat.PrintStatToFile("статистика_поколений.csv", allfilters.generationStatistic);




            // ЗАПИСЬ ОТ 180305 ДОБАВЛЕНИЕ И СОХРАНЕНИЕ В ГРАФИЧЕСКИЙ ФАЙЛ СПЕКТРОВ ИСХОДНЫХ СИГНАЛОВ, НАЧАЛЬНЫХ И КОНЕЧНЫХ ФИЛЬТРОВ


            нарисоватьСпектрToolStripMenuItem_Click(this, null);

           // MessageBox.Show(secondstate.MyFiles.Count.ToString());







        }


        private void AddNewFAtoChart(FurierSpector.FurierAnalys FA, Color cl, int width, SeriesChartType sct, int num)
        {
            Series newFurie = new Series();
            newFurie.ChartType = sct;
            newFurie.Color = cl;
            newFurie.MarkerSize = width;

            newFurie.YAxisType = AxisType.Primary;


            int M = FA.ReFurier.Count / 2;

            int tDescret = (int)numericUpDown6.Value;


            for (int i = 0; i < M; i++)
            {
                //  t = 500 * i / M;
                newFurie.Points.AddXY((i + 1) * tDescret / FA.ReFurier.Count, (FA.ReFurier[i] * FA.ReFurier[i] + FA.ImFurier[i] * FA.ImFurier[i])/num);
                //sw2.WriteLine(FA.ReFurier[i] * FA.ReFurier[i] + FA.ImFurier[i] * FA.ImFurier[i]);
            }

            // sw2.Close();

            chart2.Series.Add(newFurie);
            //  chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisX.
            //  chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            // chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 10;
            //   chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            //   chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;

            chart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;

            //  chart1.ChartAreas[0].AxisX.Maximum = 100;
            //   chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
            //  chart1.ChartAreas[0].AxisY.Minimum = 1E-2;


        }




        private void LoadRoc1_Click(object sender, EventArgs e)
        {
            ROCmainstate = new ListOfEegFiles();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    // Итак раз мы идем по всем файлам, то для каждого вайла заводим
                    //новый экземпляр класса MyOneFile
                    // OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    OneFile ROCmyOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    //теперь парсим этот файл, внося нужную информацию
                    // в теперь уже РЕАДЬНЫЙ класс FILE
                    ROCmyOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);
                    ROCmainstate.MyFiles.Add(ROCmyOnefilesignal); //Добавили в наш класс все сигналы
                } //foreach по всем файлам
            } // if openFiledialog

            // totalListOfHumanState.ListOfState.Add(mainstate);

            // MessageBox.Show(ROCmainstate.MyFiles[0].chanels[0].Count.ToString());

        }

        private void LoadROC2_Click(object sender, EventArgs e)
        {
            ROCsecondstate = new ListOfEegFiles();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                foreach (string filename in openFileDialog1.FileNames)
                {
                    // Итак раз мы идем по всем файлам, то для каждого вайла заводим
                    //новый экземпляр класса MyOneFile
                    OneFile ROCmyOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    //теперь парсим этот файл, внося нужную информацию
                    // в теперь уже РЕАЛЬНЫЙ класс FILE
                    ROCmyOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);
                    ROCsecondstate.MyFiles.Add(ROCmyOnefilesignal); //Добавили в наш экземпляр класса все сигналы              
                } //foreach по всем файлам

                //  MessageBox.Show(allOtherStates[allOtherStates.Count-1].MyFiles[0].chanels[0].Count.ToString());
            } // if openFiledialog



        }

        private void LoadROCFilter_Click(object sender, EventArgs e)
        {
            ROCfilter = new List<float>();


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                StreamReader mr = new StreamReader(openFileDialog1.FileName);

                string line = "";
                float value;// = new float[MyConst.TOTALCOUNTOFCHANEL];


                while (line != null)// (i < MyConst.FILELENGTHLIMIT)//(line!=null)
                {
                    line = mr.ReadLine();
                    if (line != null)
                    {
                        try
                        {
                            value = float.Parse(line); //line.Split('\t').Select(n => float.Parse(n)).ToArray();

                        }
                        catch (Exception)
                        {
                            value = MyConst.ERRORVALUE;
                        }


                        if (value != MyConst.ERRORVALUE)
                        {
                            ROCfilter.Add(value);
                        }

                    }// if line не равно null
                }//while по всем строкам файла 


            }
            //Начало нормализации


            float sr = ROCfilter.Average();
            //для 0 среднего
            for (int i = 0; i < ROCfilter.Count; i++)
            {
                ROCfilter[i] = ROCfilter[i] - sr;
            }




            float A = new float();

            A = (float)0;
            //для нахождения энергии начального
            for (int i = 0; i < ROCfilter.Count; i++)
            {
                A = A + ROCfilter[i] * ROCfilter[i];
            }


            //для нормализации каждого
            for (int i = 0; i < ROCfilter.Count; i++)
            {
                ROCfilter[i] = ROCfilter[i] / ((float)Math.Sqrt(A));
            }




        }

        private void ROCcalc_Click(object sender, EventArgs e)
        {
            //Итак что у нас есть:

            //ROCmainstate = new ListOfEegFiles();
            //ROCsecondstate = new ListOfEegFiles();
            //ROCfilter = new List<float>();

            //теперь для Roc-анализа нужно посчитать свертки все файлов с эти конкретно фильтром
            int aaa = new int();


            StreamWriter mywr2 = new StreamWriter("ABS_MAX_KS.csv", false, Encoding.GetEncoding("Windows-1251"));
            List<float> temp = new List<float>(); //все максимальные значения, для нахождения макимума из максимумов

            List<float> tempMain = new List<float>();
            List<float> tempOther = new List<float>();

            foreach (OneFile currmainfile in ROCmainstate.MyFiles)
            {
                if (chBoxCUDA.Checked)
                {
                    float[] c = new float[currmainfile.chanels[0].Count];
                    aaa = MyConst.cuftGetConvolution(currmainfile.chanels[0].ToArray(), currmainfile.chanels[0].Count, ROCfilter.ToArray(), ROCfilter.Count, c);
                    currmainfile.convolve_chanels[0] = c.ToList<float>();
                }
                currmainfile.CalcConvolutionOneChanel(ROCfilter, 0, -2, chBoxCUDA.Checked);
                mywr2.WriteLine(Math.Abs(currmainfile.MyOneChanelStatostic[0].convolve_max) + ";1");
                temp.Add(currmainfile.MyOneChanelStatostic[0].convolve_max);

                tempMain.Add(currmainfile.MyOneChanelStatostic[0].convolve_max);

                currmainfile.PrintOneChanel(ROCfilter, 0, 1, "ROC");

            }


            foreach (OneFile currOtherfile in ROCsecondstate.MyFiles)
            {
                if (chBoxCUDA.Checked)
                {
                    float[] c = new float[currOtherfile.chanels[0].Count];
                    aaa = MyConst.cuftGetConvolution(currOtherfile.chanels[0].ToArray(), currOtherfile.chanels[0].Count, ROCfilter.ToArray(), ROCfilter.Count, c);
                    currOtherfile.convolve_chanels[0] = c.ToList<float>();
                }

                currOtherfile.CalcConvolutionOneChanel(ROCfilter, 0, -2, chBoxCUDA.Checked);


                mywr2.WriteLine(Math.Abs(currOtherfile.MyOneChanelStatostic[0].convolve_max) + ";2");
                temp.Add(currOtherfile.MyOneChanelStatostic[0].convolve_max);
                tempOther.Add(currOtherfile.MyOneChanelStatostic[0].convolve_max);

                currOtherfile.PrintOneChanel(ROCfilter, 0, 1, "ROC");
            }

            mywr2.Close();

            float AbsMaxConv = temp.Max();

            // float AbsCountMainState = (float)ROCmainstate.MyFiles.Count * ROCmainstate.MyFiles[0].convolve_chanels[0].Count;
            // float AbsCountSecondState = (float)ROCsecondstate.MyFiles.Count * ROCsecondstate.MyFiles[0].convolve_chanels[0].Count;

            float AbsCountMainState = (float)ROCmainstate.MyFiles.Count;// *ROCmainstate.MyFiles[0].convolve_chanels[0].Count;
            float AbsCountSecondState = (float)ROCsecondstate.MyFiles.Count;// *ROCsecondstate.MyFiles[0].convolve_chanels[0].Count;


            float RocCountMain = 0;
            float RocCountOther = 0;

            float sensor = new float();
            float specify = new float();

            StreamWriter mywr = new StreamWriter("Roc_Anylize.csv", false, Encoding.GetEncoding("Windows-1251"));

            for (int i = 10; i < 101; i = i + 10)
            {
                mywr.WriteLine();
                mywr.WriteLine("Порог " + i.ToString() + "% от максимального значения;");

                RocCountMain = MyConst.RocCountOfInvolveArray(tempMain, i * AbsMaxConv / 100);
                RocCountOther = AbsCountSecondState - MyConst.RocCountOfInvolveArray(tempOther, i * AbsMaxConv / 100);

                mywr.Write(" ;ИС1;"); mywr.WriteLine("ИС2;");
                mywr.Write("ОС1;"); mywr.Write(RocCountMain + ";"); mywr.WriteLine(AbsCountSecondState - RocCountOther + ";");
                mywr.Write("ОС2;"); mywr.Write(AbsCountMainState - RocCountMain + ";"); mywr.WriteLine(RocCountOther + ";");

                sensor = (float)(RocCountMain / AbsCountMainState) * 100;
                specify = (float)(RocCountOther / AbsCountSecondState) * 100;
                mywr.WriteLine("Чувствительность = ;" + sensor.ToString());
                mywr.WriteLine("Специфичность =;" + specify.ToString());
            }



            mywr.Close();






        }

        private void контрольнаяПроверкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    // Итак раз мы идем по всем файлам, то для каждого вайла заводим
                    //новый экземпляр класса MyOneFile
                    // OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    OneFile myOnefilesignal = new OneFile(MyConst.COUNTOFCHANEL);
                    //теперь парсим этот файл, внося нужную информацию
                    // в теперь уже РЕАДЬНЫЙ класс FILE
                    myOnefilesignal.LoadDataFromFile(filename, (int)numericUpDown2.Value - 1);
                    controlCheck.MyFiles.Add(myOnefilesignal); //Добавили в наш класс все сигналы
                } //foreach по всем файлам
            }
            ///Итак мы только что загрузили третию группу файлов            
            /*
             * сейчас нужно проверить каждый файл из этой группы следующим образом:
             */
            /*теперь для каждого СС файла, после усечения у меня есть минимальное значение фитнесс функции. 
             * (то есть в файле 1_Total_F_OC_for_CC_25.csv самое последнее значение для каждого СС файла) 
             * Я нашел Минимум среди всех этих минимальных значений и получил так называемое одно ЧИСЛО  - "абсолютный минимум".
             * Это уже есть.
             * Теперь предположим у меня есть произвольный сигнал и стоит задача определить его принадлежность - либо к ОС либо к СС.
             * Я беру и прогоняю его через все ПАРЫ Ф-ОС из файла "F_OC_weght_25.csv". 
             * Для каждой прогона я получаю фитнесс значение.
             * У меня появляется массив фитнесс значения.
             * (f1,f2,f3.....fn) 
             * Далее я нахожу СРЕДНЕВЗВЕШЕННОЕ значение этого массива. (так как сумма всех весов 1, то просто нахужу w1*f1 + w2*f2+w3*f3+....+wn*fn).
             * Веса мы только что проверили.   
             * И в итоге, если это СРЕДНЕВЗВЕШЕННОЕ значение окажется меньше "абсолютный минимум", 
             * то я говорю что это сигнал принадлежит ОС, а в противном случаии СС.... Я представляю себе так.......
             */
            //Где храниться абсолютный минимум???? А вот здесь -- 
            //mySSStatistic.AbsoluteMinimum

            ////*********************ИТАК ЕСТЬ ВЕСА

            //ListOfSsStatistic mySSStatistic = new ListOfSsStatistic(); //Препроцессорный класс для весов конечного поколения
            mySSStatistic.mySort();
            //01.04.2014 вывод упорядоченных пар для каждого файла
            int aaa;
            float k = (float)0;



            string tempDirresult = @"Результаты по отсечкам/";

            if (!Directory.Exists(tempDirresult))
            {
                Directory.CreateDirectory(tempDirresult);
            }


            
            StreamWriter mywr = new StreamWriter("контрольная проверка.csv", false, Encoding.GetEncoding("Windows-1251"));
            mywr.WriteLine("Процент отсечки; Сколько определилось как СС; Сколько всего было загружено сигналов на опередение");

           
            
            for (int i = 5; i < 100; i=i+5)
            {
                k=(float)i/(float)100;
                ListOfSsStatistic tempSSStatistic = new ListOfSsStatistic();
              
                tempSSStatistic.copeFromAnother(mySSStatistic);

                // MessageBox.Show("mySSS:" + i.ToString()+ mySSStatistic.Count.ToString());
                // MessageBox.Show("tempSSS:" + i.ToString() + tempSSStatistic.Count.ToString());


                tempSSStatistic.Otsechka(k);
                tempSSStatistic.PrintTotal_F_OS(k);


               ListofPara_F_O_Weight myWeight = new ListofPara_F_O_Weight();

             myWeight.LoadFromListOfSsStatistic(tempSSStatistic);
              myWeight.Sort();
              myWeight.Reverse();
              myWeight.printWeght(k);

              StreamWriter myWriterTotal = new StreamWriter(tempDirresult + "total_result_po_otsechke_" + i.ToString()+".csv" , false, Encoding.GetEncoding("Windows-1251"));

              myWriterTotal.WriteLine("ИМЯ идентифицирующего файла из 3 группы; id фильтра пары; имя ОС файла пары; Максимальное значение свертки Иден. файла; максимальное значение свертки ОС файла пары; Фитнесс критерий(abs(abs(max1)-abs(max2))); ВЕС; ФИТНЕСС*ВЕС; СУММА(ФИТНЕСС*ВЕС); Абсолютный минимум");
               //Итак шаг первый - для каждого файла контрольной проверки....


               int totalTP = 0;
               foreach (OneFile oneCheckFile in controlCheck.MyFiles)
               {
                   float sum = 0; // тут будут накапливаться результаты голосования
               //    //полюбому нужно прогнать все пары, поэтому
                   foreach (ParaF_O_Weight paraitem in myWeight)
                   {

                       OneFilter filterItem = new OneFilter();
                       filterItem = allfilters.getMyOneFilterById(paraitem.individPara.filterId);



               //        //1 считаем свертка с чекфайлом                  
                       if (chBoxCUDA.Checked)
                       {
                           float[] c = new float[oneCheckFile.chanels[currentchanel].Count];
                           aaa = MyConst.cuftGetConvolution(oneCheckFile.chanels[currentchanel].ToArray(), oneCheckFile.chanels[currentchanel].Count, filterItem.data.ToArray(), filterItem.data.Count, c);
                           oneCheckFile.convolve_chanels[currentchanel] = c.ToList<float>();
                       }
                       oneCheckFile.CalcConvolutionOneChanel(filterItem.data, currentchanel, filterItem.ID, chBoxCUDA.Checked);
               //        ///Итак мы посчитали свертку текущего файла с текущим фильтром
               //        ///

               //        //Этап два. Нужно посчитать свертку внутри пары...

                       OneFile ocFileItem = new OneFile(1);
                       ocFileItem = mainstate.GetMyOneEegFileByFilename(paraitem.individPara.OCfilename);

                       if (chBoxCUDA.Checked)
                       {
                           float[] c = new float[ocFileItem.chanels[currentchanel].Count];
                           aaa = MyConst.cuftGetConvolution(ocFileItem.chanels[currentchanel].ToArray(), ocFileItem.chanels[currentchanel].Count, filterItem.data.ToArray(), filterItem.data.Count, c);
                           ocFileItem.convolve_chanels[currentchanel] = c.ToList<float>();
                       }
                       ocFileItem.CalcConvolutionOneChanel(filterItem.data, currentchanel, filterItem.ID, chBoxCUDA.Checked);



               //        //этап ТРИ
               //        //Нахождение фитнесса, так как две свертки уже есть

                       float kriteri3 = Math.Abs(Math.Abs(ocFileItem.convolve_chanels[currentchanel].Max()) - Math.Abs(oneCheckFile.convolve_chanels[currentchanel].Max()));

                       //myWriterTotal.WriteLine("ИМЯ идентифицирующего файла из 3 группы; id фильтра пары; имя ОС файла пары; Максимальное значение свертки Иден. файла; максимальное значение свертки ОС файла пары; Фитнесс критерий(abs(abs(max1)-abs(max2))); ВЕС; ФИТНЕСС*ВЕС ");
                      

                       sum += kriteri3 * paraitem.weight;
                       myWriterTotal.WriteLine(oneCheckFile.filename + ";" + paraitem.individPara.filterId.ToString() + ";" + paraitem.individPara.OCfilename.ToString() + ";" + oneCheckFile.convolve_chanels[currentchanel].Max().ToString() + ";" + ocFileItem.convolve_chanels[currentchanel].Max().ToString() + ";" + kriteri3.ToString() + ";" + paraitem.weight.ToString() + ";" + (kriteri3 * paraitem.weight).ToString() + ";"  + sum.ToString() + ";"+ tempSSStatistic.AbsoluteMinimum.ToString());

                   } //для каждой пары
               //    //когда все пары закончиличь примем решение о принадлежности данного файла

                   if (sum >= tempSSStatistic.AbsoluteMinimum)
                   {
                       totalTP += 1;
                   }


            } //по всем файлам третьей группе


               myWriterTotal.Close();
               mywr.WriteLine(100*k + ";" + totalTP + ";" + controlCheck.MyFiles.Count);
              

            }
            mywr.Close();   
            //mySSStatistic.PrintTotal_F_OS((float)1);
            //mySSStatistic.Otsechka((float)0.25);
            //mySSStatistic.PrintTotal_F_OS((float)0.25);


            //myWeight.LoadFromListOfSsStatistic(mySSStatistic);
            //myWeight.Sort();
            //myWeight.Reverse();
            //myWeight.printWeght((float)0.25);
            //ЗАКЛЮЧИТЕЛЬНАЯ ЧАСТЬ РАБОТЫ

            /*Нам нужны методы
             * Возвращение фильтра по ID --- 
             * public OneFilter getMyOneFilterById(int Id)
             * 
             * 
             * 
             * Возвращение сигнала ОС по его имени
             * GetMyOneEegFileByFilename(string filename)
             * */







        }

        private void нарисоватьСпектрToolStripMenuItem_Click(object sender, EventArgs e)
        {

           // chart2.ChartAreas[0].AxisY.Maximum = 200;



            //пробежимся по всем известным файлам сигналов стостояния №1
            foreach (var item in mainstate.MyFiles)
            {


                FurierSpector.FurierAnalys FA = new FurierSpector.FurierAnalys(item.chanels[0]);
                FA.Do();
                this.AddNewFAtoChart(FA, Color.Red, 8, SeriesChartType.Column, 1000);

            }




            foreach (var item in allOtherStates)
            {

                foreach (var item2 in item.MyFiles)
                {

                    // MessageBox.Show(item.chanels[0].Count.ToString());
                    FurierSpector.FurierAnalys FA = new FurierSpector.FurierAnalys(item2.chanels[0]);
                    FA.Do();
                    this.AddNewFAtoChart(FA, Color.Blue, 8, SeriesChartType.Column, 1000);
                }
            }


            foreach (var item in allfilters.generationList[0])
            {
                FurierSpector.FurierAnalys FA = new FurierSpector.FurierAnalys(item.data);
                FA.Do();
                this.AddNewFAtoChart(FA, Color.Green, 8, SeriesChartType.Column,1);
            }

            foreach (var item in allfilters.generationList[allfilters.generationList.Length - 1])
            {
                FurierSpector.FurierAnalys FA = new FurierSpector.FurierAnalys(item.data);
                FA.Do();
                this.AddNewFAtoChart(FA, Color.Black, 8, SeriesChartType.Column, 1);
            }


            chart2.SaveImage("Spectr.png",ChartImageFormat.Png);
        } //Нарисовать спектры


    } //класс FORM1
} // namespace
