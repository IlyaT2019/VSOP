using Repository.Core;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PointSurface = RPA_FLAT.Core.Model.PointSurface;

namespace RPA_FLAT.Core
{
    public class Logica : IDisposable
    {
        private readonly AppRepository<object> _appRep;
        public Logica(AppRepository<object> appRep)
        {
            _appRep = appRep;
        }

        public void DoCommand()
        {
            foreach (var i in _appRep.AppVariableCommands)
            {
                if (i.IsChecked == true)
                {
                    Do(i.NameAppVariableCommand.ToString(), i.CommandVariables);
                }
                i.StatusCommand = StatusCommand.Finish;
            }
        }

        private void Do(string nameCommand, ObservableCollection<AppVariable<object>> appVar)
        {
            try
            {
                switch (nameCommand)
                {
                    case "SortingTable":
                        new Action<ObservableCollection<AppVariable<object>>>(SortingTable).Invoke(appVar);
                        break;
                    case "GetFirstPoint":
                        new Action<ObservableCollection<AppVariable<object>>>(GetFirstPoint).Invoke(appVar);
                        break;
                    case "GetMiddlePoint":
                        new Action<ObservableCollection<AppVariable<object>>>(GetMiddlePoint).Invoke(appVar);
                        break;
                    case "GetFinishPoint":
                        new Action<ObservableCollection<AppVariable<object>>>(GetFinishPoint).Invoke(appVar);
                        break;
                    case "GetApr":
                        new Action<ObservableCollection<AppVariable<object>>>(GetApr).Invoke(appVar);
                        break;
                    case "GetBpr":
                        new Action<ObservableCollection<AppVariable<object>>>(GetBpr).Invoke(appVar);
                        break;
                    case "GetD":
                        new Action<ObservableCollection<AppVariable<object>>>(GetD).Invoke(appVar);
                        break;
                    case "GetMaxPoint":
                        new Action<ObservableCollection<AppVariable<object>>>(GetMaxPoint).Invoke(appVar);
                        break;
                    case "GetD1":
                        new Action<ObservableCollection<AppVariable<object>>>(GetD1).Invoke(appVar);
                        break;
                    case "Analis":
                        new Action<ObservableCollection<AppVariable<object>>>(Analis).Invoke(appVar);
                        break;
                    case "isNotEqualByFirstAndFinishPoint":
                        new Action<ObservableCollection<AppVariable<object>>>(isNotEqualByFirstAndFinishPoint).Invoke(appVar);
                        break;
                    case "isEqualFirstPointByXAndNotEqualPointByZ_1":
                        new Action<ObservableCollection<AppVariable<object>>>(isEqualFirstPointByXAndNotEqualPointByZ_1).Invoke(appVar);
                        break;
                    case "isEqualFirstPointByXAndNotEqualPointByZ_2":
                        new Action<ObservableCollection<AppVariable<object>>>(isEqualFirstPointByXAndNotEqualPointByZ_2).Invoke(appVar);
                        break;
                    case "isEqualFirstPointByXAndNotEqualPointByZ_3":
                        new Action<ObservableCollection<AppVariable<object>>>(isEqualFirstPointByXAndNotEqualPointByZ_3).Invoke(appVar);
                        break;
                    case "isEqualFirstPointByXAndNotEqualPointByZ_4":
                        new Action<ObservableCollection<AppVariable<object>>>(isEqualFirstPointByXAndNotEqualPointByZ_4).Invoke(appVar);
                        break;
                    case "ListIf":
                        new Action<ObservableCollection<AppVariable<object>>>(ListIf).Invoke(appVar);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception _ex)
            {                
                MessageBox.Show(_ex.Message);
            }
        }

        //Как правильно передовать результата одной функции в другую функцию с отображением на форме!!!
        private void SortingTable(ObservableCollection<AppVariable<object>> appVariables)
        {
            var baseDt = (DataTable)appVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.BaseTable).Value;
            var dF = baseDt.DefaultView;
            dF.Sort = "X ASC";
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable);
            result.Value = dF.ToTable();
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        private void GetFirstPoint(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint);
            var row = ((DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value).AsEnumerable().FirstOrDefault();
            var point = new PointSurface()
            {
                Name = "Начальная точка",
                Number = Convert.ToInt32(row["Номер точки"]),
                X = Convert.ToInt32(row["X"]),
                Y = Convert.ToInt32(row["Y"]),
                Z = Convert.ToInt32(row["Z"])
            };
            result.Value = point;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        private void GetMiddlePoint(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint);
            var rows = ((DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value).AsEnumerable();
            _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value = rows.Count();
            var middleRow = rows.ElementAt((rows.Count() - 1) / 2);
            var point = new PointSurface()
            {
                Name = "Средняя точка",
                Number = Convert.ToInt32(middleRow["Номер точки"]),
                X = Convert.ToInt32(middleRow["X"]),
                Y = Convert.ToInt32(middleRow["Y"]),
                Z = Convert.ToInt32(middleRow["Z"])
            };
            result.Value = point;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        private void GetFinishPoint(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint);
            var row = ((DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value).AsEnumerable().LastOrDefault();
            var point = new PointSurface()
            {
                Name = "Конечная точка",
                Number = Convert.ToInt32(row["Номер точки"]),
                X = Convert.ToInt32(row["X"]),
                Y = Convert.ToInt32(row["Y"]),
                Z = Convert.ToInt32(row["Z"])
            };
            result.Value = point;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        public void GetMaxPoint(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MaxPoint);
            var a_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr).Value;
            var b_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr).Value;
            var countPoints = (Int32)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value;
            var dT = (DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value;
            int indexMaxPont = 0;
            for (int i = 0; i < countPoints; i++)
            {
                var result1 = Math.Abs(Convert.ToDouble(dT.Rows[i]["Z"]) - a_pr * Convert.ToDouble(dT.Rows[i]["X"]) - b_pr);
                var result2 = Math.Abs(Convert.ToDouble(dT.Rows[indexMaxPont]["Z"]) - a_pr * Convert.ToDouble(dT.Rows[indexMaxPont]["X"]) - b_pr);
                if (result1 > result2)
                    indexMaxPont = i;
            }

            result.Value = new PointSurface()
            {
                Number = indexMaxPont,
                X = Convert.ToDouble(dT.Rows[indexMaxPont]["X"]),
                Y = Convert.ToDouble(dT.Rows[indexMaxPont]["Y"]),
                Z = Convert.ToDouble(dT.Rows[indexMaxPont]["Z"])
            };
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        private void GetApr(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr);
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;
            result.Value = (firstPoint.Z - finishPoint.Z) / (firstPoint.X - finishPoint.X);
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();

        }

        private void GetBpr(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr);
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;
            var middlePoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var a_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr).Value;
            result.Value = ((firstPoint.Z + middlePoint.Z) - a_pr * (firstPoint.X + middlePoint.X)) / 2;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        public void GetD(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D);
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;
            var a_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr).Value;
            var b_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr).Value;
            result.Value = firstPoint.Z - a_pr * firstPoint.X - b_pr;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        public void GetD1(ObservableCollection<AppVariable<object>> appVariables)
        {
            var result = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D1);
            var a_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr).Value;
            var b_pr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr).Value;
            var maxPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MaxPoint).Value;
            result.Value = maxPoint.Z - a_pr * maxPoint.X - b_pr;
            CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
        }

        private double GetD1Temp(ObservableCollection<AppVariable<object>> appVariables)
        {
            var d1 = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D1).Value;
            return d1;
        }

        private double GetDTemp(ObservableCollection<AppVariable<object>> appVariables)
        {
            var d = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D).Value;
            return d;

        }

        public void Analis(ObservableCollection<AppVariable<object>> appVariables)
        {
            //var d1 = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D1).Value;
            //var d = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D).Value;

            while (Math.Abs(Math.Abs(GetD1Temp(appVariables)) - Math.Abs(GetDTemp(appVariables))) > 0.0000001)
            {                
                //if (Math.Abs(Math.Abs(d1) - Math.Abs(d)) < 0.0000001)
                //{
                    //if (isNotEqualByFirstAndFinishPoint(appVariables))

                    isEqualFirstPointByXAndNotEqualPointByZ_1(appVariables);
                    isEqualFirstPointByXAndNotEqualPointByZ_2(appVariables);
                    isEqualFirstPointByXAndNotEqualPointByZ_3(appVariables);
                    isEqualFirstPointByXAndNotEqualPointByZ_4(appVariables);                
                //}
                ListIf(appVariables);
                CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
                GetApr(appVariables);
                CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
                GetBpr(appVariables);
                CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
                GetD(appVariables);
                CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();
                GetD1(appVariables);
                CollectionViewSource.GetDefaultView(_appRep.AppVariables).Refresh();

            }
        }

        #region Условие 1
        public void isNotEqualByFirstAndFinishPoint(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;

            if (middelPoint.X != firstPoint.X && middelPoint.X != finishPoint.X)
            {
                //1. Если средняя точка не равняется первой и последней точке,то
                //2. Заново строим среднею прямую
                //Выход из цикла
                //return true;
            }
            //return false;
        }
        #endregion 
                
        #region Условие 2
        public void isEqualFirstPointByXAndNotEqualPointByZ_1(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;

            if (middelPoint.X == firstPoint.X && middelPoint.Z < middelPoint.Z)
            {
                CalculateAminAndNmin(appVariables);
                GetCoefficAdjoinFlatness_1(appVariables);
            }
        }

        private void CalculateAminAndNmin(ObservableCollection<AppVariable<object>> appVariables)
        {
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var countPoints = (int)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value;
            var a = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A);
            var aMin = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin);
            var dT = (DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value;

            aMin.Value = 1000;

            // TODO: обязательная доработка, т.к. число будет получаться не целым при нечетном количестве точек.
            // При высчитывании средней точки при нечетном числе добавлять 0.5.
            for (int i = Convert.ToInt32(middelPoint.Number) + 1; i < countPoints; i++)
            {
                var temp = Convert.ToDouble(dT.Rows[i]["X"]);
                if (middelPoint.X > Convert.ToDouble(dT.Rows[i]["X"]))
                {
                    a.Value = (Convert.ToDouble(dT.Rows[i]["Z"]) - middelPoint.Z) / (Convert.ToDouble(dT.Rows[i]["X"]) - middelPoint.X);

                    GetCoefficAminAndNmin(appVariables, i);
                }
            }
        }

        private void GetCoefficAminAndNmin(ObservableCollection<AppVariable<object>> appVariables, int j)
        {
            var a = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A).Value;
            var aMin = Convert.ToDouble(_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin).Value);
            var nMin = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Nmin);

            if (a < aMin)
            {
                aMin = a;
                nMin.Value = j;
            }
        }

        private void GetCoefficAdjoinFlatness_1(ObservableCollection<AppVariable<object>> appVariables)
        {
            var aPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr);
            var bPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr);
            var d = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D);
            var aMin = Convert.ToDouble(_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin).Value);
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;

            if (aMin > (double)aPr.Value)
            {
                aPr.Value = aMin;
                bPr.Value = middelPoint.Z - (double)aPr.Value * middelPoint.X;
                d.Value = 0;
            }
        }
        #endregion
     
        #region Условие 3
        public void isEqualFirstPointByXAndNotEqualPointByZ_2(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;

            if (middelPoint.X == firstPoint.X && middelPoint.Z > firstPoint.Z)
            {
                CalculateAmaxAndNmax(appVariables);
                GetCoefficAdjoinFlatness_2(appVariables);
            }
        }

        private void CalculateAmaxAndNmax(ObservableCollection<AppVariable<object>> appVariables)
        {
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var countPoints = (int)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value;
            var a = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A);
            var aMax = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax);
            var dT = (DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value;

            aMax.Value = 1000.0;

            for (int i = Convert.ToInt32(middelPoint.Number) + 1; i < countPoints; i++)
            {
                if (middelPoint.X > Convert.ToDouble(dT.Rows[i]["X"]))
                {
                    a.Value = (Convert.ToDouble(dT.Rows[i]["Z"]) - middelPoint.Z) / (Convert.ToDouble(dT.Rows[i]["X"]) - middelPoint.X);

                    GetCoefficAmaxAndNmax(appVariables, i);
                }
            }
        }

        private void GetCoefficAmaxAndNmax(ObservableCollection<AppVariable<object>> appVariables, int j)
        {
            var a = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A).Value;
            var aMax = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax).Value;
            var nMax = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Nmax);

            if (a > aMax)
            {
                aMax = a;
                nMax.Value = j;
            }
        }

        private void GetCoefficAdjoinFlatness_2(ObservableCollection<AppVariable<object>> appVariables)
        {
            var aPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr);
            var bPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr);
            var d = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D);
            var aMax = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;

            if (aMax < (double)aPr.Value)
            {
                aPr.Value = aMax;
                bPr.Value = middelPoint.Z - (double)aPr.Value * middelPoint.X;
                d.Value = 0;
            }
        }
        #endregion
     
        #region Условие 4
        public void isEqualFirstPointByXAndNotEqualPointByZ_3(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;

            if (middelPoint.X == firstPoint.X && middelPoint.Z < finishPoint.Z)
            {
                CalculateAmaxAndmax_2(appVariables);
                GetCoefficAdjoinFlatness_3(appVariables);
            }
        }

        private void CalculateAmaxAndmax_2(ObservableCollection<AppVariable<object>> appVariables)
        {
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var countPoints = (int)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value;
            var a = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A);
            var aMax = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax);
            var dT = (DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value;

            aMax.Value = -1000.0;

            for (int i = 0; i < Convert.ToInt32(middelPoint.Number) - 1; i++)
            {
                if (middelPoint.X != Convert.ToDouble(dT.Rows[i]["X"]))
                {
                    a.Value = (Convert.ToDouble(dT.Rows[i]["Z"]) - middelPoint.Z) / (Convert.ToDouble(dT.Rows[i]["X"]) - middelPoint.X);

                    GetCoefficAmaxAndNmax_2(appVariables, i);
                }
            }
        }

        private void GetCoefficAmaxAndNmax_2(ObservableCollection<AppVariable<object>> appVariables, int j)
        {
            var a = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A).Value;
            var aMax = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax).Value;
            var nMax = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Nmax);

            if (a > aMax)
            {
                aMax = a;
                nMax.Value = j;
            }
        }

        private void GetCoefficAdjoinFlatness_3(ObservableCollection<AppVariable<object>> appVariables)
        {
            var aPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr);
            var bPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr);
            var d = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D);
            var aMax = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amax).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;

            if (aMax < (double)aPr.Value)
            {
                aPr.Value = aMax;
                bPr.Value = middelPoint.Z - (double)aPr.Value * middelPoint.X;
                d.Value = 0;
            }
        }

        #endregion
        
        #region Условие 5
        public void isEqualFirstPointByXAndNotEqualPointByZ_4(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;

            if (middelPoint.X == finishPoint.X && middelPoint.Z > finishPoint.Z)
            {
                CalculateAminAndmin_2(appVariables);
                GetCoefficAdjoinFlatness_4(appVariables);
            }
        }

        private void CalculateAminAndmin_2(ObservableCollection<AppVariable<object>> appVariables)
        {
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var countPoints = (int)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.CountPoints).Value;
            var a = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A);
            var aMin = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin);
            var dT = (DataTable)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.SortTable).Value;

            aMin.Value = 1000.0;

            for (int i = 0; i < Convert.ToInt32(middelPoint.Number) - 1; i++)
            {
                if (middelPoint.X != Convert.ToDouble(dT.Rows[i]["X"]))
                {
                    a.Value = (Convert.ToDouble(dT.Rows[i]["Z"]) - middelPoint.Z) / (Convert.ToDouble(dT.Rows[i]["X"]) - middelPoint.X);

                    GetCoefficAminAndmin_2(appVariables, i);
                }
            }
        }

        private void GetCoefficAminAndmin_2(ObservableCollection<AppVariable<object>> appVariables, int j)
        {
            var a = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A).Value;
            var aMin = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin).Value;
            var nMin = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Nmin);

            if (a < aMin)
            {
                aMin = a;
                nMin.Value = j;
            }
        }

        private void GetCoefficAdjoinFlatness_4(ObservableCollection<AppVariable<object>> appVariables)
        {
            var aPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr);
            var bPr = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr);
            var d = _appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.D);
            var aMin = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.Amin).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;

            if (aMin > (double)aPr.Value)
            {
                aPr.Value = aMin;
                bPr.Value = middelPoint.Z - (double)aPr.Value * middelPoint.X;
                d.Value = 0;
            }
        }

        private void ListIf(ObservableCollection<AppVariable<object>> appVariables)
        {
            var firstPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FirstPoint).Value;
            var middelPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MiddlePoint).Value;
            var finishPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.FinishPoint).Value;
            var maxPoint = (PointSurface)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.MaxPoint).Value;
            var aPr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.A_pr).Value;
            var bPr = (double)_appRep.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.B_pr).Value;

            var result = maxPoint.Z - aPr * maxPoint.X - bPr;

            if (maxPoint.X < firstPoint.X)
            {
                if ((firstPoint.Z - aPr * firstPoint.X - bPr) * result >= 0)
                {
                    firstPoint.Number = maxPoint.Number;
                    firstPoint.X = maxPoint.X;
                    firstPoint.Y = maxPoint.Y;
                    firstPoint.Z = maxPoint.Z;
                }
                else
                {
                    middelPoint.Number = firstPoint.Number;
                    middelPoint.X = firstPoint.X;
                    middelPoint.Y = firstPoint.Y;
                    middelPoint.Z = firstPoint.Z;

                    firstPoint.Number = maxPoint.Number;
                    firstPoint.X = maxPoint.X;
                    firstPoint.Y = maxPoint.Y;
                    firstPoint.Z = maxPoint.Z;
                }
            }

            if (maxPoint.X == firstPoint.X)
            {
                if ((firstPoint.Z - aPr * firstPoint.X - bPr) * result >= 0)
                {
                    firstPoint.Number = maxPoint.Number;
                    firstPoint.X = maxPoint.X;
                    firstPoint.Y = maxPoint.Y;
                    firstPoint.Z = maxPoint.Z;

                }
                else
                {
                    middelPoint.Number = maxPoint.Number;
                    middelPoint.X = maxPoint.X;
                    middelPoint.Y = maxPoint.Y;
                    middelPoint.Z = maxPoint.Z;
                }
            }

            if (maxPoint.X > firstPoint.X && maxPoint.X < middelPoint.X)
            {
                if ((firstPoint.Z - aPr * firstPoint.X - bPr) * result >= 0)
                {
                    firstPoint.Number = maxPoint.Number;
                    firstPoint.X = maxPoint.X;
                    firstPoint.Y = maxPoint.Y;
                    firstPoint.Z = maxPoint.Z;

                }
                else
                {
                    middelPoint.Number = maxPoint.Number;
                    middelPoint.X = maxPoint.X;
                    middelPoint.Y = maxPoint.Y;
                    middelPoint.Z = maxPoint.Z;
                }
            }

            if (maxPoint.X > middelPoint.X && maxPoint.X < finishPoint.X)
            {
                if ((finishPoint.Z - aPr * finishPoint.X - bPr) * result >= 0)
                {
                    finishPoint.Number = maxPoint.Number;
                    finishPoint.X = maxPoint.X;
                    finishPoint.Y = maxPoint.Y;
                    finishPoint.Z = maxPoint.Z;
                }
                else
                {
                    middelPoint.Number = maxPoint.Number;
                    middelPoint.X = maxPoint.X;
                    middelPoint.Y = maxPoint.Y;
                    middelPoint.Z = maxPoint.Z;
                }
            }

            if (maxPoint.X == finishPoint.X)
            {
                if ((finishPoint.Z - aPr * finishPoint.X - bPr) * result >= 0)
                {
                    //finishPoint = maxPoint;
                    finishPoint.Number = maxPoint.Number;
                    finishPoint.X = maxPoint.X;
                    finishPoint.Y = maxPoint.Y;
                    finishPoint.Z = maxPoint.Z;
                }
                else
                {
                    middelPoint.Number = maxPoint.Number;
                    middelPoint.X = maxPoint.X;
                    middelPoint.Y = maxPoint.Y;
                    middelPoint.Z = maxPoint.Z;
                }
            }

            if (maxPoint.X > finishPoint.X)
            {
                if ((finishPoint.Z - aPr * finishPoint.X - bPr) * result >= 0)
                {
                    //finishPoint = maxPoint;
                    finishPoint.Number = maxPoint.Number;
                    finishPoint.X = maxPoint.X;
                    finishPoint.Y = maxPoint.Y;
                    finishPoint.Z = maxPoint.Z;

                }
                else
                {
                    //middelPoint = finishPoint;
                    //finishPoint = maxPoint;
                    middelPoint.Number = finishPoint.Number;
                    middelPoint.X = finishPoint.X;
                    middelPoint.Y = finishPoint.Y;
                    middelPoint.Z = finishPoint.Z;

                    finishPoint.Number = maxPoint.Number;
                    finishPoint.X = maxPoint.X;
                    finishPoint.Y = maxPoint.Y;
                    finishPoint.Z = maxPoint.Z;

                }
            }

            if (firstPoint.X == middelPoint.X)
            {
                if ((middelPoint.Z - aPr * middelPoint.X - bPr) * result >= 0)
                {
                    middelPoint.Number = finishPoint.Number;
                    middelPoint.X = finishPoint.X;
                    middelPoint.Y = finishPoint.Y;
                    middelPoint.Z = finishPoint.Z;
                }
                else
                {
                    //firstPoint = maxPoint;
                    firstPoint.Number = maxPoint.Number;
                    firstPoint.X = maxPoint.X;
                    firstPoint.Y = maxPoint.Y;
                    firstPoint.Z = maxPoint.Z;

                }
            }
            
        }



        #endregion



        public void Dispose()
        {

        }
    }
}