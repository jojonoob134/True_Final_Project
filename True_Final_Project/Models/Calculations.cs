using Dapper;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Data;

namespace True_Final_Project.Models
{
    public class Calculations : ICalculations
    {
        private readonly IDbConnection _conn;
        public Calculations(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<CalcVal> GetAllCalc()
        {
            return _conn.Query<CalcVal>("SELECT * FROM calculating_chart;");
        }

        public CalcVal GetCalc(int id)
        {
            return _conn.QuerySingle<CalcVal>("SELECT * FROM calculating_chart WHERE monthID = @id", new { id = id });
        }

        public void UpdateCalc(CalcVal calc)
        {
            //_conn.Execute("UPDATE calculating_chart SET totalCost = (SELECT SUM(Cost) FROM cost_chart WHERE cost_chart.month = @month)",
            _conn.Execute("UPDATE calculating_chart AS cc SET cc.totalCost = (SELECT SUM(ccc.Cost) FROM cost_chart AS ccc WHERE ccc.month = @month)",
                new { month = calc.Month });
            _conn.Execute("UPDATE calculating_chart SET MonthlyIncome = @MonthlyIncome",
                new { MonthlyIncome = calc.MonthlyIncome });
        }

        public IEnumerable<CostVal> GetAllCost()
        {
            return _conn.Query<CostVal>("SELECT * FROM cost_chart;");
        }

        public CostVal GetCost(int id) 
        {
             return _conn.QuerySingle<CostVal>("SELECT * FROM cost_chart WHERE PurchesID = @id", new { id = id });
        }
        public void UpdateCost(CostVal cost)
        {
            _conn.Execute("UPDATE cost_chart SET PurchesName = @purchesName, Cost = @cost WHERE PurchesID = @id",
                new { purchesName = cost.PurchesName, cost = cost.Cost, id = cost.PurchesID });
        }

        public void InsertCostVal(CostVal CostToInsert)
        {
            _conn.Execute("INSERT INTO cost_chart (PURCHESNAME, COST, Month) VALUES (@purchesName, @cost, @month);",
                new { purchesName = CostToInsert.PurchesName, cost = CostToInsert.Cost, month = CostToInsert.Month });
        }

        public IEnumerable<Months> GetMonths()
        {
            return _conn.Query<Months>("SELECT * FROM calculating_chart;");
        }

        public CostVal AssignMonths()
        {
            //throw new NotImplementedException();
            var MonthList = GetMonths();
            var cost = new CostVal();
            cost.Months = MonthList;
            return cost;
        }

        public void DeleteCost(CostVal cost)
        {
            _conn.Execute("DELETE FROM cost_chart WHERE PurchesID = @id;", new { id = cost.PurchesID });
        }
    }
}
