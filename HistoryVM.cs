using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Calculator
{
    public class HistoryVM
    {
        string _dbPath;

        private SQLiteAsyncConnection conn;

        private string StatusMessage { get; set; }

        private async Task Init()
        {
            if (conn != null)
                return;
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<HistoryCalculations>();
                     
        }



        public HistoryVM(string dbPath)
        {
            _dbPath = dbPath;

        }



        public async Task SaveHistory(string calculation)
        {
            int result = 0;
            try
            {
                // History: Call Init()
                await Init();

                // basic validation to ensure a calculation was entered
                if (string.IsNullOrEmpty(calculation))
                    throw new Exception("Valid name required");

                //  History: Insert the new calculation into the database
                result = await conn.InsertAsync(new HistoryCalculations { Calculation = calculation });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, calculation);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", calculation, ex.Message);
            }
        }

        public async Task<List<HistoryCalculations>> GetHistoryItems()
        {
             
            try
            {
                await Init();
                return await conn.Table<HistoryCalculations>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<HistoryCalculations>();
        }

        public async Task<int> ClearHistory()
        {
            await Init();
            return await conn.DeleteAllAsync<HistoryCalculations>();

        }

    }
}
