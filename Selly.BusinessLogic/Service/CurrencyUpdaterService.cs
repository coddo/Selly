using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Selly.BusinessLogic.Core;
using Selly.BusinessLogic.Utility;
using Selly.Models;

namespace Selly.BusinessLogic.Service
{
    public class CurrencyUpdaterService
    {
        private const string BASE_CURRENCY = "RON";

        private const string CURRENCY_PROVIDER_BASE_URL = "http://api.fixer.io/latest";
        private const string BASE_CURRENCY_PARAMETER_NAME = "base";
        private const string BASE_CURRENCY_PARAMETER_VALUE = BASE_CURRENCY;

        private static CurrencyUpdaterService mInstance;

        private readonly Timer mTimer;

        private CurrencyUpdaterService()
        {
            mTimer = new Timer(TimeSpan.FromHours(1), RefreshCurrencies);
        }

        public static CurrencyUpdaterService Instance => mInstance ?? (mInstance = new CurrencyUpdaterService());

        #region Main service logic

        public void StartUpdaterService()
        {
            mTimer.Start();
        }

        public void StopUpdaterService()
        {
            mTimer.Stop();
        }

        private static async Task RefreshCurrencies()
        {
            var currencyData = await RestClient.GetAsync<CurrencyModel>(CURRENCY_PROVIDER_BASE_URL, new Dictionary<string, string>
            {
                {
                    BASE_CURRENCY_PARAMETER_NAME, BASE_CURRENCY_PARAMETER_VALUE
                }
            }).ConfigureAwait(false);

            var existingCurrencies = await CurrencyCore.GetAllAsync().ConfigureAwait(false);

            if (existingCurrencies == null || existingCurrencies.Count == 0)
            {
                await PopulateCurrencies(currencyData).ConfigureAwait(false);
            }
            else
            {
                await UpdateCurrencies(existingCurrencies, currencyData).ConfigureAwait(false);
            }
        }

        #endregion

        #region Auxiliary methods

        private static async Task PopulateCurrencies(CurrencyModel currencyData)
        {
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Name = currencyData.Base,
                    Multiplier = 1
                }
            };

            currencies.AddRange(currencyData.Rates.Select(rate => new Currency
            {
                Name = rate.Key,
                Multiplier = rate.Value
            }));

            await CurrencyCore.CreateAsync(currencies).ConfigureAwait(false);
        }

        private static async Task UpdateCurrencies(IList<Currency> existingCurrencies, CurrencyModel currencyData)
        {
            await CreateOrUpdateCurrency(existingCurrencies, currencyData.Base, 1).ConfigureAwait(false);

            foreach (var rate in currencyData.Rates)
            {
                await CreateOrUpdateCurrency(existingCurrencies, rate.Key, rate.Value).ConfigureAwait(false);
            }

            await CurrencyCore.UpdateAsync(existingCurrencies).ConfigureAwait(false);
        }

        private static async Task CreateOrUpdateCurrency(IEnumerable<Currency> currencies, string name, double multiplier)
        {
            var currency = currencies.FirstOrDefault(c => c.Name == name);
            if (currency == null)
            {
                currency = new Currency
                {
                    Name = name,
                    Multiplier = multiplier
                };

                await CurrencyCore.CreateAsync(currency).ConfigureAwait(false);
            }
            else
            {
                currency.Multiplier = multiplier;
            }
        }

        #endregion

        #region Inline models

        public class CurrencyModel
        {
            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [JsonProperty("rates")]
            public IDictionary<string, double> Rates { get; set; }
        }

        #endregion
    }
}