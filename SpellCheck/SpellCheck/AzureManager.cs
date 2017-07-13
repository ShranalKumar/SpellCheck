﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{
    class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<skmuspellchecktable> spellCheckTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("https://skmuspellcheck.azurewebsites.net/");
            this.spellCheckTable = this.client.GetTable<skmuspellchecktable>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }
                return instance;
            }
        }

        public async Task<List<skmuspellchecktable>> getAllRows()
        {
            return await this.spellCheckTable.ToListAsync();
        }

        public async Task PostWord(skmuspellchecktable NewWord)
        {
            await this.spellCheckTable.InsertAsync(NewWord);
        }
    }
}
