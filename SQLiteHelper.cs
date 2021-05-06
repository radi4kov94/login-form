﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plamen
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Person>().Wait();
        }

        //Insert and Update new record
        public Task<int> SaveItemAsync(Person person)
        {
            if (person.PersonID != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        }

        //Delete
        public Task<int> DeleteItemAsync(Person person)
        {
            return db.DeleteAsync(person);
        }

        //Read All Items
        public Task<List<Person>> GetItemsAsync()
        {
            return db.Table<Person>().ToListAsync();
        }


        //Read Item
        public Task<Person> GetItemAsync(int personId)
        {
            return db.Table<Person>().Where(i => i.PersonID == personId).FirstOrDefaultAsync();
        }

        //LogIn Item
        public Task<Person> LogInAsync(string name, string password)
        {
            return db.Table<Person>().Where(i => i.Name == name && i.Password == password).FirstOrDefaultAsync();
        }
    }
}
