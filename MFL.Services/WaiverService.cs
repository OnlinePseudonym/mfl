using System;
using System.Collections.Generic;
using MFL.Data.Repository;
using MFL.Data.Entities;
using MFL.Data.Models;
using MFL.DataTransferObjects;
using MFL.Services.Clients;
using MFL.Common.Extensions;
using MFL.Common.Serialization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MFL.Services
{
    public interface IWaiverService
    {

        IEnumerable<WaiverTransaction> GetAll();
        WaiverTransaction GetById(int id);
        IEnumerable<WaiverTransaction> GetByIds(IEnumerable<int> ids);
        void Create(WaiverTransaction transaction);
        void Create(IEnumerable<WaiverTransaction> transactions);
        void Update(WaiverTransaction transaction);
        void Delete(int id);
        Task<GenericResponse> AddDropPlayers(string leagueId, string addIds = "", string dropIds = "");
    }

    public class WaiverService : IWaiverService
    {
        private IUnitOfWork _uow;
        private IRepository<WaiverTransaction> _transactions;
        private IMFLHttpClient _client;

        public WaiverService(IUnitOfWork uow, IMFLHttpClient client)
        {
            _uow = uow;
            _client = client;
            _transactions = _uow.GetRepository<WaiverTransaction>();
        }

        public void Create(WaiverTransaction transaction)
        {
            try
            {
                _transactions.Insert(transaction);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure creating transaction", ex);
            }
        }

        public void Create(IEnumerable<WaiverTransaction> transactions)
        {
            try
            {
                foreach (var transaction in transactions)
                {
                    _transactions.Insert(transaction);
                }
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure inserting transactions", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _transactions.Delete(id);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure deleting transaction", ex);
            }
        }

        public IEnumerable<WaiverTransaction> GetAll()
        {
            return _transactions.Get();
        }

        public WaiverTransaction GetById(int id)
        {
            try
            {
                return _transactions.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failure retrieving transaction", ex);
            }
        }

        public IEnumerable<WaiverTransaction> GetByIds(IEnumerable<int> ids)
        {
            try
            {
                return _transactions.Get(x => ids.Contains(x.Id));
            }
            catch (Exception ex)
            {
                throw new Exception("Failure retrieving transactions", ex);
            }
        }

        public void Update(WaiverTransaction transaction)
        {
            try
            {
                _transactions.Update(transaction);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure updating transaction", ex);
            }
        }

        public async Task<GenericResponse> AddDropPlayers(string leagueId, string addIds = "", string dropIds = "")
        {
            var response = await _client.Client.GetAsync($"/2020/import?TYPE=fcfsWaiver&L={leagueId}&ADD={addIds}&DROP={dropIds}&FRANCHISE_ID=&JSON=1");
            response.EnsureSuccessStatusCode();

            var transRes = new GenericResponse()
            {
                Success = true,
                Message = "Roster transaction successful"
            };
            var xmlRes = XElement.Parse(await response.Content.ReadAsStringAsync());

            if (xmlRes.Name.LocalName == "error")
            {
                transRes.Success = false;
                transRes.Message = xmlRes.Value.ToString();
            }

            return transRes;
        }
    }
}
