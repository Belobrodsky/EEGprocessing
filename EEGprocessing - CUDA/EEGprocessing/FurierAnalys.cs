using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurierSpector
{



//Procedure inverseDTF(var Array_Re, Array_Im:Array of real; size:LongInt);
//Var
//t, f : LongInt;
//k,norm : Single;
//Temp_Array_Re,Temp_Array_Im : Array Of Single;
//Begin
//k:=2* pi/size;
//norm:=1/sqrt(size);
//SetLength(Temp_Array_Re, size);
//SetLength(Temp_Array_Im, size);
//    For t:=0 To size-1 Do
//    begin
//Temp_Array_Re[t]:=Array_Re[t];
//Temp_Array_Im[t]:=Array_Im[t];
//end;
//For t:=0 To size-1 Do
//Begin
//Array_Re[t]:=0;
//Array_Im[t]:=0;
//For f:=0 To size-1 Do
//begin
//Array_Re[t]:=Array_Re[t]+Temp_Array_Re[f]* cos(-k* f*t);
//    Array_Im[t]:=Array_Im[t]+Temp_Array_Re[f]* sin(-k* f*t);
//    end;
//Array_Re[t]:=norm* Array_Re[t];
//    Array_Im[t]:=norm* Array_Im[t];
//    End;
//End;

    
    class FurierAnalys
    {

        /// <summary>
        /// тут будет лежать исходный сигнал
        /// </summary>
        List<float> initialSignal;

        /// <summary>
        /// действительный часть спектра
        /// </summary>
        List<float> _ReFurier;
        /// <summary>
        /// Мнимая часть спектра
        /// </summary>
        List<float> _ImFurier;

        /// <summary>
        /// Сюда положиться восстановленный сигнал
        /// </summary>
        List<float> inverseTransform;


        List<float> _obraz;



        public void Print(string describ)
        {



            string tempDir = @"Матрица фурье образов по столбцам/";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }


            

            StreamWriter sw = new StreamWriter(tempDir + describ + ".csv");

            for (int i = 0; i < this._ReFurier.Count; i++)
            {
                sw.WriteLine(this._ReFurier[i]+";" + this._ImFurier[i]);
            }


            sw.Close();

        }


        public List<float> ReFurier
        {
            get
            {
                return _ReFurier;
            }
            set
            {
                _ReFurier = value;
            }
        }

        public List<float> ImFurier
        {
            get
            {
                return _ImFurier;
            }

            set
            {
                _ImFurier = value;
            }
        }

        public List<float> InverseTransform
        {
            get
            {
                return inverseTransform;
            }

            set
            {
                inverseTransform = value;
            }
        }

        public List<float> InitialSignal
        {
            get
            {
                return initialSignal;
            }

            set
            {
                initialSignal = value;
            }
        }

        public List<float> Obraz {
            
            get
            {
            return this._obraz;
            }
            
            set 
            {
            this._obraz=value;
            
            } }

        public FurierAnalys(List<float> signal)
        {
            this.InitialSignal = new List<float>();
            this.InitialSignal = signal;

            this._obraz = new List<float>();

            

            this._ReFurier = new List<float>();
            this._ImFurier = new List<float>();
        }



        /// <summary>
        /// тут запустим просчет фурье спектра
        /// </summary>
        public void Do()
        {

            int N = this.InitialSignal.Count;
  
            float temp;
            float tempIM;


            //            SetLength(b, 0);
            //            for i:= 0 to length(a) - 1 do
            //                    begin
            //            SetLength(b, length(b) + 1);
            //            temp:= 0;
            //            for j:= 0 to length(a) - 1 do
            //                    begin
            //            temp:= temp + a[j] * sin((2 * Pi * (j + 1) * (i + 1)) / length(a));
            //            end;
            //            b[length(b) - 1]:= temp;
            //            end;
            
            //отчет сигнала совпадает с отчетом спектра

            for (int i = 0; i < N; i++)
            {
                //this._ReFurier.Add(0);
               // this._ImFurier.Add(0);
                temp = 0;
                tempIM = 0;

                for (int j = 0; j < N; j++)
                {
                    //   temp += this.initialSignal[j] * Math.Sin((2*Math.PI*(j+1)*(i+1))/N);

                    temp += this.initialSignal[j] * (float)Math.Cos((2 * Math.PI * (j + 1) * (i + 1)) / N);

                    //this._ReFurier[t] += temp_array[f] * Math.Cos(k * f * t);
                    //this._ImFurier[t] += temp_array[f] * Math.Sin(k * f * t);
                    //temp:= temp + a[j] * cos((2 * Pi * (j + 1) * (i + 1)) / length(a));

                    tempIM += this.initialSignal[j] * (float)Math.Sin((2 * Math.PI * (j + 1) * (i + 1)) / N);
                }
                this._ReFurier.Add(temp);
                this._ImFurier.Add(tempIM);

                this._obraz.Add(temp*temp+tempIM*tempIM);

            }







            //            SetLength(c, 0);
            //            for i:= 0 to length(a) - 1 do
            //                    begin
            //SetLength(c, length(c) + 1);
            //            temp:= 0;
            //            for j:= 0 to length(a) - 1 do
            //                    begin
            //temp:= temp + a[j] * cos((2 * Pi * (j + 1) * (i + 1)) / length(a));
            //            end;
            //            c[length(c) - 1]:= temp;
            //            end;









            ////k:=2* pi/size;
            //float k = 2 * Math.PI / N;
            ////norm:=1/sqrt(size);
            //float norm = 1 / Math.Sqrt(N);

            //SetLength(Temp_Array_Re, size);
            //SetLength(Temp_Array_Im, size);



            //    For t:=0 To size-1 Do
            //    begin
            //Temp_Array_Re[t]:=Array_Re[t];
            //Temp_Array_Im[t]:=Array_Im[t];
            //end;

            //инициализация
            //for (int i = 0; i < N; i++)
            //{
            //    temp_array[i] = this.InitialSignal[i];
            //    temp_array_im[i] = 0;
            //}





            //For t:=0 To size-1 Do
            //Begin
            //    Array_Re[t]:=0;
            //    Array_Im[t]:=0;
            //             For f:=0 To size-1 Do
            //             begin
            //              Array_Re[t]:=Array_Re[t]+Temp_Array_Re[f]* cos(k* f*t);
            //              Array_Im[t]:=Array_Im[t]+Temp_Array_Re[f]* sin(k* f*t);
            //             end;
            //    Array_Re[t]:=norm* Array_Re[t];
            //    Array_Im[t]:=norm* Array_Im[t];
            //End;


        }

        //    Procedure DTF(var Array_Re, Array_Im:Array of real; size:LongInt);
        //Var
        //t, f : LongInt;
        //k,norm : Single;


        //Temp_Array_Re,Temp_Array_Im : Array Of Single;
        //Begin




        //End;


            /// <summary>
            /// выполним обратное преобразование Фурье
            /// </summary>
        public void invers()
        {
            this.inverseTransform = new List<float>();

            //            SetLength(b, 0);
            //            for i:= 0 to length(a) - 1 do
            //                    begin
            //SetLength(b, length(b) + 1);
            //            temp:= 0;
            //            for j:= 0 to length(a) - 1 do
            //                    begin
            //temp:= temp + (a[j] * sin((2 * Pi * (j + 1) * (i + 1)) / length(a))) / length(a);
            //            end;
            //            b[length(b) - 1]:= temp;
            //            end;

            List<float> Re = new List<float>();
            List<float> Im = new List<float>();
            int N = this._ReFurier.Count;
            float temp;
            float tempIM;

            for (int i = 0; i < N; i++)
            {
                Re.Add(0);
                Im.Add(0);

                temp = 0;
                tempIM = 0;
                for (int j = 0; j < N; j++)
                {
                    temp += (this._ReFurier[j]*(float)Math.Cos((2*Math.PI*(j+1)*(i+1))/N))/ N;
                    tempIM+= (this._ImFurier[j] * (float)Math.Sin((2 * Math.PI * (j + 1) * (i + 1)) / N)) / N;
                }
                Re[i] = temp;
                Im[i] = tempIM;

                // this.inverseTransform.Add(temp + tempIM);
                this.inverseTransform.Add(temp+tempIM);
            }




//            SetLength(c, 0);
//            for i:= 0 to length(a1) - 1 do
//                    begin
//SetLength(c, length(c) + 1);
//            temp:= 0;
//            for j:= 0 to length(a1) - 1 do
//                    begin
//temp:= temp + (a1[j] * cos((2 * Pi * (j + 1) * (i + 1)) / length(a1))) / length(a1);
//            end;
//            c[length(c) - 1]:= temp;
//            end;


        }



        //        Procedure inverseDTF(var Array_Re, Array_Im:Array of real; size:LongInt);
        //Var
        //t, f : LongInt;
        //k,norm : Single;
        //Temp_Array_Re,Temp_Array_Im : Array Of Single;
        //Begin
        //k:=2*pi/size;
        //norm:=1/sqrt(size);
        //SetLength(Temp_Array_Re, size);
        //SetLength(Temp_Array_Im, size);
        //        For t:=0 To size-1 Do
        //        begin
        //Temp_Array_Re[t]:=Array_Re[t];
        //Temp_Array_Im[t]:=Array_Im[t];
        //end;
        //For t:=0 To size-1 Do
        //Begin
        //Array_Re[t]:=0;
        //Array_Im[t]:=0;
        //For f:=0 To size-1 Do
        //begin
        //Array_Re[t]:=Array_Re[t]+Temp_Array_Re[f]*cos(-k* f*t);
        //        Array_Im[t]:=Array_Im[t]+Temp_Array_Re[f]*sin(-k* f*t);
        //        end;
        //Array_Re[t]:=norm* Array_Re[t];
        //        Array_Im[t]:=norm* Array_Im[t];
        //        End;
        //End;









    }
}
